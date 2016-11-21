namespace ICU6_SerialTester
{
    partial class Sensor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNAME = new System.Windows.Forms.Label();
            this.labelIN = new System.Windows.Forms.Label();
            this.labelOUT = new System.Windows.Forms.Label();
            this.pictureBoxIN = new System.Windows.Forms.PictureBox();
            this.pictureBoxOUT = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOUT)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "IN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "OUT";
            // 
            // labelNAME
            // 
            this.labelNAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNAME.AutoSize = true;
            this.labelNAME.Location = new System.Drawing.Point(8, 3);
            this.labelNAME.Name = "labelNAME";
            this.labelNAME.Size = new System.Drawing.Size(157, 32);
            this.labelNAME.TabIndex = 2;
            this.labelNAME.Text = "SENSOR #";
            // 
            // labelIN
            // 
            this.labelIN.AutoSize = true;
            this.labelIN.BackColor = System.Drawing.SystemColors.Control;
            this.labelIN.Location = new System.Drawing.Point(6, 137);
            this.labelIN.Name = "labelIN";
            this.labelIN.Size = new System.Drawing.Size(24, 32);
            this.labelIN.TabIndex = 3;
            this.labelIN.Text = "-";
            this.labelIN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOUT
            // 
            this.labelOUT.AutoSize = true;
            this.labelOUT.Location = new System.Drawing.Point(90, 137);
            this.labelOUT.Name = "labelOUT";
            this.labelOUT.Size = new System.Drawing.Size(24, 32);
            this.labelOUT.TabIndex = 4;
            this.labelOUT.Text = "-";
            this.labelOUT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxIN
            // 
            this.pictureBoxIN.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBoxIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxIN.Location = new System.Drawing.Point(14, 70);
            this.pictureBoxIN.Name = "pictureBoxIN";
            this.pictureBoxIN.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxIN.TabIndex = 5;
            this.pictureBoxIN.TabStop = false;
            // 
            // pictureBoxOUT
            // 
            this.pictureBoxOUT.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBoxOUT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxOUT.Location = new System.Drawing.Point(101, 70);
            this.pictureBoxOUT.Name = "pictureBoxOUT";
            this.pictureBoxOUT.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxOUT.TabIndex = 6;
            this.pictureBoxOUT.TabStop = false;
            // 
            // Sensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBoxOUT);
            this.Controls.Add(this.pictureBoxIN);
            this.Controls.Add(this.labelOUT);
            this.Controls.Add(this.labelIN);
            this.Controls.Add(this.labelNAME);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Sensor";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(184, 200);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOUT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelNAME;
        private System.Windows.Forms.Label labelIN;
        private System.Windows.Forms.Label labelOUT;
        private System.Windows.Forms.PictureBox pictureBoxIN;
        private System.Windows.Forms.PictureBox pictureBoxOUT;
    }
}
