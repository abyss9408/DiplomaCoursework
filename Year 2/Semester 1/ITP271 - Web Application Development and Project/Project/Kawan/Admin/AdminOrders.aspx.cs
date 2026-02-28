using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AdminOrders : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUsername"] != null)
        {
            lblAdminUser.Text = Session["AdminUsername"].ToString();
            if (!IsPostBack)
            {
                CalculateRevenue();

            }
        }
        else
        {
            Response.Redirect("/Login/AdminLogin.aspx");
        }
    }

    public void CalculateRevenue()
    {
        decimal revenue;
        SqlDataAdapter revenue_sda = new SqlDataAdapter("Select sum(Grand_Total) as Revenue From Orders", con);
        DataTable revenue_dt = new DataTable();
        revenue_sda.Fill(revenue_dt);

        revenue = (decimal)revenue_dt.Rows[0][0];
        lbl_Revenue.Text = revenue.ToString("C");
    }

    protected void btnLogout_Click1(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("/Login/AdminLogin.aspx");
    }

    protected void btn_ClearOrderbyOrderID_Click(object sender, EventArgs e)
    {
        string order_id;
        order_id = Server.HtmlEncode(txtClearOrderByOrderID.Text);

        SqlDataAdapter order_sda = new SqlDataAdapter("Select * From Orders Where OrderID='" + order_id + "'", con);
        DataTable order_dt = new DataTable();
        order_sda.Fill(order_dt);

        if (order_dt.Rows.Count > 0)
        {
            string query = "Delete From Orders_Products Where OrderID='" + order_id + "'";
            string query2 = "Delete From Orders Where OrderID='" + order_id + "'";

            con.Open();
            SqlCommand delete_orders_products = new SqlCommand(query, con);
            delete_orders_products.ExecuteNonQuery();

            SqlCommand delete_orders = new SqlCommand(query2, con);
            delete_orders.ExecuteNonQuery();
            con.Close();

            gv_Orders.DataSourceID = null;
            gv_Orders.DataSource = sdsOrders;
            gv_Orders.DataBind();

            gv_OrdersItems.DataSourceID = null;
            gv_OrdersItems.DataSource = sdsOrdersItems;
            gv_OrdersItems.DataBind();

            lbl_Message.Text = "Successfully deleted " + order_id;

            CalculateRevenue();
        }
        else
        {
            lbl_Message.Text = "Error: Invalid OrderID";
        }
        
    }
}