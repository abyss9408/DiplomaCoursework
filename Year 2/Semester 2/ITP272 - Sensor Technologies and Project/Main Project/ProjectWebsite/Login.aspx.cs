using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    string strConnectionString = ConfigurationManager.ConnectionStrings["SmartHomeDBConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }       
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(strConnectionString);
        try
        {
            string user = tbUsername.Text;
            string pass = tbPassword.Text;

            SqlDataAdapter login_sda = new SqlDataAdapter("SELECT * FROM Login WHERE Username='" + user + "' AND Password='" + pass + "'", con);
            DataTable login_dt = new DataTable();
            login_sda.Fill(login_dt);

            if (login_dt.Rows.Count == 1)
            {
                string role = login_dt.Rows[0]["Account_Type"].ToString();
                Session["User"] = user;
                Session["Role"] = role;

                // check if login user is admin
                if (role == "Admin")
                {
                    Response.Redirect("Admin.aspx");
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }               
            }
            else
            {
                lbl_Error.Text = "Invalid username/password";
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message;
        }

    }
}