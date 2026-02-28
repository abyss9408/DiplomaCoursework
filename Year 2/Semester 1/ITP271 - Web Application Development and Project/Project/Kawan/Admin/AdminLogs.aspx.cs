using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AdminLogs : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUsername"] != null)
        {
            lblAdminUser.Text = Session["AdminUsername"].ToString();
        }
        else
        {
            Response.Redirect("/Login/AdminLogin.aspx");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("/Login/AdminLogin.aspx");
    }

    protected void btnClearLogs_Click(object sender, EventArgs e)
    {
        string query = "Delete From User_login_logs";
        SqlCommand clear_logs = new SqlCommand(query, con);

        con.Open();
        clear_logs.ExecuteNonQuery();
        con.Close();

        Response.Redirect("AdminLogs.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_Filter_Error.Text = "";
            GridView6.DataSourceID = null;
            GridView6.DataSource = sdsFilterUserID;
            GridView6.DataBind();

            if (GridView6.Rows.Count == 0)
            {
                lbl_Filter_Error.Text = "Error: User is non existant or has not logged in yet for the first time";
            }
        }
        catch (Exception)
        {
            lbl_Filter_Error.Text = "Error: User Id is invalid";
        }      
    }

    protected void Lbtn_ListAll_Click(object sender, EventArgs e)
    {
        lbl_Filter_Error.Text = "";
        GridView6.DataSourceID = null;
        GridView6.DataSource = sdsLogs;
        GridView6.DataBind();
    }
}