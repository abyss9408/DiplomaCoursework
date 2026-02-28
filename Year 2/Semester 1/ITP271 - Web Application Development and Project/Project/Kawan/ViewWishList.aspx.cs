using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ViewWishList : System.Web.UI.Page
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
                DataTable wish_dt = new DataTable();
                DataRow dr;
                wish_dt.Columns.Add("productid");
                wish_dt.Columns.Add("productname");
                wish_dt.Columns.Add("price");
                wish_dt.Columns.Add("productimage");

                // retrieve productid, quantity and total price based on user id
                string user_id = Session["UserId"].ToString();
                SqlDataAdapter items_sda = new SqlDataAdapter("Select * From Users_Wish_Items Where UserID='" + user_id + "'", con);
                DataTable items_dt = new DataTable();
                items_sda.Fill(items_dt);

                DataTable details_dt = new DataTable();
                string prod_id;
                for (int i = 0; i < items_dt.Rows.Count; i++)
                {
                    prod_id = items_dt.Rows[i][1].ToString();
                    // retrieve products' details such as name, image and unit price based on product id
                    SqlDataAdapter details_sda = new SqlDataAdapter("Select * From Products Where ProductID='" + prod_id + "'", con);
                    details_sda.Fill(details_dt);

                    // fill cart_dt
                    dr = wish_dt.NewRow();
                    dr["productid"] = items_dt.Rows[i][1].ToString();
                    dr["productname"] = details_dt.Rows[i][1].ToString();
                    dr["productimage"] = details_dt.Rows[i][2].ToString();
                    dr["price"] = details_dt.Rows[i][5].ToString();

                    wish_dt.Rows.Add(dr);

                }

                gv_WishList.DataSource = wish_dt;
                gv_WishList.DataBind();

                lbl_NoOfProducts.Text = gv_WishList.Rows.Count.ToString();
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

    protected void gv_WishList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string prod_id = gv_WishList.Rows[e.RowIndex].Cells[0].Text;
        string user_id = Session["UserId"].ToString();
        string query = "Delete From Users_Wish_Items Where UserID='" + user_id + "' and ProductID='" + prod_id + "'";
        SqlCommand remove_wish_item = new SqlCommand(query, con);

        con.Open();
        remove_wish_item.ExecuteNonQuery();
        con.Close();

        Response.Redirect("ViewWishList.aspx");
    }

    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        // clear user's wishlist based on user id
        string user_id = Session["UserId"].ToString();
        string query = "Delete From Users_Wish_Items Where UserID='" + user_id + "'";
        SqlCommand clear_wish = new SqlCommand(query, con);

        con.Open();
        clear_wish.ExecuteNonQuery();
        con.Close();

        Response.Redirect("ViewWishList.aspx");
    }
}