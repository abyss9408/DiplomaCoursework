using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using System.Diagnostics;
using System.Threading.Tasks;
using GrovePi;
using GrovePi.Sensors;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace SmartHome
{
    public sealed class StartupTask : IBackgroundTask
    {
        // State Machine variables to control different mode of operation
        /*const int MODE_SENDLIGHT = 1;
        const int MODE_SENDRFID = 2;
        const int MODE_SENDMOTION = 3;
        const int MODE_SENDWATER = 4;
        static int curMode; 
        static int lightSensorMode;*/

        private System.Threading.Semaphore sm = new System.Threading.Semaphore(1, 1);

        // for data comms
        DataComms dataComms;
        // This is used to check for data coming in from Winform
        string strDataReceived = "";

        private static SerialComms uartComms;
        private static string strRfidDetected = ""; //used to check for RFID
        private static string prevStrRfidDetected = "";

        //We shall use A1 for Light Sensor, A2 for Water Sensor, D2 for Motion Sensor, D3 for Buzzer, D5 for Red LED and D6 for Green LED
        Pin lightPin = Pin.AnalogPin1;
        Pin waterPin = Pin.AnalogPin2;
        Pin motionPin = Pin.DigitalPin2;
        Pin buzzerPin = Pin.DigitalPin3;
        ILed ledRed = DeviceFactory.Build.Led(Pin.DigitalPin5);
        ILed ledGreen = DeviceFactory.Build.Led(Pin.DigitalPin6);

        // 1023: completely dry, more water: value will drop
        int moistureAdcValue = 1023;

        private int sensorMoistureAdcValue;
        private int motionDetected;

        int lightAdcValue;

        private void Sleep(int NoOfMs)
        {
            Task.Delay(NoOfMs).Wait();
        }

        private int getMoisture()
        {
            int adcValue;

            sm.WaitOne();
            adcValue = DeviceFactory.Build.GrovePi().AnalogRead(waterPin);
            sm.Release();
            if (adcValue <= 1023)
                moistureAdcValue = adcValue;
            return moistureAdcValue;
        }

        private int GetLightValue(Pin pin)
        {
            sm.WaitOne();
            int value = DeviceFactory.Build.GrovePi().AnalogRead(pin);
            sm.Release();
            return value;
        }

        private async void startMotionMonitoring()
        {
            await Task.Delay(100);
            while (true)
            {
                Sleep(100);
                sm.WaitOne();
                string motionState = DeviceFactory.Build.GrovePi().DigitalRead(motionPin).ToString();
                sm.Release();
                if (motionState == "1")
                {
                    motionDetected = 1;
                    Task.Delay(300).Wait();
                }
                else
                {
                    motionDetected = 0;
                    Task.Delay(300).Wait();
                }
            }
        }

        // this method is automatically called when data comes from Winform
        public void commsDataReceive(string dataReceived)
        {
            // You can use strDataReceived anywhere in your codes to
            // check for any data coming in from Winform
            strDataReceived = dataReceived;
            Debug.WriteLine("Data Received : " + strDataReceived);
        }

        // use this method to send data out to Winforms
        private void sendDataToWindows(string strDataOut)
        {
            try
            {
                dataComms.sendData(strDataOut);
                Debug.WriteLine("Sending Msg : " + strDataOut);
            }
            catch (Exception)
            {
                Debug.WriteLine("ERROR. Did you forget to initcomms()?");
            }
        }

        // This is to setup the comms for data transfer with Winforms
        private void initComms()
        {
            dataComms = new DataComms();
            dataComms.dataReceiveEvent += new DataComms.DataReceivedDelegate(commsDataReceive);
        }

        private void handleModeSendLight()
        {
            // 1. Define Behaviour in this mode



            //if (prevLightDark != lightDark) // send only when change of status
            // send data to windows form every 2s
            Sleep(2000);
            sendDataToWindows("LIGHT=" + lightAdcValue);
        }

        private void handleModeSendRFID()
        {
            // 1. Define Behaviour in this mode
            if (!strRfidDetected.Equals("") && strRfidDetected != prevStrRfidDetected) // send only when change of status
                sendDataToWindows("RFID=" + strRfidDetected);

            prevStrRfidDetected = strRfidDetected;
            strRfidDetected = ""; // clear after processing

        }

        private void handleModeSendMotion()
        {
            // 1. Define Behaviour in this mode
            // send data every 2s
            Sleep(2000);
            sendDataToWindows("MOTIONPRESENT=" + motionDetected);            

        }

        private void handleModeSendWater()
        {
            // send data every 2s
            Sleep(2000);
            sendDataToWindows("MOISTURE=" + moistureAdcValue);

        }

        //this method is automatically called when there is card detected
        static void UartDataHandler(object sender, SerialComms.UartEventArgs e)
        {
            //strRfidDetected can be used anywhere in the program to check
            //for card detected
            strRfidDetected = e.data;
            Debug.WriteLine("Card Detected : " + strRfidDetected);
        }

        //Must call this to initalize the Serial Comms
        private void StartUart()
        {
            uartComms = new SerialComms();
            uartComms.UartEvent += new SerialComms.UartEventDelegate(UartDataHandler);
        }

        //This is to set tone of the buzzer. val = 0 is to off buzzer
        private void activateBuzzer(Pin pin, byte val)
        {
            sm.WaitOne();
            DeviceFactory.Build.GrovePi().AnalogWrite(pin, val);
            sm.Release();
        }

        // This is created to change LED status to On or Off
        private void ChangeLEDState(ILed led, SensorStatus targetState)
        {
            sm.WaitOne();
            led.ChangeState(targetState);
            sm.Release();
        }

        private void accessDeniedBuzzerSound()
        {
            activateBuzzer(buzzerPin, 60);
            Sleep(80);
            activateBuzzer(buzzerPin, 60);
            Sleep(80);
            activateBuzzer(buzzerPin, 60);
            Sleep(80);
            activateBuzzer(buzzerPin, 60);
            Sleep(80);
            activateBuzzer(buzzerPin, 0);
        }

        private void accessGrantedBuzzerSound()
        {
            activateBuzzer(buzzerPin, 60);
            Sleep(80);
            activateBuzzer(buzzerPin, 120);
            Sleep(80);
            activateBuzzer(buzzerPin, 60);
            Sleep(80);
            activateBuzzer(buzzerPin, 120);
            Sleep(80);
            activateBuzzer(buzzerPin, 0);
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //

            initComms();

            //Must call this to initalize the Serial Comms
            StartUart();

            startMotionMonitoring();         

            while (true)
            {
                Sleep(200);

                lightAdcValue = GetLightValue(lightPin);
                sensorMoistureAdcValue = getMoisture();
                Debug.WriteLine("Light ADC: " + lightAdcValue);               
                Debug.WriteLine("Moisture ADC: " + sensorMoistureAdcValue);
                if (!strRfidDetected.Equals(""))//this is true for any card detected
                {
                    Debug.WriteLine("One card is detected.");
                    if (strDataReceived.Equals("VALIDRFID")) //"light = 200"
                    {
                        ChangeLEDState(ledGreen, SensorStatus.On);
                        accessGrantedBuzzerSound();
                        ChangeLEDState(ledGreen, SensorStatus.Off);
                        Debug.WriteLine("Access granted: Welcome");
                    }
                    else if (strDataReceived.Equals("INVALIDRFID"))
                    {
                        ChangeLEDState(ledRed, SensorStatus.On);
                        accessDeniedBuzzerSound();
                        ChangeLEDState(ledRed, SensorStatus.Off);
                        Debug.WriteLine("Access denied: You are not authorized");
                    }
                    strDataReceived = "";

                }

                // state machine
                    handleModeSendLight();
                    handleModeSendRFID();
                    handleModeSendMotion();
                    handleModeSendWater();


                /*if (motionDetected == "Yes" && lightAdcValue <= 500)
                {
                    motionDetected = "No";
                    Debug.WriteLine("Found Movement and it's dark: Turning on lights");
                }*/
            }
        }
    }
}
