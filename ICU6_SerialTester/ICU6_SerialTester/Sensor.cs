using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICU6_SerialTester
{
    public partial class Sensor : UserControl
    {
        public int InCount = 0;
        public int OutCount = 0;

        delegate void SetTextCallback();


        public Sensor()
        {
            InitializeComponent();
        }

        public void Reset( )
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelIN.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Reset);
                this.Invoke(d);
            }
            else
            {
                InCount=0;
                OutCount = 0;
                labelIN.Text = InCount.ToString();
                labelOUT.Text = OutCount.ToString();
                pictureBoxIN.BackColor = Color.LightGray;
                pictureBoxOUT.BackColor = Color.LightGray;               
            }
        }

        public void SetSensorNumber( int num )
        {
            labelNAME.Text = "SENSOR #" + num.ToString();
        }

        public void IncrementInCount()
        {           
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelIN.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(IncrementInCount);
                this.Invoke(d); 
            }
            else
            {
                InCount++;
                labelIN.Text = InCount.ToString();
                pictureBoxIN.BackColor = Color.Green;
                //timer1.Enabled = true;
            }
        }

        public void IncrementOutCount()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelOUT.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(IncrementOutCount);
                this.Invoke(d);
            }
            else
            {
                OutCount++;
                labelOUT.Text = OutCount.ToString();
                pictureBoxOUT.BackColor = Color.Green;
               // timer1.Enabled = true;
            }
        }

        
    }
}
