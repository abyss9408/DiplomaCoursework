using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using Recaptcha.Web;

public partial class Register : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblRegister.Text = "";
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        // byte array
        byte[] salt;
        // generate salt
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        int user_id;
        int set_id;
        string username;
        string password_hash;
        string last_login;
        string security_qn;
        string answer;
        string status;
        string full_name;
        string date_of_birth;
        string salutation;
        string email;
        int phone_no;
        string country;
        string address;
        string city;
        string state_province;
        int zip_postal_code;
        string credit_debit_card_no;
        string name_on_card;
        string card_expiry;
        int cvv;

        // queries to insert data
        string query = "Insert into Users(UserID, Username, Password, Last_Login, Status, Full_Name, Date_Of_Birth, Salutation, Email, Phone_Number, Country, Address, City, State_Province, Zip_Postal_Code, Credit_Debit_Card_Number, Name_On_Card, Expiry_Of_Card, CVV)" +
            "values(@uid, @user, @pass, @lastlogin, @status, @fullname, @dob, @salute, @email, @phone, @country, @address, @city, @state, @zip, @cardnum, @nameoncard, @cardexpirey, @cvv)";
        string query2 = "Insert into Sec_Qns_Sets(SetID, UserID, Question, Answer) values(@sid, @uid, @question, @ans)";

        if (string.IsNullOrEmpty(Recaptcha1.Response))
        {
            lblRegister.Text = "Captcha cannot be empty.";
        }
        else
        {
            RecaptchaVerificationResult result = Recaptcha1.Verify();
            if (result == RecaptchaVerificationResult.Success)
            {
                SqlCommand users_cmd = new SqlCommand(query, con);
                SqlCommand qns_cmd = new SqlCommand(query2, con);

                // auto generate userid
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(UserID as int)), 0)+1 From Users", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                // auto generate setid
                SqlDataAdapter set_sda = new SqlDataAdapter("Select isnull(max(cast(SetID as int)), 0)+1 From Sec_Qns_Sets", con);
                DataTable set_dt = new DataTable();
                set_sda.Fill(set_dt);

                SqlDataAdapter user_sda = new SqlDataAdapter("Select * From Users Where Username='" + txtRegUser.Text + "' or Email='" + txtEmail.Text + "'", con);
                DataTable user_dt = new DataTable();
                user_sda.Fill(user_dt);


                if (user_dt.Rows.Count == 0)
                {
                    // hash and salt the password using PBKDF2
                    var pbkdf2 = new Rfc2898DeriveBytes(txtRegPass.Text, salt, 10000);

                    // place the string in the byte array
                    byte[] hash = pbkdf2.GetBytes(20);
                    // make new byte array where to store the hashed password+salt
                    byte[] hashBytes = new byte[36];
                    // place the hash and salt in their respective places
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);

                    // get data
                    user_id = (int)dt.Rows[0][0];
                    set_id = (int)set_dt.Rows[0][0];
                    username = Server.HtmlEncode(txtRegUser.Text);
                    password_hash = Convert.ToBase64String(hashBytes);
                    last_login = "0";
                    security_qn = ddlSecurityQn.SelectedItem.ToString();
                    answer = Server.HtmlEncode(txtAns.Text);
                    status = "Active";
                    full_name = Server.HtmlEncode(txtFullName.Text);
                    date_of_birth = ddlMonth.SelectedItem.ToString() + "/" + ddlDay.SelectedItem.ToString() + "/" + ddlYear.SelectedItem.ToString();
                    salutation = ddlSalutation.SelectedItem.ToString();
                    email = Server.HtmlEncode(txtEmail.Text);
                    phone_no = int.Parse(txtMobileNo.Text);
                    country = ddlCountry.SelectedItem.ToString();
                    address = Server.HtmlEncode(txtAddress.Text);
                    city = Server.HtmlEncode(txtCity.Text);
                    state_province = Server.HtmlEncode(txtStateProvince.Text);
                    zip_postal_code = int.Parse(txtZipPostalCode.Text);
                    credit_debit_card_no = txtCardNum.Text;
                    name_on_card = Server.HtmlEncode(txtNameOnCard.Text);
                    card_expiry = Server.HtmlEncode(txtExpiry.Text);
                    cvv = int.Parse(txtCVV.Text);

                    // insert data

                    con.Open();
                    users_cmd.Parameters.AddWithValue("@uid", user_id);
                    users_cmd.Parameters.AddWithValue("@user", username);
                    users_cmd.Parameters.AddWithValue("@pass", password_hash);
                    users_cmd.Parameters.AddWithValue("@lastlogin", last_login);
                    users_cmd.Parameters.AddWithValue("@status", status);
                    users_cmd.Parameters.AddWithValue("@fullname", full_name);
                    users_cmd.Parameters.AddWithValue("@dob", date_of_birth);
                    users_cmd.Parameters.AddWithValue("@salute", salutation);
                    users_cmd.Parameters.AddWithValue("@email", email);
                    users_cmd.Parameters.AddWithValue("@phone", phone_no);
                    users_cmd.Parameters.AddWithValue("@country", country);
                    users_cmd.Parameters.AddWithValue("@address", address);
                    users_cmd.Parameters.AddWithValue("@city", city);
                    users_cmd.Parameters.AddWithValue("@state", state_province);
                    users_cmd.Parameters.AddWithValue("@zip", zip_postal_code);
                    users_cmd.Parameters.AddWithValue("@cardnum", credit_debit_card_no);
                    users_cmd.Parameters.AddWithValue("@nameoncard", name_on_card);
                    users_cmd.Parameters.AddWithValue("@cardexpirey", card_expiry);
                    users_cmd.Parameters.AddWithValue("@cvv", cvv);
                    users_cmd.ExecuteNonQuery();

                    qns_cmd.Parameters.AddWithValue("@sid", set_id);
                    qns_cmd.Parameters.AddWithValue("@uid", user_id);
                    qns_cmd.Parameters.AddWithValue("@question", security_qn);
                    qns_cmd.Parameters.AddWithValue("@ans", answer);
                    qns_cmd.ExecuteNonQuery();

                    con.Close();
                    Response.Redirect("/RegistrationSuccessful.aspx");
                }
                else
                {
                    txtRegUser.Text = "";
                    txtEmail.Text = "";
                    Response.Write("<script>alert('Username/Email already exists.');</script>");
                    con.Close();
                }
            }
            if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
            {
                lblRegister.Text = "Incorrect captcha response.";
            }
            else
            {
                lblRegister.Text = "Some other problem with captcha.";
            }
        }

        

        


    }
}