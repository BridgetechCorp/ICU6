namespace ICU6_SerialTester
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxSaveToLogFile = new System.Windows.Forms.CheckBox();
            this.buttonSetFilename = new System.Windows.Forms.Button();
            this.sensor1 = new ICU6_SerialTester.Sensor();
            this.sensor2 = new ICU6_SerialTester.Sensor();
            this.sensor3 = new ICU6_SerialTester.Sensor();
            this.sensor4 = new ICU6_SerialTester.Sensor();
            this.sensor5 = new ICU6_SerialTester.Sensor();
            this.sensor6 = new ICU6_SerialTester.Sensor();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(27, 15);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(72, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM Port not open";
            // 
            // serialPort1
            // 
            this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort1_ErrorReceived);
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.sensor1);
            this.flowLayoutPanel1.Controls.Add(this.sensor2);
            this.flowLayoutPanel1.Controls.Add(this.sensor3);
            this.flowLayoutPanel1.Controls.Add(this.sensor4);
            this.flowLayoutPanel1.Controls.Add(this.sensor5);
            this.flowLayoutPanel1.Controls.Add(this.sensor6);
            this.flowLayoutPanel1.Controls.Add(this.richTextBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 57);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(750, 254);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richTextBox1.Location = new System.Drawing.Point(2, 126);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(734, 124);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(639, 15);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.buttonReset.MinimumSize = new System.Drawing.Size(69, 34);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(69, 34);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "RESET";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(559, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button1.MinimumSize = new System.Drawing.Size(50, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 34);
            this.button1.TabIndex = 8;
            this.button1.Text = "Status";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "data";
            this.saveFileDialog1.Filter = "\"txt files (*.txt)|*.txt|All files (*.*)|*.*\"; ";
            this.saveFileDialog1.InitialDirectory = "@\"C:\\\"; ";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(117, 326);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(397, 22);
            this.textBox1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Log File Name";
            // 
            // checkBoxSaveToLogFile
            // 
            this.checkBoxSaveToLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSaveToLogFile.AutoSize = true;
            this.checkBoxSaveToLogFile.Location = new System.Drawing.Point(639, 326);
            this.checkBoxSaveToLogFile.Name = "checkBoxSaveToLogFile";
            this.checkBoxSaveToLogFile.Size = new System.Drawing.Size(106, 21);
            this.checkBoxSaveToLogFile.TabIndex = 11;
            this.checkBoxSaveToLogFile.Text = "Save to Log";
            this.checkBoxSaveToLogFile.UseVisualStyleBackColor = true;
            // 
            // buttonSetFilename
            // 
            this.buttonSetFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetFilename.Location = new System.Drawing.Point(520, 318);
            this.buttonSetFilename.Name = "buttonSetFilename";
            this.buttonSetFilename.Size = new System.Drawing.Size(113, 38);
            this.buttonSetFilename.TabIndex = 12;
            this.buttonSetFilename.Text = "Set Filename";
            this.buttonSetFilename.UseCompatibleTextRendering = true;
            this.buttonSetFilename.UseVisualStyleBackColor = true;
            this.buttonSetFilename.Click += new System.EventHandler(this.buttonSetFilename_Click);
            // 
            // sensor1
            // 
            this.sensor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sensor1.Location = new System.Drawing.Point(2, 2);
            this.sensor1.Margin = new System.Windows.Forms.Padding(1);
            this.sensor1.Name = "sensor1";
            this.sensor1.Padding = new System.Windows.Forms.Padding(1);
            this.sensor1.Size = new System.Drawing.Size(121, 122);
            this.sensor1.TabIndex = 0;
            // 
            // sensor2
            // 
            this.sensor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sensor2.Location = new System.Drawing.Point(125, 2);
            this.sensor2.Margin = new System.Windows.Forms.Padding(1);
            this.sensor2.Name = "sensor2";
            this.sensor2.Padding = new System.Windows.Forms.Padding(1);
            this.sensor2.Size = new System.Drawing.Size(121, 122);
            this.sensor2.TabIndex = 1;
            // 
            // sensor3
            // 
            this.sensor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sensor3.Location = new System.Drawing.Point(248, 2);
            this.sensor3.Margin = new System.Windows.Forms.Padding(1);
            this.sensor3.Name = "sensor3";
            this.sensor3.Padding = new System.Windows.Forms.Padding(1);
            this.sensor3.Size = new System.Drawing.Size(121, 122);
            this.sensor3.TabIndex = 2;
            // 
            // sensor4
            // 
            this.sensor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sensor4.Location = new System.Drawing.Point(371, 2);
            this.sensor4.Margin = new System.Windows.Forms.Padding(1);
            this.sensor4.Name = "sensor4";
            this.sensor4.Padding = new System.Windows.Forms.Padding(1);
            this.sensor4.Size = new System.Drawing.Size(121, 122);
            this.sensor4.TabIndex = 3;
            // 
            // sensor5
            // 
            this.sensor5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sensor5.Location = new System.Drawing.Point(494, 2);
            this.sensor5.Margin = new System.Windows.Forms.Padding(1);
            this.sensor5.Name = "sensor5";
            this.sensor5.Padding = new System.Windows.Forms.Padding(1);
            this.sensor5.Size = new System.Drawing.Size(121, 122);
            this.sensor5.TabIndex = 4;
            // 
            // sensor6
            // 
            this.sensor6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sensor6.Location = new System.Drawing.Point(617, 2);
            this.sensor6.Margin = new System.Windows.Forms.Padding(1);
            this.sensor6.Name = "sensor6";
            this.sensor6.Padding = new System.Windows.Forms.Padding(1);
            this.sensor6.Size = new System.Drawing.Size(121, 122);
            this.sensor6.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 359);
            this.Controls.Add(this.buttonSetFilename);
            this.Controls.Add(this.checkBoxSaveToLogFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonReset);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.MinimumSize = new System.Drawing.Size(27, 316);
            this.Name = "Form1";
            this.Text = "ICU6 Serial Tester v1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Sensor sensor1;
        private Sensor sensor2;
        private Sensor sensor3;
        private Sensor sensor4;
        private Sensor sensor5;
        private Sensor sensor6;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxSaveToLogFile;
        private System.Windows.Forms.Button buttonSetFilename;
    }
}

