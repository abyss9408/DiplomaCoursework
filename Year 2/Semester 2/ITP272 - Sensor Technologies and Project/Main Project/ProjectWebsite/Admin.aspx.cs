using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin : System.Web.UI.Page
{
    string strConnectionString = ConfigurationManager.ConnectionStrings["SmartHomeDBConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if (Session["User"] != null && (string)Session["Role"] != "Admin")
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                Response.AppendHeader("Refresh", "10");
            }
        }
    }

    private void lightDataUser2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Light_Time_Occured");
        dt.Columns.Add("Light_Value");
        dt.Rows.Add(0);
        dt.Rows.Add(1);
        dt.Rows.Add(2);
        dt.Rows.Add(3);
        dt.Rows.Add(4);
        dt.Rows.Add(5);
        dt.Rows[0]["Light_Time_Occured"] = "1:00 PM";
        dt.Rows[0]["Light_Value"] = "735";
        dt.Rows[1]["Light_Time_Occured"] = "2:00 PM";
        dt.Rows[1]["Light_Value"] = "780";
        dt.Rows[2]["Light_Time_Occured"] = "3:00 PM";
        dt.Rows[2]["Light_Value"] = "565";
        dt.Rows[3]["Light_Time_Occured"] = "4:00 PM";
        dt.Rows[3]["Light_Value"] = "496";
        dt.Rows[4]["Light_Time_Occured"] = "5:00 PM";
        dt.Rows[4]["Light_Value"] = "453";
        dt.Rows[5]["Light_Time_Occured"] = "6:00 PM";
        dt.Rows[5]["Light_Value"] = "395";
        LightChart2.DataSourceID = "";
        LightChart2.DataSource = dt;
        LightChart2.Series["Light Value"].XValueMember = "Light_Time_Occured";
        LightChart2.Series["Light Value"].YValueMembers = "Light_Value";
    }

    private void waterDataUser2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Water_Time_Occured");
        dt.Columns.Add("Water_Value");
        dt.Rows.Add(0);
        dt.Rows.Add(1);
        dt.Rows.Add(2);
        dt.Rows.Add(3);
        dt.Rows.Add(4);
        dt.Rows.Add(5);
        dt.Rows[0]["Water_Time_Occured"] = "1:00 PM";
        dt.Rows[0]["Water_Value"] = "980";
        dt.Rows[1]["Water_Time_Occured"] = "2:00 PM";
        dt.Rows[1]["Water_Value"] = "600";
        dt.Rows[2]["Water_Time_Occured"] = "3:00 PM";
        dt.Rows[2]["Water_Value"] = "235";
        dt.Rows[3]["Water_Time_Occured"] = "4:00 PM";
        dt.Rows[3]["Water_Value"] = "256";
        dt.Rows[4]["Water_Time_Occured"] = "5:00 PM";
        dt.Rows[4]["Water_Value"] = "453";
        dt.Rows[5]["Water_Time_Occured"] = "6:00 PM";
        dt.Rows[5]["Water_Value"] = "667";
        WaterChart2.DataSourceID = "";
        WaterChart2.DataSource = dt;
        WaterChart2.Series["Water Value"].XValueMember = "Water_Time_Occured";
        WaterChart2.Series["Water Value"].YValueMembers = "Water_Value";
    }

    private void rfidDataUser2()
    {

    }

    private void lightDataUser3()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Light_Time_Occured");
        dt.Columns.Add("Light_Value");
        dt.Rows.Add(0);
        dt.Rows.Add(1);
        dt.Rows.Add(2);
        dt.Rows.Add(3);
        dt.Rows.Add(4);
        dt.Rows.Add(5);
        dt.Rows[0]["Light_Time_Occured"] = "1:00 PM";
        dt.Rows[0]["Light_Value"] = "650";
        dt.Rows[1]["Light_Time_Occured"] = "2:00 PM";
        dt.Rows[1]["Light_Value"] = "668";
        dt.Rows[2]["Light_Time_Occured"] = "3:00 PM";
        dt.Rows[2]["Light_Value"] = "590";
        dt.Rows[3]["Light_Time_Occured"] = "4:00 PM";
        dt.Rows[3]["Light_Value"] = "554";
        dt.Rows[4]["Light_Time_Occured"] = "5:00 PM";
        dt.Rows[4]["Light_Value"] = "489";
        dt.Rows[5]["Light_Time_Occured"] = "6:00 PM";
        dt.Rows[5]["Light_Value"] = "415";
        LightChart3.DataSourceID = "";
        LightChart3.DataSource = dt;
        LightChart3.Series["Light Value"].XValueMember = "Light_Time_Occured";
        LightChart3.Series["Light Value"].YValueMembers = "Light_Value";
    }

    private void waterDataUser3()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Water_Time_Occured");
        dt.Columns.Add("Water_Value");
        dt.Rows.Add(0);
        dt.Rows.Add(1);
        dt.Rows.Add(2);
        dt.Rows.Add(3);
        dt.Rows.Add(4);
        dt.Rows.Add(5);
        dt.Rows[0]["Water_Time_Occured"] = "1:00 PM";
        dt.Rows[0]["Water_Value"] = "1011";
        dt.Rows[1]["Water_Time_Occured"] = "2:00 PM";
        dt.Rows[1]["Water_Value"] = "991";
        dt.Rows[2]["Water_Time_Occured"] = "3:00 PM";
        dt.Rows[2]["Water_Value"] = "978";
        dt.Rows[3]["Water_Time_Occured"] = "4:00 PM";
        dt.Rows[3]["Water_Value"] = "989";
        dt.Rows[4]["Water_Time_Occured"] = "5:00 PM";
        dt.Rows[4]["Water_Value"] = "1003";
        dt.Rows[5]["Water_Time_Occured"] = "6:00 PM";
        dt.Rows[5]["Water_Value"] = "1023";
        WaterChart3.DataSourceID = "";
        WaterChart3.DataSource = dt;
        WaterChart3.Series["Water Value"].XValueMember = "Water_Time_Occured";
        WaterChart3.Series["Water Value"].YValueMembers = "Water_Value";
    }

    protected void ddlDisplayUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdminOption.SelectedIndex == 0)
        {
            AssignRFID.Visible = true;
            User1.Visible = false;
            User2.Visible = false;
            User3.Visible = false;
        }
        else if (ddlAdminOption.SelectedIndex == 1)
        {
            AssignRFID.Visible = false;
            User1.Visible = true;
            User2.Visible = false;
            User3.Visible = false;
        }
        else if (ddlAdminOption.SelectedIndex == 2)
        {
            AssignRFID.Visible = false;
            User1.Visible = false;
            User2.Visible = true;
            User3.Visible = false;
            lightDataUser2();
            waterDataUser2();
        }
        else
        {
            AssignRFID.Visible = false;
            User1.Visible = false;
            User2.Visible = false;
            User3.Visible = true;
            lightDataUser3();
            waterDataUser3();
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(strConnectionString);
        string rfid_value;
        string user_id;

        rfid_value = ddlAssignRFID.SelectedValue;
        user_id = tbAssignRFID.Text;

        con.Open();
        SqlCommand assign = new SqlCommand("Update Valid_Rfid SET User_Id='" + user_id + "', State='Active', Used='Yes' WHERE Valid_Rfid_Value='" + rfid_value + "'", con);
        assign.ExecuteNonQuery();
        con.Close();

        string script = "alert(\"Successfully Assigned RFID to User.\");";
        ScriptManager.RegisterStartupScript(this, GetType(),
                              "ServerControlScript", script, true);
    }
}