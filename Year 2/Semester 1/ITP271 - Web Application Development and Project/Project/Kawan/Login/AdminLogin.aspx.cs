using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
using Recaptcha.Web;

public partial class AdminLogin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdminLogin_Click(object sender, EventArgs e)
    {   
        // validate admin login
        try
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Admin Where Username='" + Server.HtmlEncode(txtAdminUser.Text) + "' and Password='" + Server.HtmlEncode(txtAdminPass.Text) + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            // if captcha is not checked
            if (string.IsNullOrEmpty(Recaptcha1.Response))
            {
                lblValidAdmin.Text = "Captcha cannot be empty.";
            }
            else
            {
                RecaptchaVerificationResult result = Recaptcha1.Verify();
                if (result == RecaptchaVerificationResult.Success)
                {
                    if (dt.Rows.Count.ToString() == "1")
                    {
                        Session["AdminUsername"] = txtAdminUser.Text;
                        Response.Redirect("~/Admin/AdminUsers.aspx");
                    }
                    else
                    {
                        lblValidAdmin.Text = "Invalid admin username and/or password";
                    }
                }
                else if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
                {
                    lblValidAdmin.Text = "Incorrect captcha response.";
                }
                else
                {
                    lblValidAdmin.Text = "Some other problem with captcha.";
                }
            }
        }
        catch (Exception)
        {
            Response.Write("<script>alert('Error logging in. Please try again');</script>");
        }
        
    }
}