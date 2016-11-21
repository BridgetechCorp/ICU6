using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;


namespace ICU6_SerialTester
{
    public partial class Form1 : Form
    {
        delegate void SetTextCallback( string str );

        public void SetText( string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d,new object[] { text });
            }
            else
            {               
                richTextBox1.Text += text;               
            }
        }





        public Form1()
        {
            InitializeComponent();
        }


        // increment the correct sensor number
        void HandleSensorUpdate(int sensorNum, int SensorVal)
        {
            while (SensorVal > 0)
            {
                SensorVal--;
                switch (sensorNum)
                {
                    case 1:
                        AddStringToTextBox("[Sensor 1 IN]");
                        sensor1.IncrementInCount();
                        break;
                    case 2:
                        AddStringToTextBox("[Sensor 1 OUT]");
                        sensor1.IncrementOutCount();
                        break;
                    case 3:
                        AddStringToTextBox("[Sensor 2 IN]");
                        sensor2.IncrementInCount();
                        break;
                    case 4:
                        AddStringToTextBox("[Sensor 2 OUT]");
                        sensor2.IncrementOutCount();
                        break;
                    case 5:
                        AddStringToTextBox("[Sensor 3 IN]");
                        sensor3.IncrementInCount();
                        break;
                    case 6:
                        AddStringToTextBox("[Sensor 3 OUT]");
                        sensor3.IncrementOutCount();
                        break;
                    case 7:
                        AddStringToTextBox("[Sensor 4 IN]");
                        sensor4.IncrementInCount();
                        break;
                    case 8:
                        AddStringToTextBox("[Sensor 4 OUT]");
                        sensor4.IncrementOutCount();
                        break;
                    case 9:
                        AddStringToTextBox("[Sensor 5 IN]");
                        sensor5.IncrementInCount();
                        break;
                    case 10:
                        AddStringToTextBox("[Sensor 5 OUT]");
                        sensor5.IncrementOutCount();
                        break;
                    case 11:
                        AddStringToTextBox("[Sensor 6 IN]");
                        sensor6.IncrementInCount();
                        break;
                    case 12:
                        AddStringToTextBox("[Sensor 6 OUT]");
                        sensor6.IncrementOutCount();
                        break;
                }
            }
        }

        void SetNumberSensors(int val)
        {
            SetText(" "+val.ToString() + " Sensors Found");
           // Console.WriteLine(val.ToString()+" Sensors Found"); // richTextBox1.Text
        }

        void SetDebugModeOn( )
        {
            SetText("Debug Mode ON");
            //Console.WriteLine("Debug Mode ON"); // richTextBox1.Text
        }

        void SetErrorCode(int val)
        {
            SetText(val.ToString() + " Error Code");
            //Console.WriteLine(val.ToString() + " Error Code"); // richTextBox1.Text
        }

        void AddStringToTextBox( string str )
        {
            SetText(str);
            //Console.WriteLine(str); // richTextBox1.Text
        }


        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Task.Delay(200);
            int state = 0;
            int sensorNum = 0;
            int sensorVal = 0;

            int len = sp.BytesToRead;

            for (int i=0;i<len;i++)
            {
                int val = sp.ReadByte();
               
                AddStringToTextBox("[0x"+val.ToString("X")+"]");

                switch (state)
                {
                    case 0:
                        if (val == 0xC5) state = 1;
                        break;
                    case 1:
                        if (val == 0xFF) state = 2; else state = 0;
                        break;
                    case 2:
                        if (val == 0xBF)
                            state = 3;
                        else if (val == 0xC5)
                            state = 1;
                        else if (val == 0xDD)
                            state = 5;
                        else
                            state = 0;
                        break;
                    case 3:
                        sensorNum = val; 
                        state = 4;
                        break;
                    case 4:
                        sensorVal = val;
                        if (sensorVal > 0)
                            HandleSensorUpdate(sensorNum, sensorVal);
                        state = 2;
                        break;
                    case 5: // DEBUG case
                        if (val == 0xB0)  // bootup
                            state = 6;
                        else if (val == 0xBE) // hearbeat response
                            state = 7;
                        else if (val == 0xDE) // debug mode on response
                            state = 8;
                        else if (val == 0xEE) // error message
                            state = 9;
                        break;
                    case 6: // handle bootup message
                        switch(val)
                        {
                            case 0: // software reset
                                AddStringToTextBox(" Software Reset");
                                break;
                            case 1: // brownout
                                AddStringToTextBox(" BROWNOUT");
                                break;
                            case 2: // watchdog timeout
                                AddStringToTextBox(" Watchdog Timeout");
                                break;
                            case 3: // power on reset
                                AddStringToTextBox(" Power On Reset");
                                break;
                            case 4: // MCLR reset
                                AddStringToTextBox(" MCLR Reset");
                                break;
                            default: // unknown
                                AddStringToTextBox(" Unknown Reset");
                                state = 0;
                                break;
                        }                        
                        break;
                    case 7: // hearbeat response
                        SetNumberSensors(val);
                        state = 0;
                        break;
                    case 8: // debug mode on response
                        SetDebugModeOn();
                        state = 0;
                        break;
                    case 9: // error message
                        SetErrorCode(val);
                        state = 0;
                        break;
                    default:
                        state = 0;
                        break;
                }               
            }
            AddStringToTextBox("\n");
        }

        private void serialPort1_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sensor1.SetSensorNumber(1);
            sensor2.SetSensorNumber(2);
            sensor3.SetSensorNumber(3);
            sensor4.SetSensorNumber(4);
            sensor5.SetSensorNumber(5);
            sensor6.SetSensorNumber(6);

            sensor1.Reset();
            sensor2.Reset();
            sensor3.Reset();
            sensor4.Reset();
            sensor5.Reset();
            sensor6.Reset();




            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();                      

            // Display each port name to the console.
            foreach (string port in ports)
            {
                this.comboBox1.Items.Add(port);               
            }
           // this.comboBox1.Items.Add("COM1");
           // this.comboBox1.Items.Add("COM7");

            if (this.comboBox1.Items.Count > 0)
                this.comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();

            try
            {
                this.serialPort1.PortName = this.comboBox1.SelectedItem.ToString();
                this.serialPort1.BaudRate = 115200;
                this.serialPort1.StopBits = StopBits.One;
                this.serialPort1.Parity = Parity.None;
                this.serialPort1.DataBits = 8;
                this.serialPort1.Handshake = Handshake.None;
                this.serialPort1.ReadTimeout = 500;
                this.serialPort1.WriteTimeout = 500;
                this.serialPort1.Open();
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
                return;
            }
            label1.Text = this.serialPort1.PortName.ToString() + " Is Open!";

            // send a status message
            byte[] buff = { 0xF2 };
            this.serialPort1.Write(buff, 0, 1);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            byte[] buff = { 0xF1 };
            this.serialPort1.Write(buff, 0, 1);

            sensor1.Reset();
            sensor2.Reset();
            sensor3.Reset();
            sensor4.Reset();
            sensor5.Reset();
            sensor6.Reset();            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buff = { 0xF2 };
            this.serialPort1.Write(buff, 0, 1);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }    
    }
}
