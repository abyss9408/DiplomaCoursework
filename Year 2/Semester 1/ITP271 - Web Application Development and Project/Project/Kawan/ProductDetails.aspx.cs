using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class ProductDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ProdID"] != null)
        {
            // Get Product ID from querystring
            string prodID = Request.QueryString["ProdID"].ToString();
            string query = "Select * From Products where ProductID='" + prodID + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lbl_ProdID.Text = dt.Rows[0][0].ToString();
            lbl_ProdName.Text = dt.Rows[0][1].ToString();
            img_Product.ImageUrl = dt.Rows[0][2].ToString();
            lbl_ProdDetails.Text = dt.Rows[0][3].ToString();
            lbl_ProdStock.Text = dt.Rows[0][4].ToString();
            decimal price = (decimal)dt.Rows[0][5];
            lbl_Price.Text = price.ToString("C");
        }
        else
        {
            // do nothing
        }       
    }


    protected void btnAddProductToCart_Click(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Write("<script>alert('You need to login first before you can any items to cart');</script>");
        }
        else if (lbl_ProdStock.Text == "0") // product out of stock
        {
            Response.Write("<script>alert('This product is out of stock. Please wait for restock.');</script>");
        }
        else
        {
            string user_id = Session["UserId"].ToString();
            string prod_id = lbl_ProdID.Text;
            SqlDataAdapter cart_sda = new SqlDataAdapter("Select * From Users_Carts_Items Where UserID='" + user_id + "' and ProductID='" + prod_id + "'", con);
            DataTable cart_dt = new DataTable();
            cart_sda.Fill(cart_dt);

            // if product is not in cart
            if (cart_dt.Rows.Count == 0)
            {
                int stock;
                int quantity = int.Parse(txtQuantity.Text);
                string s_price = lbl_Price.Text.Substring(1);
                decimal price = decimal.Parse(s_price);
                decimal total_price = quantity * price;

                SqlDataAdapter check_stock = new SqlDataAdapter("Select Stock From Products Where ProductID='" + lbl_ProdID.Text + "'", con);
                DataTable check_stock_dt = new DataTable();
                check_stock.Fill(check_stock_dt);

                stock = (int)check_stock_dt.Rows[0][0];

                if (quantity == 0)
                {
                    Response.Write("<script>alert('Quantity cannot be 0');</script>");
                }
                else if (quantity <= stock)
                {
                    string query = "Insert into Users_Carts_Items(UserID, ProductID, Quantity, Total_Price)" +
                    " values(@uid, @pid, @qty, @tprice)";

                    // insert user shopping cart items into database
                    SqlCommand insert_into_cart = new SqlCommand(query, con);

                    con.Open();
                    insert_into_cart.Parameters.AddWithValue("@uid", user_id);
                    insert_into_cart.Parameters.AddWithValue("@pid", lbl_ProdID.Text);
                    insert_into_cart.Parameters.AddWithValue("@qty", quantity);
                    insert_into_cart.Parameters.AddWithValue("@tprice", total_price);
                    insert_into_cart.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("/ViewShoppingCart.aspx");
                }
                else
                {
                    Response.Write("<script>alert('The quantity you want to purchase is more than the stock we have. Please change the quantity.');</script>");
                }
                
            }
            else // if product is in cart
            {
                Response.Write("<script>alert('This product is already in your cart');</script>");
            }
            
        }
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx");
    }

    protected void btnSubtractQuantity_Click(object sender, EventArgs e)
    {
        int quantity = int.Parse(txtQuantity.Text);
        quantity -= 1;
        if (quantity <= 0)
        {
            lbl_Error.Text = "Quantity cannot be 0 or less than 0";
        }
        else
        {
            txtQuantity.Text = quantity.ToString();
        }
    }

    protected void btnAddQuantity_Click(object sender, EventArgs e)
    {
        int quantity = int.Parse(txtQuantity.Text);
        quantity += 1;
        txtQuantity.Text = quantity.ToString();
    }

    protected void btnWishList_Click(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Write("<script>alert('You need to login first before you can any items to wishlist');</script>");
        }
        else
        {
            string user_id = Session["UserId"].ToString();
            string prod_id = lbl_ProdID.Text;
            SqlDataAdapter wishlist_sda = new SqlDataAdapter("Select * From Users_Wish_Items Where UserID='" + user_id + "' and ProductID='" + prod_id + "'", con);
            DataTable wishlist_dt = new DataTable();
            wishlist_sda.Fill(wishlist_dt);

            // if product is not in wishlist
            if (wishlist_dt.Rows.Count == 0)
            {
                string query = "Insert into Users_Wish_Items(UserID, ProductID)" +
                " values(@uid, @pid)";

                // insert user wish list items into database
                SqlCommand insert_into_cart = new SqlCommand(query, con);

                con.Open();
                insert_into_cart.Parameters.AddWithValue("@uid", user_id);
                insert_into_cart.Parameters.AddWithValue("@pid", lbl_ProdID.Text);
                insert_into_cart.ExecuteNonQuery();
                con.Close();

                Response.Redirect("/ViewWishList.aspx");
            }
            else // if product is in wishlist
            {
                Response.Write("<script>alert('This product is already in your wishlist')</script>");
            }

            
        }
    }
}