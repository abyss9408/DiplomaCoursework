using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

public partial class Support : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblError.Text = "";
        }
    }

    protected void lbSendMessage_Click(object sender, EventArgs e)
    {
        try
        {
            string s_from = email.Value;
            string s_subject = subject.Value;
            string s_message = "Hi, I am " + name.Value + " " + message.Value + ". My email is " + s_from;

            MailAddress to = new MailAddress("kawansmarthome@gmail.com");
            MailAddress from = new MailAddress(s_from);

            MailMessage email_message = new MailMessage(from, to);
            email_message.Subject = s_subject;
            email_message.Body = s_message;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("kawansmarthome@gmail.com", "Kawan123!");

            client.Send(email_message);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}