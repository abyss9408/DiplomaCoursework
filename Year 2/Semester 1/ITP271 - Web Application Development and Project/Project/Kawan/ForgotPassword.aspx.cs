using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ForgotPassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_Proceed_Click(object sender, EventArgs e)
    {
        int user_id;
        // validate username and email
        SqlDataAdapter user_sda = new SqlDataAdapter("Select * From Users Where Username='" + Server.HtmlEncode(txt_Username.Text) + "'and Email='" + Server.HtmlEncode(txt_Email.Text) + "'", con);
        DataTable user_dt = new DataTable();
        user_sda.Fill(user_dt);

        // get user id
        user_id = (int)user_dt.Rows[0][0];

        // validate security question
        SqlDataAdapter sec_sda = new SqlDataAdapter("Select * From Sec_Qns_Sets Where UserID='" + user_id + "'and Question='" + ddl_SecurityQns.SelectedItem.ToString() + "'and Answer='" + Server.HtmlEncode(txt_Answer.Text) + "'", con);
        DataTable sec_dt = new DataTable();
        sec_sda.Fill(sec_dt);

        // if username, email and security question are valid
        if (user_dt.Rows.Count.ToString() == "1" && sec_dt.Rows.Count.ToString() == "1")
        {
            Session["Reset_Username"] = Server.HtmlEncode(txt_Username.Text);
            Session["Reset_Email"] = Server.HtmlEncode(txt_Email.Text);
            Response.Redirect("/ResetPassword.aspx");
        }
        else
        {
            lbl_Error.Text = "Username and/or Email does not exist/Security question and/or answer is incorrect";
        }
    }
}