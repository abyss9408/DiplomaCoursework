using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Recaptcha.Web;

public partial class Confirmation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string active = "Active";
        string inactive = "Inactive";
        string blacklist = "Blacklisted";
        if (Session["UserId"] != null && Session["Status"].ToString() == active)
        {
            if (!IsPostBack)
            {
                // for gv_Checkout
                DataTable checkout_dt = new DataTable();
                DataRow dr;
                checkout_dt.Columns.Add("productid");
                checkout_dt.Columns.Add("productname");
                checkout_dt.Columns.Add("quantity");
                checkout_dt.Columns.Add("price");
                checkout_dt.Columns.Add("totalprice");
                checkout_dt.Columns.Add("productimage");

                // retrieve productid, quantity and total price based on user id
                string user_id = Session["UserId"].ToString();
                SqlDataAdapter items_sda = new SqlDataAdapter("Select * From Users_Carts_Items Where UserID='" + user_id + "'", con);
                DataTable items_dt = new DataTable();
                items_sda.Fill(items_dt);

                DataTable details_dt = new DataTable();
                string prod_id;
                decimal gtotal = 0;
                for (int i = 0; i < items_dt.Rows.Count; i++)
                {
                    prod_id = items_dt.Rows[i][1].ToString();
                    // retrieve products' details such as name, image and unit price based on product id
                    SqlDataAdapter details_sda = new SqlDataAdapter("Select * From Products Where ProductID='" + prod_id + "'", con);
                    details_sda.Fill(details_dt);

                    // fill cart_dt
                    dr = checkout_dt.NewRow();
                    dr["productid"] = items_dt.Rows[i][1].ToString();
                    dr["productname"] = details_dt.Rows[i][1].ToString();
                    dr["productimage"] = details_dt.Rows[i][2].ToString();
                    dr["quantity"] = items_dt.Rows[i][2].ToString();
                    dr["price"] = details_dt.Rows[i][5].ToString();
                    dr["totalprice"] = items_dt.Rows[i][3].ToString();

                    checkout_dt.Rows.Add(dr);

                    // calculate grand total
                    gtotal = gtotal + Convert.ToDecimal(checkout_dt.Rows[i]["totalprice"].ToString());
                }

                gv_Checkout.DataSource = checkout_dt;
                gv_Checkout.DataBind();

                if (gv_Checkout.Rows.Count > 0)
                {
                    gv_Checkout.FooterRow.Cells[4].Text = "Total Amount";
                    gv_Checkout.FooterRow.Cells[5].Text = gtotal.ToString("C");
                }
                else
                {
                    // do nothing
                }
                
            }
            lbl_Date.Text = DateTime.Now.ToShortDateString();
            find_payment_details();
            findorderid();
        }
        // if the user has logged in and is inactive
        else if (Session["UserId"] != null && Session["Status"].ToString() == inactive)
        {
            Response.Redirect("AccountInactive.aspx");
        }
        // if the user has logged in and is blacklisted
        else if (Session["UserId"] != null && Session["Status"].ToString() == blacklist)
        {
            Response.Redirect("AccountBlacklisted.aspx");
        }
        else if (Session["AdminUsername"] != null)
        {
            Response.Redirect("/Admin/AdminUsers.aspx");
        }
        else
        {
            Response.Redirect("/Login/Login.aspx");
        }
    }

    public void find_payment_details()
    {
        string user_id = Session["UserId"].ToString();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Select UserID, Credit_Debit_Card_Number, CVV From Users Where UserID='" + user_id + "'", con);
        da.Fill(dt);
        txtCardNumber.Text = dt.Rows[0][1].ToString();
        txtCVV.Text = dt.Rows[0][2].ToString();
    }

    // calculate grand total (outdated)
    /*public decimal grandtotal()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["buyitems"];
        int nrow = dt.Rows.Count;
        int i = 0;
        decimal gtotal = 0;
        while (i < nrow)
        {
            gtotal = gtotal + Convert.ToDecimal(dt.Rows[i]["totalprice"].ToString());

            i = i + 1;
        }
        return gtotal;
    }*/

    public void findorderid()
    {
        string pass = "abcdefghijklmnopqrstuvwxyz123456789";
        Random r = new Random();
        char[] mypass = new char[5];
        for (int i = 0; i < 5; i++)
        {
            mypass[i] = pass[(int)(35 * r.NextDouble())];

        }
        string orderid;
        orderid = "Order" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + new string(mypass);

        lbl_OrderID.Text = orderid;


    }

    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        string user_id = Session["UserId"].ToString();
        string sub_quantity;
        string prod_to_subtract;
        string s_grand_total = gv_Checkout.FooterRow.Cells[5].Text.Substring(1);
        decimal grand_total = decimal.Parse(s_grand_total);

        

        try
        {
            if (string.IsNullOrEmpty(Recaptcha1.Response))
            {
                lbl_Error.Text = "Captcha cannot be empty.";
            }
            else
            {
                RecaptchaVerificationResult result = Recaptcha1.Verify();
                if (result == RecaptchaVerificationResult.Success)
                {
                    string insert_query = "Insert into Orders(OrderID, UserID, Date, Grand_Total) values(@orderid, @userid, @date, @grandtotal)";
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(insert_query, con);
                    cmd1.Parameters.AddWithValue("@orderid", lbl_OrderID.Text);
                    cmd1.Parameters.AddWithValue("@userid", user_id);
                    cmd1.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd1.Parameters.AddWithValue("@grandtotal", grand_total);
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    DataTable check_out_of_stock_dt = new DataTable();
                    DataTable check_qty_stock_dt = new DataTable();
                    string out_of_stock_prodname = "";
                    string qty_more_than_stock_prodname = "";
                    bool out_of_stock = false;
                    bool qty_more_than_stock = false;
                    for (int i = 0; i < gv_Checkout.Rows.Count; i++)
                    {
                        string order_products = "Insert into Orders_Products(OrderID, ProductID, Quantity, Total_Price) values(@orderid, @productid, @quantity, @totalprice)";
                        int quantity = int.Parse(gv_Checkout.Rows[i].Cells[4].Text);
                        int stock;
                        decimal total_price = decimal.Parse(gv_Checkout.Rows[i].Cells[5].Text);

                        // check if out of stock
                        SqlDataAdapter check_out_of_stock_sda = new SqlDataAdapter("Select * From Products Where ProductID='" + gv_Checkout.Rows[i].Cells[0].Text + "' and Stock='0'", con);
                        check_out_of_stock_sda.Fill(check_out_of_stock_dt);

                        // check if quantities of any product is more than stock available
                        SqlDataAdapter check_qty_stock_sda = new SqlDataAdapter("Select Stock From Products Where ProductID='" + gv_Checkout.Rows[i].Cells[0].Text + "'", con);
                        check_qty_stock_sda.Fill(check_qty_stock_dt);
                        stock = (int)check_qty_stock_dt.Rows[0][0];

                        if (check_out_of_stock_dt.Rows.Count > 0)
                        {
                            out_of_stock = true;
                            out_of_stock_prodname = gv_Checkout.Rows[i].Cells[1].Text;
                            break;
                        }
                        else if (quantity > stock)
                        {
                            qty_more_than_stock = true;
                            qty_more_than_stock_prodname = gv_Checkout.Rows[i].Cells[1].Text;
                            break;
                        }
                        else
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandText = order_products;
                            cmd2.Connection = con;
                            cmd2.Parameters.AddWithValue("@orderid", lbl_OrderID.Text);
                            cmd2.Parameters.AddWithValue("@productid", gv_Checkout.Rows[i].Cells[0].Text);
                            cmd2.Parameters.AddWithValue("@quantity", quantity);
                            cmd2.Parameters.AddWithValue("@totalprice", total_price);
                            cmd2.ExecuteNonQuery();

                            sub_quantity = gv_Checkout.Rows[i].Cells[4].Text.ToString();
                            prod_to_subtract = gv_Checkout.Rows[i].Cells[0].Text.ToString();
                            string subtract_stock = "Update Products Set Stock=Stock -'" + sub_quantity + "' Where ProductID='" + prod_to_subtract + "'";
                            SqlCommand cmd3 = new SqlCommand();
                            cmd3.CommandText = subtract_stock;
                            cmd3.Connection = con;
                            cmd3.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    if (out_of_stock)
                    {
                        lbl_Error.Text = "Error: " + out_of_stock_prodname + " is out of stock";
                    }
                    else if (qty_more_than_stock)
                    {
                        lbl_Error.Text = "Error: " + qty_more_than_stock_prodname + " quantity is more than stock available";
                    }
                    else
                    {
                        // clear user's shopping cart based on user id
                        string delete_query = "Delete From Users_Carts_Items Where UserID='" + user_id + "'";
                        SqlCommand clear_cart = new SqlCommand(delete_query, con);

                        con.Open();
                        clear_cart.ExecuteNonQuery();
                        con.Close();

                        Response.Redirect("/SuccessfulOrder.aspx");
                    }
                }
                else if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
                {
                    lbl_Error.Text = "Incorrect captcha response.";
                }
                else
                {
                    lbl_Error.Text = "Some other problem with captcha.";
                }
            }
        }
        catch (Exception)
        {
            lbl_Error.Text = "An error occured, please try again.";
        }

                
    }

    protected void btn_Purchase_Click(object sender, EventArgs e)
    {
        lbl_Prompt.Visible = true;
        Recaptcha1.Visible = true;
        btn_Purchase.Visible = false;
        btn_Confirm.Visible = true;
        btn_Cancel.Visible = true;
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        lbl_Prompt.Visible = false;
        btn_Purchase.Visible = true;
        btn_Confirm.Visible = false;
        btn_Cancel.Visible = false;
    }
}