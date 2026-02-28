using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // check if the user is blacklisted or inactive
        string inactive = "Inactive";
        string blacklisted = "Blacklisted";
        if (Session["UserId"] != null && Session["Status"].ToString() == blacklisted)
        {
            Response.Redirect("/AccountBlacklisted.aspx");
        }
        else if (Session["UserId"] != null && Session["Status"].ToString() == inactive)
        {
            Response.Redirect("/AccountInactive.aspx");
        }
        // check if it is an admin
        else if (Session["AdminUsername"] != null)
        {
            Response.Redirect("/Admin/AdminUsers.aspx");
        }

        if (!IsPostBack)
        {
            // filter by category
            if (Request.QueryString["cat"] != null)
            {
                DataList1.DataSourceID = null;
                DataList1.DataSource = SqlDataSource2;
                DataList1.DataBind();

                lbl_Message.Text = DataList1.Items.Count.ToString() + " item(s) found";
            }
            else
            {
                DataList1.DataSourceID = null;
                DataList1.DataSource = SqlDataSource1;
                DataList1.DataBind();

                lbl_Message.Text = "";
            }
        }

        
    }

    protected void linkbtnViewAllProducts_Click(object sender, EventArgs e)
    {
        // list all products
        Response.Redirect("~/Products.aspx");
    }

    // displays phones only
    protected void linkbtnPhones_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx?cat=Phone");
    }

    // displays cameras only
    protected void linkbtnCameras_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx?cat=Camera");
    }

    // displays routers only
    protected void linkbtnRouters_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx?cat=Router");
    }

    // displays headsets only
    protected void linkbtnHeadset_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx?cat=Headset");
    }

    // displays keyboards only
    protected void linkbtnKeyboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx?cat=Keyboard");
    }

    // displays mice only
    protected void linkbtnMouse_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx?cat=Mouse");
    }

    // displays monitors only
    protected void linkbtnMonitor_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx?cat=Monitor");
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "viewdetails")
        {
            Response.Redirect("~/ProductDetails.aspx?ProdID=" + e.CommandArgument.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "")
        {
            DataList1.DataSourceID = null;
            DataList1.DataSource = sdsSearchBar;
            DataList1.DataBind();

            if (DataList1.Items.Count == 0)
            {
                lbl_Message.Text = "No items found";
            }
            else
            {
                lbl_Message.Text = DataList1.Items.Count.ToString() + " item(s) found";
            }
        }
        else
        {
            Response.Write("<script>alert('Please enter something in the textbox to search for products.')</script>");
        }
    }

    protected void btnPurchase_Click(object sender, EventArgs e)
    {

    }
}