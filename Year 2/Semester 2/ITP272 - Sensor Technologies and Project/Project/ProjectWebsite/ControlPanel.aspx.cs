using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ControlPanel : System.Web.UI.Page
{
    static string strConnectionString = ConfigurationManager.ConnectionStrings["SmartHomeDBConnectionString"].ConnectionString;

    SqlConnection con = new SqlConnection(strConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["User"]) == null)
            {
                Response.Redirect("Default.aspx");
            }        
            else
            {
                // load lights status
                SqlDataAdapter house_sda = new SqlDataAdapter("SELECT * FROM House WHERE House_Id=1", con);
                DataTable house_dt = new DataTable();
                house_sda.Fill(house_dt);
                tbLights.Text = "All lights " + house_dt.Rows[0][1].ToString();

                // load lights mode
                lblLightsMode.Text = GetLightsMode();

                // load windows status
                tbWindows.Text = "All windows " + house_dt.Rows[0][3].ToString();

                // load windows mode
                lblWindowsMode.Text = GetWindowsMode();

                SqlDataAdapter valid_Rfid_sda = new SqlDataAdapter("SELECT * FROM Valid_Rfid WHERE id=1", con);
                DataTable valid_Rfid_dt = new DataTable();
                valid_Rfid_sda.Fill(valid_Rfid_dt);
                string rfid_state = valid_Rfid_dt.Rows[0]["State"].ToString();
                lblRfidStatus.Text = rfid_state;

                Response.AppendHeader("Refresh", "30");
            }

            if (Session["User"] != null && (string)Session["Role"] == "Admin")
            {
                ddlAdminControl.Visible = true;
            }
        }
    }

    protected void btnSwitchOnLights_Click(object sender, EventArgs e)
    {
        string lights_mode = GetLightsMode();

        if (lights_mode == "Manual")
        {
            if (tbLights.Text.Contains("Off"))
            {
                tbLights.Text = "";
                tbLights.Text = "All lights On";
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='On' WHERE House_Id=1", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        else
        {
            string script = "alert(\"Lights are in auto mode. Please change to manual mode to switch on the lights\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
    }

    private string GetLightsMode()
    {
        // extracts lights mode from database
        SqlDataAdapter lights_mode_sda = new SqlDataAdapter("SELECT * FROM House WHERE House_Id=1", con);
        DataTable lights_mode_dt = new DataTable();
        lights_mode_sda.Fill(lights_mode_dt);
        string lights_mode = lights_mode_dt.Rows[0][2].ToString();
        return lights_mode;
    }

    protected void btnSwitchOffLights_Click(object sender, EventArgs e)
    {
        string lights_mode = GetLightsMode();

        if (lights_mode == "Manual")
        {
            if (tbLights.Text.Contains("On"))
            {
                tbLights.Text = "";
                tbLights.Text = "All lights Off";
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='Off' WHERE House_Id=1", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        else
        {
            string script = "alert(\"Lights are in auto mode. Please change to manual mode to switch on the lights\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
    }

    protected void btnLightsAuto_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Mode='Auto' WHERE House_Id=1", con);
        cmd.ExecuteNonQuery();
        con.Close();
        lblLightsMode.Text = "Auto";
    }

    protected void btnLightsManual_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Mode='Manual' WHERE House_Id=1", con);
        cmd.ExecuteNonQuery();
        con.Close();
        lblLightsMode.Text = "Manual";
    }

    private string GetWindowsMode()
    {
        // extracts windows mode from database
        SqlDataAdapter windows_mode_sda = new SqlDataAdapter("SELECT * FROM House WHERE House_Id=1", con);
        DataTable windows_mode_dt = new DataTable();
        windows_mode_sda.Fill(windows_mode_dt);
        string windows_mode = windows_mode_dt.Rows[0][4].ToString();
        return windows_mode;
    }

    protected void btnOpenWindows_Click(object sender, EventArgs e)
    {
        string windows_mode = GetWindowsMode();

        if (windows_mode == "Manual")
        {
            if (tbWindows.Text.Contains("Closed"))
            {
                tbWindows.Text = "";
                tbWindows.Text = "All windows Open";
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Open' WHERE House_Id=1", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        else
        {
            string script = "alert(\"Windows are in auto mode. Please change to manual mode to open windows\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
    }

    protected void btnCloseWindows_Click(object sender, EventArgs e)
    {
        string windows_mode = GetWindowsMode();

        if (windows_mode == "Manual")
        {
            if (tbWindows.Text.Contains("Open"))
            {
                tbWindows.Text = "";
                tbWindows.Text = "All windows Closed";
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Closed' WHERE House_Id=1", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        else
        {
            string script = "alert(\"Windows are in auto mode. Please change to manual mode to open windows\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
    }

    protected void btnWindowsAuto_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Mode='Auto' WHERE House_Id=1", con);
        cmd.ExecuteNonQuery();
        con.Close();
        lblWindowsMode.Text = "Auto";
    }

    protected void btnWindowsManual_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Mode='Manual' WHERE House_Id=1", con);
        cmd.ExecuteNonQuery();
        con.Close();
        lblWindowsMode.Text = "Manual";
    }

    protected void btnEnableRfid_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE Valid_Rfid SET State='Active' WHERE Valid_Rfid_Value='6A00493CACB3'", con);
        cmd.ExecuteNonQuery();
        con.Close();

        string script = "alert(\"RFID is enabled.\");";
        ScriptManager.RegisterStartupScript(this, GetType(),
                              "ServerControlScript", script, true);

        lblRfidStatus.Text = "Active";
    }

    protected void btnDisableRfid_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE Valid_Rfid SET State='Inactive' WHERE Id=1", con);
        cmd.ExecuteNonQuery();
        con.Close();

        string script = "alert(\"RFID is disabled.\");";
        ScriptManager.RegisterStartupScript(this, GetType(),
                              "ServerControlScript", script, true);

        lblRfidStatus.Text = "Inactive";
    }
}