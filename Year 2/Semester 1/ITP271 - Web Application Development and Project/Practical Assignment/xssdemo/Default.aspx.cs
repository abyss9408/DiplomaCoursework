using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"] as string;

        if (id == null)
        {
            lblId.Text = "NA";
        }
        else
        {
            lblId.Text = id; // without encoding/escaping
            // lblId.Text = Server.HtmlEncode(id); // with encoding/escaping
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string raw_input = txtSubmit.Text;
        string encoded_input = Server.HtmlEncode(raw_input);
        lblDisplay.Text = "Your submitted comment: " + raw_input; // without encoding/escaping
        // lblDisplay.Text = "Your submitted comment: " + encoded_input; // with encoding/escaping
    }
}