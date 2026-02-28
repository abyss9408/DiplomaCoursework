using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AdminUsers : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        int no_of_users;
        if (Session["AdminUsername"] != null)
        {
            lblAdminUser.Text = Session["AdminUsername"].ToString();
            SqlCommand count_no_of_users = new SqlCommand("Select count(*) from Users", con);
            con.Open();
            no_of_users = (int)count_no_of_users.ExecuteScalar();
            con.Close();
            lbl_NoOfUsers.Text = no_of_users.ToString();
        }
        else
        {
            Response.Redirect("/Login/AdminLogin.aspx");
        }
    }  

    protected void btnLogout_Click1(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("/Login/AdminLogin.aspx");
    }

    protected void btn_Blacklist_Click(object sender, EventArgs e)
    {
        SqlDataAdapter user_sda = new SqlDataAdapter("Select * From Users Where UserID='" + Server.HtmlEncode(txt_UserToBL.Text) + "'", con);
        DataTable user_dt = new DataTable();
        user_sda.Fill(user_dt);

        if (user_dt.Rows.Count.ToString() == "1")
        {
            SqlCommand blacklist = new SqlCommand("Update Users Set Status='Blacklisted' Where UserID='" + Server.HtmlEncode(txt_UserToBL.Text) + "'", con);
            con.Open();
            blacklist.ExecuteNonQuery();
            con.Close();

            lbl_BlacklistMsg.Text = "Successfully blacklisted user";
            GridView3.DataBind();
        }
        else
        {
            lbl_BlacklistMsg.Text = "Couldn't blacklist user: User is not found";
        }
    }

    protected void btn_Unblacklist_Click(object sender, EventArgs e)
    {

        SqlDataAdapter user_sda = new SqlDataAdapter("Select * From Users Where UserID='" + Server.HtmlEncode(txt_UserToBL.Text) + "'", con);
        DataTable user_dt = new DataTable();
        user_sda.Fill(user_dt);

        if (user_dt.Rows.Count.ToString() == "1")
        {
            SqlCommand unblacklist = new SqlCommand("Update Users Set Status='Active' Where UserID='" + Server.HtmlEncode(txt_UserToBL.Text) + "'", con);
            con.Open();
            unblacklist.ExecuteNonQuery();
            con.Close();

            lbl_BlacklistMsg.Text = "Successfully unblacklisted user";
            GridView3.DataBind();
        }
        else
        {
            lbl_BlacklistMsg.Text = "Couldn't unblacklist/reactivate user: User is not found";
        }
    }

    protected void ddpStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatusFilter.SelectedIndex == 0)
        {
            GridView3.DataSourceID = null;
            GridView3.DataSource = sdsUsers;
            GridView3.DataBind();
        }
        else
        {
            GridView3.DataSourceID = null;
            GridView3.DataSource = sdsFilterByStatus;
            GridView3.DataBind();
        }        
    }
}