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
using System.Security.Cryptography;
using Recaptcha.Web;

public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        int login_success_id;
        int user_id;
        string machine_name;
        string ip_address = "";
        string status = "";
        string active = "Active";
        string inactive = "Inactive";
        string blacklist = "Blacklisted";
        string last_login;

        try
        {
            // if captcha is not checked
            if (string.IsNullOrEmpty(Recaptcha1.Response))
            {
                lblValid.Text = "Captcha cannot be empty.";
            }
            else
            {
                RecaptchaVerificationResult result = Recaptcha1.Verify();
                if (result == RecaptchaVerificationResult.Success)
                {
                    // validate username
                    SqlDataAdapter user_sda = new SqlDataAdapter("Select * From Users Where Username='" + Server.HtmlEncode(txtUser.Text) + "'", con);
                    DataTable user_dt = new DataTable();
                    user_sda.Fill(user_dt);

                    // if username is valid
                    if (user_dt.Rows.Count.ToString() == "1")
                    {
                        // get the saved string
                        string saved_password_hash = user_dt.Rows[0][2].ToString();
                        // turn it into bytes
                        byte[] hashBytes = Convert.FromBase64String(saved_password_hash);
                        // take the salt out of the string
                        byte[] salt = new byte[16];
                        Array.Copy(hashBytes, 0, salt, 0, 16);
                        // hash the user inputted PW with the salt
                        var pbkdf2 = new Rfc2898DeriveBytes(txtPass.Text, salt, 10000);
                        // put the hashed input in a byte array so that it can be compared byte-to-byte
                        byte[] hash = pbkdf2.GetBytes(20);
                        // compare results(byte-by-byte)
                        // starting from 16 in the stored array cause 0-15 are the salt there.
                        int ok = 1;
                        for (int i = 0; i < 20; i++)
                        {
                            // f hash of the keyed in password does not match
                            if (hashBytes[i + 16] != hash[i])
                                ok = 0;
                        }

                        // if hash of the keyed in password matches the one in the database
                        if (ok == 1)
                        {
                            // generate login_success_id
                            SqlDataAdapter login_success_id_sda = new SqlDataAdapter("Select isnull(max(cast(LoginSuccessID as int)), 0)+1 From User_login_logs", con);
                            DataTable login_success_id_dt = new DataTable();
                            login_success_id_sda.Fill(login_success_id_dt);
                            login_success_id = (int)login_success_id_dt.Rows[0][0];

                            // get user id based on username
                            SqlDataAdapter user_id_sda = new SqlDataAdapter("Select * From Users Where Username='" + Server.HtmlEncode(txtUser.Text) + "'", con);
                            DataTable user_id_dt = new DataTable();
                            user_id_sda.Fill(user_id_dt);
                            user_id = (int)user_id_dt.Rows[0][0];

                            // get Coordinated Universal Time
                            DateTime date_uni_time = DateTime.UtcNow;

                            // get local Time
                            DateTime date_local_time = DateTime.Now;

                            // get computer name
                            machine_name = System.Environment.MachineName;

                            // get public ip address
                            try
                            {
                                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                                using (WebResponse response = request.GetResponse())
                                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                                {
                                    ip_address = stream.ReadToEnd();
                                }
                                int first = ip_address.IndexOf("Address: ") + 9;
                                int last = ip_address.IndexOf("</body>");
                                ip_address = ip_address.Substring(first, last - first);
                            }
                            catch (Exception)
                            {
                                ip_address = "Error getting user ip";
                            }
                            finally
                            {
                                // logs successful login
                                string query = "Insert into User_login_logs(LoginSuccessID, UserID, Login_Universal_Timestamp, Login_Local_Timestamp, Computer_Name, IP_Address)" + "values(@id, @uid, @unitimestamp, @localtimestamp, @cname, @ip)";
                                SqlCommand cmd = new SqlCommand(query, con);
                                con.Open();
                                cmd.Parameters.AddWithValue("@id", login_success_id);
                                cmd.Parameters.AddWithValue("@uid", user_id);
                                cmd.Parameters.AddWithValue("@unitimestamp", date_uni_time);
                                cmd.Parameters.AddWithValue("@localtimestamp", date_local_time);
                                cmd.Parameters.AddWithValue("@cname", machine_name);
                                cmd.Parameters.AddWithValue("@ip", ip_address);
                                cmd.ExecuteNonQuery();
                                con.Close();

                                last_login = user_dt.Rows[0][3].ToString();
                                if (last_login == "0")
                                {
                                    last_login = "You Have Logged in Website First Time";
                                    Session["firsttime"] = 1;
                                }

                                // sets last login
                                setlastlogin();

                                // check if the account is active, inactive or blacklisted
                                SqlDataAdapter status_sda = new SqlDataAdapter("Select * From Users Where UserID='" + user_id.ToString() + "'", con);
                                DataTable status_dt = new DataTable();
                                status_sda.Fill(status_dt);
                                status = status_dt.Rows[0][4].ToString();

                                // user is active
                                if (status == active)
                                {
                                    Session["UserId"] = user_id.ToString();
                                    Session["Username"] = txtUser.Text;
                                    Session["Status"] = active;
                                    Session["lastlogin"] = last_login;
                                    Response.Redirect("/Welcome.aspx");
                                }
                                // user deactivated his/her account
                                else if (status == inactive)
                                {
                                    Session["UserId"] = user_id.ToString();
                                    Session["Username"] = txtUser.Text;
                                    Session["Status"] = inactive;
                                    Session["lastlogin"] = last_login;
                                    Response.Redirect("/AccountInactive.aspx");
                                }
                                // user is blacklisted
                                else
                                {
                                    // redirects user to welcome page
                                    Session["UserId"] = user_id.ToString();
                                    Session["Username"] = txtUser.Text;
                                    Session["Status"] = blacklist;
                                    Session["lastlogin"] = last_login;
                                    Response.Redirect("/AccountBlacklisted.aspx");
                                }
                            }
                        }
                        else
                        {
                            lblValid.Text = "Invalid username and/or password";
                        }
                    }
                    else
                    {
                        lblValid.Text = "Invalid username and/or password";
                    }
                }
                else if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
                {
                    lblValid.Text = "Incorrect captcha response.";
                }
                else
                {
                    lblValid.Text = "Some other problem with captcha.";
                }
            }
        }
        catch (Exception)
        {
            Response.Write("<script>alert('Error logging in. Please try again');</script>");
        }
                
    }

    private void setlastlogin()
    {
        string updatedata = "Update Users set Last_Login='" + DateTime.Now + "' where username='" + txtUser.Text + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = updatedata;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
    }
}