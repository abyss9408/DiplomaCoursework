using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ViewShoppingCart : System.Web.UI.Page
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
                // for gv_ShoppingCart
                DataTable cart_dt = new DataTable();
                DataRow dr;
                cart_dt.Columns.Add("productid");
                cart_dt.Columns.Add("productname");
                cart_dt.Columns.Add("quantity");
                cart_dt.Columns.Add("price");
                cart_dt.Columns.Add("totalprice");
                cart_dt.Columns.Add("productimage");

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
                    dr = cart_dt.NewRow();
                    dr["productid"] = items_dt.Rows[i][1].ToString();
                    dr["productname"] = details_dt.Rows[i][1].ToString();
                    dr["productimage"] = details_dt.Rows[i][2].ToString();
                    dr["quantity"] = items_dt.Rows[i][2].ToString();
                    dr["price"] = details_dt.Rows[i][5].ToString();
                    dr["totalprice"] = items_dt.Rows[i][3].ToString();

                    cart_dt.Rows.Add(dr);

                    // calculate grand total
                    gtotal = gtotal + Convert.ToDecimal(cart_dt.Rows[i]["totalprice"].ToString());
                }

                gv_ShoppingCart.DataSource = cart_dt;
                gv_ShoppingCart.DataBind();

                if (gv_ShoppingCart.Rows.Count > 0)
                {
                    gv_ShoppingCart.FooterRow.Cells[5].Text = "Total Amount";
                    gv_ShoppingCart.FooterRow.Cells[6].Text = gtotal.ToString("C");
                }
                else
                {
                    // do nothing
                }

                // display number of products in user's shopping cart
                lbl_NoOfProducts.Text = gv_ShoppingCart.Rows.Count.ToString(); 

            }
        }
        else if (Session["UserId"] != null && Session["Status"].ToString() == inactive)
        {
            Response.Redirect("AccountInactive.aspx");
        }
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

    protected void gv_ShoppingCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string prod_id = gv_ShoppingCart.Rows[e.RowIndex].Cells[0].Text;
        string user_id = Session["UserId"].ToString();
        string query = "Delete From Users_Carts_Items Where UserID='" + user_id + "' and ProductID='" + prod_id + "'";
        SqlCommand remove_cart_item = new SqlCommand(query, con);

        con.Open();
        remove_cart_item.ExecuteNonQuery();
        con.Close();

        Response.Redirect("ViewShoppingCart.aspx");
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        bool qty_0_error = false;
        bool qty_more_than_stock_error = false;
        try
        {
            foreach (GridViewRow row in gv_ShoppingCart.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string productId = row.Cells[0].Text;

                    string userId = Session["UserId"].ToString();
                    int stock;
                    int quantity = int.Parse(((TextBox)row.Cells[4].FindControl("txtQuantity")).Text);
                    decimal price = Convert.ToDecimal(row.Cells[3].Text.ToString());
                    decimal totalprice = price * quantity;

                    SqlDataAdapter check_stock = new SqlDataAdapter("Select Stock From Products Where ProductID='" + productId + "'", con);
                    DataTable check_stock_dt = new DataTable();
                    check_stock.Fill(check_stock_dt);

                    stock = (int)check_stock_dt.Rows[0][0];

                    if (quantity == 0)
                    {
                        lbl_Error.Text = "Error: One or more of the quantities is set to 0";
                        qty_0_error = true;
                        break;
                    }
                    // if respective quantity is more than stock of the respective product available
                    else if (quantity > stock)
                    {
                        lbl_Error.Text = "Error: One or more of the products quantities is more than the stock available";
                        qty_more_than_stock_error = true;
                        break;
                    }
                    else
                    {
                        string query = "Update Users_Carts_Items Set Quantity=@qty, Total_Price=@tpice Where UserID=@uid and ProductID=@pid";
                        SqlCommand update_cart = new SqlCommand(query, con);
                        con.Open();
                        update_cart.Parameters.AddWithValue("@qty", quantity);
                        update_cart.Parameters.AddWithValue("@tpice", totalprice);
                        update_cart.Parameters.AddWithValue("@uid", userId);
                        update_cart.Parameters.AddWithValue("@pid", productId);
                        update_cart.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }
            if (qty_more_than_stock_error)
            {
                lbl_Error.Text = "Error: One or more the quantities of the respective products you want to buy is/are more than the stock of the respective products";
            }
            else if (qty_0_error)
            {
                lbl_Error.Text = "Error: One or more the quantities of the respective products you want to buy is/are 0";
            }
            else
            {
                Response.Redirect("/ViewShoppingCart.aspx");
            }
        }
        catch (FormatException e1)
        {
            lbl_Error.Text = e1.Message.ToString();
        }
        
    }

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        // clear user's shopping cart based on user id
        string user_id = Session["UserId"].ToString();
        string query = "Delete From Users_Carts_Items Where UserID='" + user_id + "'";
        SqlCommand clear_cart = new SqlCommand(query, con);

        con.Open();
        clear_cart.ExecuteNonQuery();
        con.Close();

        Response.Redirect("/ViewShoppingCart.aspx");
    }

    protected void btn_Checkout_Click(object sender, EventArgs e)
    {
        if (gv_ShoppingCart.Rows.Count == 0)
        {
            Response.Write("<script>alert('Please add items to your cart before checkout');</script>");
        }
        else
        {
            Response.Redirect("/Confirmation.aspx");
        }
    }
}