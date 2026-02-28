using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Recaptcha.Web;

public partial class Welcome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        // check if the user has logged in
        string active = "Active";
        string inactive = "Inactive";
        string blacklist = "Blacklisted";
        if (Session["UserId"] != null && Session["Status"].ToString() == active)
        {
            lblUserID.Text = Session["UserId"].ToString();
            lblUser.Text = Session["Username"].ToString();
            if (Session["lastlogin"].ToString() == "You Have Logged in Website First Time")
            {
                lbl_LastLogin.Text = "You Have Logged in Website For the First Time";
            }
            else
            {
                lbl_LastLogin.Text = "You Have Last Logged In Website :" + Session["lastlogin"].ToString();
            }
        }
        // if the user has logged in and is inactive
        else if (Session["UserId"] != null && Session["Status"].ToString() == inactive)
        {
            Response.Redirect("/AccountInactive.aspx");
        }
        // if the user has logged in and is blacklisted
        else if (Session["UserId"] != null && Session["Status"].ToString() == blacklist)
        {
            Response.Redirect("/AccountBlacklisted.aspx");
        }
        else if (Session["AdminUsername"] != null)
        {
            Response.Redirect("/Admin/AdminUsers.aspx");
        }
        else
        {
            Response.Redirect("/Login/Login.aspx");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        // terminate all sessions
        Session.RemoveAll();
        Response.Redirect("/Login/Login.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string query = "Insert into Feedback(FeedbackID, UserID, Feedback_Subject, Feedback_Desc)" + "values(@id, @uid, @subject, @desc)";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(FeedbackID as int)), 0)+1 From Feedback", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        int feedback_id = (int)dt.Rows[0][0];
        int user_id = int.Parse(lblUserID.Text);
        con.Open();
        cmd.Parameters.AddWithValue("@id", feedback_id);
        cmd.Parameters.AddWithValue("@uid", user_id);
        cmd.Parameters.AddWithValue("@subject", Server.HtmlEncode(txtSubject.Text));
        cmd.Parameters.AddWithValue("@desc", Server.HtmlEncode(txtDesc.Text));
        cmd.ExecuteNonQuery();
        con.Close();

        lblSubmit.Text = "Your feedback has been recorded!";
        Response.Write("<script>alert('Your feedback has been recorded!');</script>");
    }

    public static void sendWebHook(string url, string msg, string username)
    {
        Http.Post(url, new NameValueCollection()
        {
            {"username", username},
            {"content", msg}
        });
    }

    protected void btnSubmitIssue_Click(object sender, EventArgs e)
    {
        string webhook_url = "https://discordapp.com/api/webhooks/606398120527396890/VBzgpweAIdAAvI1Bj58Y9WZZqx-vV8dKqAdLAtaVmSPcuU7Ud0bYb66BOL-VeaSel-QT";
        string issue = txt_Issue.Text;
        string uid = lblUserID.Text;
        string uname = lblUser.Text;
        lbl_SupportError.Text = "";
        try
        {
            sendWebHook(webhook_url, string.Concat(new string[] { "```Issue: " + issue + "\nReported by: " + uname + "\nUserID: " + uid + "```", }), "Reports BOT");
            Response.Write("<script>alert('Your report has been submitted.');</script>");
        }
        catch (Exception)
        {
            lblSubmit.Text = "Error: You are sending too many requests at once. Please slow down.";
        }
    }
}