using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["User"]) == null)
            {
                NotLoggedIn.Visible = true;
                LoggedIn.Visible = false;
            }
            else
            {
                lblUser.Text = Session["User"].ToString();
                NotLoggedIn.Visible = false;
                LoggedIn.Visible = true;
            }

            if (Request.Url.AbsolutePath.EndsWith("Default.aspx"))
            {
                HomeCurrent.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("Admin.aspx"))
            {
                AdminCurrent.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("Dashboard.aspx"))
            {
                DashboardCurrent.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("ControlPanel.aspx"))
            {
                ControlPanelCurrent.Attributes["class"] = "nav-item active";
            }
            else if (Request.Url.AbsolutePath.EndsWith("Support.aspx"))
            {
                SupportCurrent.Attributes["class"] = "nav-item active";
            }
        }      
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("Default.aspx");
    }
}