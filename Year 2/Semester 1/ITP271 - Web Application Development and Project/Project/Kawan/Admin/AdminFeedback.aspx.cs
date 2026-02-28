using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AdminFeedback : System.Web.UI.Page
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

    protected void btnClearFeedback_Click(object sender, EventArgs e)
    {
        string query = "Delete From Feedback";
        SqlCommand clear_feedback = new SqlCommand(query, con);

        con.Open();
        clear_feedback.ExecuteNonQuery();
        con.Close();

        Response.Redirect("AdminFeedback.aspx");
    }

    protected void btnLogout_Click1(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("/Login/AdminLogin.aspx");
    }
}