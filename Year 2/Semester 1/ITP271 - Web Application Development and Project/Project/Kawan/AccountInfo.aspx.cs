using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class AccountInfo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string user_id;
        string username;
        string active = "Active";
        string inactive = "Inactive";
        string blacklist = "Blacklisted";
        DateTime dob;
        int age = 0;
        if (Session["UserId"] != null && Session["Status"].ToString() == active)
        {
            if (!IsPostBack)
            {
                user_id = Session["UserId"].ToString();
                username = Session["Username"].ToString();
                SqlDataAdapter sda = new SqlDataAdapter("Select * From Users Where UserID='" + user_id + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblUserID.Text = user_id;
                dob = Convert.ToDateTime(dt.Rows[0][6].ToString());

                // calculate age
                age = DateTime.Now.Year - dob.Year;
                if (DateTime.Now.DayOfYear < dob.DayOfYear)
                    age = age - 1;

                lbl_Age.Text = age.ToString();
                txtNameUpdate.Text = dt.Rows[0][5].ToString();
                txtEmailUpdate.Text = dt.Rows[0][8].ToString();
                txtMobileNumberUpdate.Text = dt.Rows[0][9].ToString();
                DropDownListCountryUpdate.Text = dt.Rows[0][10].ToString();
                txtAddressUpdate.Text = dt.Rows[0][11].ToString();
                txtCityUpdate.Text = dt.Rows[0][12].ToString();
                txtStateOrProvinceUpdate.Text = dt.Rows[0][13].ToString();
                txtZipOrPostalCodeUpdate.Text = dt.Rows[0][14].ToString();
                txtCreditCardOrDebitCardNumberUpdate.Text = dt.Rows[0][15].ToString();
                txtNameOnCardUpdate.Text = dt.Rows[0][16].ToString();
                txtExpiryOfCardUpdate.Text = dt.Rows[0][17].ToString();
                txtCVVUpdate.Text = dt.Rows[0][18].ToString();
            }            
        }
        else if (Session["UserId"] != null && Session["Status"].ToString() == inactive)
        {
            Response.Redirect("AccountInactive.aspx");
        }
        else if (Session["UserId"] != null && Session["Status"].ToString() == blacklist)
        {
            Response.Redirect("AccountBlacklisted.aspx");
        }
        else
        {
            Response.Redirect("/Login/Login.aspx");
        }
    }

    protected void btnDeleteAccount_Click(object sender, EventArgs e)
    {
        BtnNo.Visible = true;
        btnYes.Visible = true;
        lbl_DeactivatePrompt.Visible = true;
    }

    protected void btnUpdateAccountInfo_Click(object sender, EventArgs e)
    {
        string query;

        query = "Update Users Set Full_Name=@fname, " +
            "Email=@email, " +
            "Phone_Number=@pnum, " +
            "Country=@country, " +
            "Address=@address, " +
            "City=@city, " +
            "State_Province=@state, " +
            "Zip_Postal_Code=@zip, " +
            "Credit_Debit_Card_Number=@cardnum, " +
            "Name_On_Card=@cardname, " +
            "Expiry_Of_Card=@expiry, " +
            "CVV=@cvv " +
            "Where UserID='" + lblUserID.Text + "'";
        SqlCommand cmd = new SqlCommand(query, con);

        con.Open();
        cmd.Parameters.AddWithValue("@fname", txtNameUpdate.Text);
        cmd.Parameters.AddWithValue("@email", txtEmailUpdate.Text);
        cmd.Parameters.AddWithValue("@pnum", txtMobileNumberUpdate.Text);
        cmd.Parameters.AddWithValue("@country", DropDownListCountryUpdate.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@address", txtAddressUpdate.Text);
        cmd.Parameters.AddWithValue("@city", txtCityUpdate.Text);
        cmd.Parameters.AddWithValue("@state", txtStateOrProvinceUpdate.Text);
        cmd.Parameters.AddWithValue("@zip", txtZipOrPostalCodeUpdate.Text);
        cmd.Parameters.AddWithValue("@cardnum", txtCreditCardOrDebitCardNumberUpdate.Text);
        cmd.Parameters.AddWithValue("@cardname", txtNameOnCardUpdate.Text);
        cmd.Parameters.AddWithValue("@expiry", txtExpiryOfCardUpdate.Text);
        cmd.Parameters.AddWithValue("@cvv", txtCVVUpdate.Text);
        cmd.ExecuteNonQuery();
        con.Close();

        Response.Redirect("AccountInfo.aspx");
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        string query = "Update Users Set Status = 'Inactive' Where UserID='" + lblUserID.Text + "'";
        SqlCommand cmd = new SqlCommand(query, con);

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Response.Redirect("AccountInactive.aspx");
    }

    protected void BtnNo_Click(object sender, EventArgs e)
    {
        BtnNo.Visible = false;
        btnYes.Visible = false;
        lbl_DeactivatePrompt.Visible = false;
    }
}