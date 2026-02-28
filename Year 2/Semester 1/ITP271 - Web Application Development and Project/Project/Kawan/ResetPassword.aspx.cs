using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;

public partial class ResetPassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Reset_Username"] != null && Session["Reset_Email"] != null)
        {
            lbl_Username.Text = Session["Reset_Username"].ToString();
            lbl_Email.Text = Session["Reset_Email"].ToString();
        }
        else
        {
            Response.Redirect("/ForgotPassword.aspx");
        }
    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        // byte array
        byte[] salt;
        // generate salt
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        string password_hash;

        SqlDataAdapter user_sda = new SqlDataAdapter("Select * From Users Where Username='" + Server.HtmlEncode(lbl_Username.Text) + "'and Email='" + Server.HtmlEncode(lbl_Email.Text) + "'", con);
        DataTable user_dt = new DataTable();
        user_sda.Fill(user_dt);

        // query to reset password
        string query = "Update Users Set Password=@pass Where Username='" + Server.HtmlEncode(lbl_Username.Text) + "'and Email='" + Server.HtmlEncode(lbl_Email.Text) + "'";
        SqlCommand reset_password = new SqlCommand(query, con);

        // if username and email are valid
        if (user_dt.Rows.Count.ToString() == "1")
        {
            // hash and salt the password using PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(txt_ConfirmPassword.Text, salt, 10000);

            // place the string in the byte array
            byte[] hash = pbkdf2.GetBytes(20);
            // make new byte array where to store the hashed password+salt
            byte[] hashBytes = new byte[36];
            // place the hash and salt in their respective places
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            password_hash = Convert.ToBase64String(hashBytes);
            con.Open();
            reset_password.Parameters.AddWithValue("@pass", password_hash);
            reset_password.ExecuteNonQuery();
            con.Close();
           
            Session.RemoveAll();
            Response.Redirect("/ResetSuccessful.aspx");
        }
        else
        {
            // do nothing
        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("/Login.aspx");
    }
}