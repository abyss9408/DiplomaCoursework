using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            if ((Session["User"]) == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if (Session["User"] != null && (string)Session["Role"] == "Admin")
            {
                Response.Redirect("Admin.aspx");
            }
            else
            {
                Response.AppendHeader("Refresh", "10");
            }
        }
    }


    protected void LightChart_Load(object sender, EventArgs e)
    {

    }
}