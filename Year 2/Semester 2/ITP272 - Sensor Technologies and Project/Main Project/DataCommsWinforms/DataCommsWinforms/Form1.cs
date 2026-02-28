using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace DataCommsWinforms
{
    public partial class Form1 : Form
    {
        static string strConnectionString = ConfigurationManager.ConnectionStrings["DataCommsDBConnection"].ConnectionString;

        DataComms dataComms;

        public delegate void myprocessDataDelegate(String strData);

        SqlConnection con = new SqlConnection(strConnectionString);

        private bool mouseDown;
        private Point lastLocation;

        int watt;
        float money_saved;

        string date;
        string time;

        private void saveLightSensorDataToDB(string strDate, string strTime, string strlightValue, string strStatus)
        {

            String strCommandText =
                "INSERT LightSensor(House_Id, Light_Date_Occured, Light_Time_Occured, Light_Value, Brightness_Status) " +
                "VALUES (@house_id, @date, @time, @value, @status)";

            SqlCommand updateCmd = new SqlCommand(strCommandText, con);
            updateCmd.Parameters.AddWithValue("@house_id", lblHouse_Id.Text);
            updateCmd.Parameters.AddWithValue("@date", strDate);
            updateCmd.Parameters.AddWithValue("@time", strTime);
            updateCmd.Parameters.AddWithValue("@value", strlightValue);
            updateCmd.Parameters.AddWithValue("@status", strStatus);

            con.Open();

            updateCmd.ExecuteNonQuery();

            con.Close();
        }

        private void saveRFIDSensorDataToDB(string strDate, string strTime, string strrfidValue, string strStatus)
        {

            String strCommandText =
                "INSERT RFID(House_Id, Rfid_Date_Occured, Rfid_Time_Occured, Rfid_Value, Rfid_Status) " +
                "VALUES (@house_id, @date, @time, @value, @status)";

            SqlCommand updateCmd = new SqlCommand(strCommandText, con);
            updateCmd.Parameters.AddWithValue("@house_id", lblHouse_Id.Text);
            updateCmd.Parameters.AddWithValue("@date", strDate);
            updateCmd.Parameters.AddWithValue("@time", strTime);
            updateCmd.Parameters.AddWithValue("@value", strrfidValue);
            updateCmd.Parameters.AddWithValue("@status", strStatus);

            con.Open();

            updateCmd.ExecuteNonQuery();

            con.Close();
        }

        private void saveMotionSensorDataToDB(string strDate, string strTime, string strmotionValue)
        {

            String strCommandText =
                "INSERT MotionSensor(House_Id, Motion_Date_Occured, Motion_Time_Occured, Motion_Value) " +
                "VALUES (@house_id, @date, @time, @value)";

            SqlCommand updateCmd = new SqlCommand(strCommandText, con);
            updateCmd.Parameters.AddWithValue("@house_id", lblHouse_Id.Text);
            updateCmd.Parameters.AddWithValue("@date", strDate);
            updateCmd.Parameters.AddWithValue("@time", strTime);
            updateCmd.Parameters.AddWithValue("@value", strmotionValue);

            con.Open();

            updateCmd.ExecuteNonQuery();

            con.Close();
        }

        private void saveWaterSensorDataToDB(string strDate, string strTime, string strwaterValue, string strStatus)
        {

            String strCommandText =
                "INSERT WaterSensor(Water_Date_Occured, Water_Time_Occured, Water_Value, Water_Status) " +
                "VALUES (@date, @time, @value, @status)";

            SqlCommand updateCmd = new SqlCommand(strCommandText, con);
            updateCmd.Parameters.AddWithValue("@house_id", lblHouse_Id.Text);
            updateCmd.Parameters.AddWithValue("@date", strDate);
            updateCmd.Parameters.AddWithValue("@time", strTime);
            updateCmd.Parameters.AddWithValue("@value", strwaterValue);
            updateCmd.Parameters.AddWithValue("@status", strStatus);

            con.Open();

            updateCmd.ExecuteNonQuery();

            con.Close();
        }

        // utility method
        private string extractStringValue(string strData, string ID)
        {
            string result = strData.Substring(strData.IndexOf(ID) + ID.Length);
            return result;
        }

        // utility method
        private float extractFloatValue(string strData, string ID)
        {
            return (float.Parse(extractStringValue(strData, ID)));
        }

        private int extractIntValue(string strData, string ID)
        {
            return (int.Parse(extractStringValue(strData, ID)));
        }

        // create data handler
        private void handleLightSensorData(string strData, string strDate, string strTime, string ID)
        {
            string strlightValue = extractStringValue(strData, ID);
            string status = "";

            // update GUI component
            tbRoomLight.Text = strlightValue;
            tbOverviewLight.Text = strlightValue;

            int fLightValue = extractIntValue(strData, ID);
            if (fLightValue <= 600)
                status = "Dark";
            else
                status = "Bright";
            tbRoomStatus.Text = status;

            // update database
            saveLightSensorDataToDB(strDate, strTime, strlightValue, status);
            fillLightChart();
        }

        // create data handler
        private void handleRfidData(string strData, string strDate, string strTime, string ID)
        {
            string strrfidvalue = extractStringValue(strData, ID);

            // update GUI component
            tbRFID.Text = strrfidvalue;
            

            SqlDataAdapter valid_Rfid_sda = new SqlDataAdapter("SELECT * FROM Valid_Rfid", con);
            DataTable valid_Rfid_dt = new DataTable();
            valid_Rfid_sda.Fill(valid_Rfid_dt);
            string valid_rfid = valid_Rfid_dt.Rows[0]["Valid_Rfid_Value"].ToString();
            string rfid_state = valid_Rfid_dt.Rows[0]["State"].ToString();
            string rfid_used = valid_Rfid_dt.Rows[0]["Used"].ToString();
            string status = "";
            if (strrfidvalue.Equals(valid_rfid) && rfid_state == "Active" && rfid_used == "Yes")
            {
                dataComms.sendData("VALIDRFID");
                status = "Valid";
                rtbRfidLog.AppendText("Access Granted: Door Opening (" + strDate + " " + strTime + ")\n");
            }
            else
            {
                dataComms.sendData("INVALIDRFID");
                status = "Invalid";
                rtbRfidLog.AppendText("Access Denied: Invalid RFID (" + strDate + " " + strTime + ")\n");
            }
            tbRFIDValidity.Text = status;
            // update database
            saveRFIDSensorDataToDB(strDate, strTime, strrfidvalue, status);
        }

        private void handleMotionData(string strData, string strDate, string strTime, string ID)
        {
            string strmotionvalue = extractStringValue(strData, ID);

            // update GUI component
            tbMotion.Text = strmotionvalue;
            tbOverviewMotion.Text = strmotionvalue;

            if (strmotionvalue == "1")
            {
                rtbMotion.AppendText(strDate + " " + strTime + ": Motion Detected!\n");
            }
            else
            {
                rtbMotion.AppendText(strDate + " " + strTime + ": No Motion Detected!\n");
            }           

            // update database
            saveMotionSensorDataToDB(strDate, strTime, strmotionvalue);
        }

        private void handleWaterSensorData(string strData, string strDate, string strTime, string ID)
        {
            string strMoistureValue = extractStringValue(strData, ID);

            // update GUI component
            tbMoisture.Text = strMoistureValue;
            tbOverviewWater.Text = strMoistureValue;

            int fMoistureValue = extractIntValue(strData, ID);
            string status = "";
            if (fMoistureValue > 1000)
                status = "Dry";
            else if (fMoistureValue < 200)
                status = "Extremely Wet";
            else
                status = "Moderately Wet";
            tbMoistureStatus.Text = status;           

            // update database
            saveWaterSensorDataToDB(strDate, strTime, strMoistureValue, status);
            fillWaterChart();
        }

        // need to edit
        private void extractSensorData(string strData, string strDate, string strTime)
        {
            // check whether Light Value and status are sent
            if (strData.IndexOf("LIGHT=") != -1)
                handleLightSensorData(strData, strDate, strTime, "LIGHT=");
            else if (strData.IndexOf("RFID=") != -1) // check rfid status
                handleRfidData(strData, strDate, strTime, "RFID=");
            else if (strData.IndexOf("MOTIONPRESENT=") != -1) // check motion status
                handleMotionData(strData, strDate, strTime, "MOTIONPRESENT=");
            else if (strData.IndexOf("MOISTURE=") != -1)
                handleWaterSensorData(strData, strDate, strTime, "MOISTURE=");
        }

        // Raw data received from hardware comes here
        public void handleSensorData(String strData)
        {
            

            extractSensorData(strData, date, time); // get sensors values out

            // update raw data received to listbox
            string strMessage = date + " " + time + ": " + strData;
            lbDataComms.Items.Insert(0, strMessage);
        }

        // This method is automatically called when data is received
        public void processDataReceive(String strData)
        {
            myprocessDataDelegate d = new myprocessDataDelegate(handleSensorData);
            lbDataComms.Invoke(d, new object[] { strData });
        }

        // This method is automatically called when data is received
        public void commsDataReceive(string dataReceived)
        {
            processDataReceive(dataReceived);
        }

        // This method is automatically called when there is error
        public void commsSendError(string errMsg)
        {
            MessageBox.Show(errMsg);
            processDataReceive(errMsg);
        }

        private void InitComms()
        {
            dataComms = new DataComms();
            dataComms.dataReceiveEvent += new DataComms.DataReceivedDelegate(commsDataReceive);
            dataComms.dataSendErrorEvent += new DataComms.DataSendErrorDelegate(commsSendError);
        }
        public Form1(string user_id, string house_id)
        {
            InitializeComponent();
            lblUser_Id.Text = user_id;
            lblHouse_Id.Text = house_id;
            timer1.Start();
        }

        private void frmDataComms_Load(object sender, EventArgs e)
        {
            date = DateTime.Now.ToString("MM/dd/yyyy"); // get current date
            time = DateTime.Now.ToString("h:mm tt"); // get current time
            InitComms();
            fillLightChart();
            fillWaterChart();
            fillEnergyChart();
            fillMoneyChart();

            OverviewPanel.Dock = DockStyle.Fill;
            LogsPanel.Dock = DockStyle.Fill;
            LightPanel.Dock = DockStyle.Fill;
            RFIDPanel.Dock = DockStyle.Fill;
            MotionPanel.Dock = DockStyle.Fill;
            WaterPanel.Dock = DockStyle.Fill;

            // load lights status
            SqlDataAdapter house_sda = new SqlDataAdapter("SELECT * FROM House WHERE House_Id=1", con);
            DataTable house_dt = new DataTable();
            house_sda.Fill(house_dt);
            rtbLights.AppendText("All lights " + house_dt.Rows[0]["Lights_Status"].ToString());
            tbLightsStatus.Text = house_dt.Rows[0]["Lights_Status"].ToString();

            // load lights mode
            tbLightsMode.Text = GetLightsMode();
            tbMotionLightsMode.Text = GetLightsMode();

            // load windows status
            rtbWindows.AppendText("All windows " + house_dt.Rows[0]["Windows_Status"].ToString());

            // load windows mode
            tbWindowsMode.Text = GetWindowsMode();

            // load saved energy data
            SqlDataAdapter energy_sda = new SqlDataAdapter("SELECT * FROM EnergySaving WHERE House_Id='" 
                + lblHouse_Id.Text + "' AND Date='" + date + "'", con);
            DataTable energy_dt = new DataTable();
            energy_sda.Fill(energy_dt);

            // on a new day
            if (energy_dt.Rows.Count == 0)
            {
                // create a new day record
                SqlCommand new_day = new SqlCommand("INSERT INTO EnergySaving(House_Id, Date, Energy_Saved, Money_Saved)" +
                    " VALUES(@house_id, @date, @energy, @money)", con);
                new_day.Parameters.AddWithValue("@house_id", lblHouse_Id.Text);
                new_day.Parameters.AddWithValue("@date", date);
                new_day.Parameters.AddWithValue("@energy", 0);
                new_day.Parameters.AddWithValue("@money", 0);

                con.Open();
                new_day.ExecuteNonQuery();
                con.Close();

                // load new energy data
                SqlDataAdapter new_energy_sda = new SqlDataAdapter("SELECT * FROM EnergySaving WHERE House_Id='"
                    + lblHouse_Id.Text + "' AND Date='" + date + "'", con);
                DataTable new_energy_dt = new DataTable();
                new_energy_sda.Fill(new_energy_dt);
                watt = (int)new_energy_dt.Rows[0]["Energy_Saved"];
            }
            else
            {
                watt = (int)energy_dt.Rows[0]["Energy_Saved"];
            }
            money_saved = watt * 0.01f;
            lblEnergySaved.Text = watt.ToString();
            lblMoneySaved.Text = money_saved.ToString("C");

        }

        private void btnSendRfid_Click(object sender, EventArgs e)
        {
            dataComms.sendData("SENDRFID");
        }

        private void btnSendLight_Click(object sender, EventArgs e)
        {
            dataComms.sendData("SENDLIGHT");
        }

        private void btnSendMotion_Click(object sender, EventArgs e)
        {
            dataComms.sendData("SENDMOTION");
        }

        //fillChart methods 
        private void fillLightChart()
        {
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Light_Time_Occured, Light_Value from LightSensor WHERE Light_Date_Occured='" + date + "'", con);
            adapt.Fill(ds);
            LightChart.DataSource = ds;
            LightChart.DataBind();
            //set the member of the chart data source used to data bind to the X-values of the series  
            LightChart.Series["Light Value"].XValueMember = "Light_Time_Occured";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            LightChart.Series["Light Value"].YValueMembers = "Light_Value";

            con.Close();
        }

        private void fillWaterChart()
        {
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Water_Time_Occured, Water_Value from WaterSensor WHERE Water_Date_Occured='" + date + "'", con);
            adapt.Fill(ds);
            WaterChart.DataSource = ds;
            WaterChart.DataBind();
            //set the member of the chart data source used to data bind to the X-values of the series  
            WaterChart.Series["Moisture Value"].XValueMember = "Water_Time_Occured";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            WaterChart.Series["Moisture Value"].YValueMembers = "Water_Value";

            con.Close();
        }

        private void btnSendWater_Click(object sender, EventArgs e)
        {
            dataComms.sendData("SENDWATER");
        } 

        private void btnSwitchOffLights_Click(object sender, EventArgs e)
        {
            string lights_mode = GetLightsMode();

            if (lights_mode == "Manual")
            {
                if (tbRoomLight.Text != "")
                {
                    int lightValue = int.Parse(tbRoomLight.Text);

                    if (lightValue <= 600 && rtbLights.Text.Contains("On"))
                    {
                        DialogResult dialogResult = MessageBox.Show("There is insufficient natural light. Do you want to switch off the lights?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            rtbLights.Clear();
                            rtbLights.AppendText("All lights Off");
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='Off' WHERE House_Id=1", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else if (rtbLights.Text.Contains("On"))
                    {
                        rtbLights.Clear();
                        rtbLights.AppendText("All lights Off");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='Off' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lights are in auto mode. Please change to manual mode to switch off the lights", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSwitchOnLights_Click(object sender, EventArgs e)
        {
            string lights_mode = GetLightsMode();
            
            if (lights_mode == "Manual")
            {
                if (tbRoomLight.Text != "")
                {
                    int lightValue = int.Parse(tbRoomLight.Text);

                    if (lightValue > 600 && rtbLights.Text.Contains("Off"))
                    {
                        DialogResult dialogResult = MessageBox.Show("There is sufficient natural light. Do you want to switch on the lights?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            rtbLights.Clear();
                            rtbLights.AppendText("All lights On");
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='On' WHERE House_Id=1", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else if (rtbLights.Text.Contains("Off"))
                    {
                        rtbLights.Clear();
                        rtbLights.AppendText("All lights On");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='On' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lights are in auto mode. Please change to manual mode to switch on the lights", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnOpenWindows_Click(object sender, EventArgs e)
        {
            string windows_mode = GetWindowsMode();

            if (windows_mode == "Manual")
            {
                if (tbMoisture.Text != "")
                {
                    int moistureValue = int.Parse(tbMoisture.Text);

                    if (moistureValue < 200 && rtbWindows.Text.Contains("Closed"))
                    {
                        DialogResult dialogResult = MessageBox.Show("It's raining outside. Do you want to open the windows?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            rtbWindows.Clear();
                            rtbWindows.AppendText("All windows Open");
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Open' WHERE House_Id=1", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else if (rtbWindows.Text.Contains("Closed"))
                    {
                        rtbWindows.Clear();
                        rtbWindows.AppendText("All windows Open");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Open' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Windows are in auto mode. Please change to manual mode to open windows", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }           
        }

        private void btnCloseWindows_Click(object sender, EventArgs e)
        {
            string windows_mode = GetWindowsMode();

            if (windows_mode == "Manual")
            {
                if (tbMoisture.Text != "")
                {
                    int moistureValue = int.Parse(tbMoisture.Text);

                    if (moistureValue >= 200 && rtbWindows.Text.Contains("Open"))
                    {
                        DialogResult dialogResult = MessageBox.Show("It's not raining outside. Do you want to close the windows?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            rtbWindows.Clear();
                            rtbWindows.AppendText("All windows Closed");
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Closed' WHERE House_Id=1", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else if (rtbWindows.Text.Contains("Open"))
                    {
                        rtbWindows.Clear();
                        rtbWindows.AppendText("All windows Closed");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Closed' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Windows are in auto mode. Please change to manual mode to close windows", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fillEnergyChart();
            fillMoneyChart();
            date = DateTime.Now.ToString("MM/dd/yyyy"); // get current date
            time = DateTime.Now.ToString("h:mm tt"); // get current time
            lblTime.Text = date + " " + time;
            string windows_mode = GetWindowsMode();
            string lights_mode = GetLightsMode();

            if (windows_mode == "Auto")
            {
                // auto close windows when it's raining outside and open windows when it's not raining outside
                if (tbMoisture.Text != "")
                {
                    int moistureValue = int.Parse(tbMoisture.Text);

                    // close the windows when moistureValue is less than 200
                    if (moistureValue < 200 && rtbWindows.Text.Contains("Open"))
                    {
                        rtbWindows.Clear();
                        rtbWindows.AppendText("All windows Closed");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Closed' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    // open the windows when moistureValue is more than or equal to 200
                    else if (moistureValue >= 200 && rtbWindows.Text.Contains("Closed"))
                    {
                        rtbWindows.Clear();
                        rtbWindows.AppendText("All windows Open");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Status='Open' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            if (lights_mode == "Auto")
            {
                // auto switch on/off lights depends on the environment light
                if (tbRoomLight.Text != "")
                {
                    int lightValue = int.Parse(tbRoomLight.Text);

                    // switch off the lights when lightValue is more than 600 and there is no motion
                    if (lightValue > 600 && rtbLights.Text.Contains("On") && tbMotion.Text == "0")
                    {
                        rtbLights.Clear();
                        rtbLights.AppendText("All lights Off");
                        tbLightsStatus.Text = "Off";
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='Off' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    // switch on the lights when lightValue is less than or equal to 600 and there is motion
                    else if (lightValue <= 600 && rtbLights.Text.Contains("Off") && tbMotion.Text == "1")
                    {
                        rtbLights.Clear();
                        rtbLights.AppendText("All lights On");
                        tbLightsStatus.Text = "On";
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Status='On' WHERE House_Id=1", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

            // load lights status
            SqlDataAdapter house_sda = new SqlDataAdapter("SELECT * FROM House WHERE House_Id=1", con);
            DataTable house_dt = new DataTable();
            house_sda.Fill(house_dt);
            rtbLights.Clear();
            rtbLights.AppendText("All lights " + house_dt.Rows[0]["Lights_Status"].ToString());
            tbLightsStatus.Text = house_dt.Rows[0]["Lights_Status"].ToString();

            // load lights mode
            tbLightsMode.Text = GetLightsMode();
            tbMotionLightsMode.Text = GetLightsMode();

            // load windows status
            rtbWindows.Clear();
            rtbWindows.AppendText("All windows " + house_dt.Rows[0]["Windows_Status"].ToString());

            // load windows mode
            tbWindowsMode.Text = GetWindowsMode();
        }

        private string GetWindowsMode()
        {
            // extracts windows mode from database
            SqlDataAdapter windows_mode_sda = new SqlDataAdapter("SELECT * FROM House WHERE House_Id=1", con);
            DataTable windows_mode_dt = new DataTable();
            windows_mode_sda.Fill(windows_mode_dt);
            string windows_mode = windows_mode_dt.Rows[0]["Windows_Mode"].ToString();
            return windows_mode;
        }

        private void btnWindowsManual_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Mode='Manual' WHERE House_Id=1", con);
            cmd.ExecuteNonQuery();
            con.Close();
            tbWindowsMode.Text = "Manual";
        }

        private void btnWindowsAuto_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE House SET Windows_Mode='Auto' WHERE House_Id=1", con);
            cmd.ExecuteNonQuery();
            con.Close();
            tbWindowsMode.Text = "Auto";
        }

        private string GetLightsMode()
        {
            // extracts lights mode from database
            SqlDataAdapter lights_mode_sda = new SqlDataAdapter("SELECT * FROM House WHERE House_Id=1", con);
            DataTable lights_mode_dt = new DataTable();
            lights_mode_sda.Fill(lights_mode_dt);
            string lights_mode = lights_mode_dt.Rows[0]["Lights_Mode"].ToString();
            return lights_mode;
        }

        private void btnLightsManual_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Mode='Manual' WHERE House_Id=1", con);
            cmd.ExecuteNonQuery();
            con.Close();
            tbLightsMode.Text = "Manual";
        }

        private void btnLightsAuto_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE House SET Lights_Mode='Auto' WHERE House_Id=1", con);
            cmd.ExecuteNonQuery();
            con.Close();
            tbLightsMode.Text = "Auto";
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            lbDataComms.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void btnDisplayLogs_Click(object sender, EventArgs e)
        {
            OverviewPanel.Visible = false;
            LogsPanel.Visible = true;
            LightPanel.Visible = false;
            RFIDPanel.Visible = false;
            MotionPanel.Visible = false;
            WaterPanel.Visible = false;
        }

        private void btnDisplayLight_Click(object sender, EventArgs e)
        {
            OverviewPanel.Visible = false;
            LogsPanel.Visible = false;
            LightPanel.Visible = true;
            RFIDPanel.Visible = false;
            MotionPanel.Visible = false;
            WaterPanel.Visible = false;
        }

        private void btnDisplayRfid_Click(object sender, EventArgs e)
        {
            OverviewPanel.Visible = false;
            LogsPanel.Visible = false;
            LightPanel.Visible = false;
            RFIDPanel.Visible = true;
            MotionPanel.Visible = false;
            WaterPanel.Visible = false;
        }

        private void btnDisplayMotion_Click(object sender, EventArgs e)
        {
            OverviewPanel.Visible = false;
            LogsPanel.Visible = false;
            LightPanel.Visible = false;
            RFIDPanel.Visible = false;
            MotionPanel.Visible = true;
            WaterPanel.Visible = false;
        }

        private void btnDisplayWater_Click(object sender, EventArgs e)
        {
            OverviewPanel.Visible = false;
            LogsPanel.Visible = false;
            LightPanel.Visible = false;
            RFIDPanel.Visible = false;
            MotionPanel.Visible = false;
            WaterPanel.Visible = true;
        }

        private void energyTimer_Tick(object sender, EventArgs e)
        {                     
            if (rtbLights.Text.Contains("Off") && tbLightsMode.Text == "Auto")
            {
                watt = watt + 1;
                money_saved = watt * 0.01f;

                // save to database
                con.Open();
                SqlCommand update = new SqlCommand("UPDATE EnergySaving SET Energy_Saved='" + watt + "', Money_Saved='" + money_saved +
                    "' WHERE House_Id='" + lblHouse_Id.Text + "' AND Date='" + date + "'", con);
                update.ExecuteNonQuery();
                con.Close();

                lblEnergySaved.Text = watt.ToString();
                lblMoneySaved.Text = money_saved.ToString("C");
            }           
        }

        private void fillEnergyChart()
        {
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Date, Energy_Saved from EnergySaving", con);
            adapt.Fill(ds);
            EnergyChart.DataSource = ds;
            EnergyChart.DataBind();
            //set the member of the chart data source used to data bind to the X-values of the series  
            EnergyChart.Series["Energy Saved (watts)"].XValueMember = "Date";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            EnergyChart.Series["Energy Saved (watts)"].YValueMembers = "Energy_Saved";

            con.Close();
        }

        private void fillMoneyChart()
        {
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Date, Money_Saved from EnergySaving", con);
            adapt.Fill(ds);
            MoneyChart.DataSource = ds;
            MoneyChart.DataBind();
            //set the member of the chart data source used to data bind to the X-values of the series  
            MoneyChart.Series["Money Saved ($)"].XValueMember = "Date";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            MoneyChart.Series["Money Saved ($)"].YValueMembers = "Money_Saved";
            con.Close();
        }

        private void btnDisplayOverview_Click(object sender, EventArgs e)
        {
            OverviewPanel.Visible = true;
            LogsPanel.Visible = false;
            LightPanel.Visible = false;
            RFIDPanel.Visible = false;
            MotionPanel.Visible = false;
            WaterPanel.Visible = false;
        }

        private void btnRefreshLightEnergy_Click(object sender, EventArgs e)
        {
            fillLightChart();
            fillEnergyChart();
            fillMoneyChart();
        }
    }
}
