using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class SignUp : System.Web.UI.Page
{
    string strConnectionString = ConfigurationManager.ConnectionStrings["SmartHomeDBConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(strConnectionString);
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Login(Username, Password, Email, Name, Phone_Number, Address, Postal_Code) "+
                "Values('"+tbUsername.Text+"','"+tbPassword.Text+ "','" + tbEmail.Text + "','" + tbName.Text + "','" + tbPhoneNumber.Text + "','" + tbAddress.Text + "','" + tbPostalCode.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

            string script = "alert(\"Successfully Registered\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message;
        }
    }
}