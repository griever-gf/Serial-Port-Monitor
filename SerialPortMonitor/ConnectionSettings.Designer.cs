namespace SerialPortMonitor
{
    partial class ConnectionSettings
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
            this.bCancel = new System.Windows.Forms.Button();
            this.bSaveSettings = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.lCaption = new System.Windows.Forms.Label();
            this.lManufacturer = new System.Windows.Forms.Label();
            this.lDeviceID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbFlowControl = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbRate = new System.Windows.Forms.ComboBox();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(518, 239);
            this.bCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 28);
            this.bCancel.TabIndex = 28;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bSaveSettings
            // 
            this.bSaveSettings.Location = new System.Drawing.Point(422, 239);
            this.bSaveSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bSaveSettings.Name = "bSaveSettings";
            this.bSaveSettings.Size = new System.Drawing.Size(75, 28);
            this.bSaveSettings.TabIndex = 27;
            this.bSaveSettings.Text = "Save";
            this.bSaveSettings.UseVisualStyleBackColor = true;
            this.bSaveSettings.Click += new System.EventHandler(this.bSaveSettings_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lDescription);
            this.groupBox2.Controls.Add(this.lCaption);
            this.groupBox2.Controls.Add(this.lManufacturer);
            this.groupBox2.Controls.Add(this.lDeviceID);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbFlowControl);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbStopBits);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cbParity);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbDataBits);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cbRate);
            this.groupBox2.Controls.Add(this.cbPortName);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(7, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(603, 206);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Serial Connection";
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(21, 101);
            this.lDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(79, 17);
            this.lDescription.TabIndex = 36;
            this.lDescription.Text = "Description";
            // 
            // lCaption
            // 
            this.lCaption.AutoSize = true;
            this.lCaption.Location = new System.Drawing.Point(21, 71);
            this.lCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lCaption.Name = "lCaption";
            this.lCaption.Size = new System.Drawing.Size(56, 17);
            this.lCaption.TabIndex = 35;
            this.lCaption.Text = "Caption";
            // 
            // lManufacturer
            // 
            this.lManufacturer.AutoSize = true;
            this.lManufacturer.Location = new System.Drawing.Point(21, 135);
            this.lManufacturer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lManufacturer.Name = "lManufacturer";
            this.lManufacturer.Size = new System.Drawing.Size(92, 17);
            this.lManufacturer.TabIndex = 34;
            this.lManufacturer.Text = "Manufacturer";
            // 
            // lDeviceID
            // 
            this.lDeviceID.AutoSize = true;
            this.lDeviceID.Location = new System.Drawing.Point(21, 167);
            this.lDeviceID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lDeviceID.Name = "lDeviceID";
            this.lDeviceID.Size = new System.Drawing.Size(64, 17);
            this.lDeviceID.TabIndex = 33;
            this.lDeviceID.Text = "DeviceID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(376, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 32;
            this.label5.Text = "Flow Control";
            // 
            // cbFlowControl
            // 
            this.cbFlowControl.FormattingEnabled = true;
            this.cbFlowControl.Items.AddRange(new object[] {
            "None",
            "RTS/CTS",
            "XON/XOFF"});
            this.cbFlowControl.Location = new System.Drawing.Point(473, 32);
            this.cbFlowControl.Margin = new System.Windows.Forms.Padding(4);
            this.cbFlowControl.Name = "cbFlowControl";
            this.cbFlowControl.Size = new System.Drawing.Size(112, 24);
            this.cbFlowControl.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(412, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 30;
            this.label7.Text = "Stop Bits";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Items.AddRange(new object[] {
            "0",
            "1",
            "1.5",
            "2"});
            this.cbStopBits.Location = new System.Drawing.Point(507, 135);
            this.cbStopBits.Margin = new System.Windows.Forms.Padding(4);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(79, 24);
            this.cbStopBits.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(411, 71);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 28;
            this.label8.Text = "Parity";
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cbParity.Location = new System.Drawing.Point(507, 66);
            this.cbParity.Margin = new System.Windows.Forms.Padding(4);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(77, 24);
            this.cbParity.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(411, 106);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "Data Bits";
            // 
            // cbDataBits
            // 
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBits.Location = new System.Drawing.Point(507, 102);
            this.cbDataBits.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(77, 24);
            this.cbDataBits.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(223, 36);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Rate";
            // 
            // cbRate
            // 
            this.cbRate.FormattingEnabled = true;
            this.cbRate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cbRate.Location = new System.Drawing.Point(271, 32);
            this.cbRate.Margin = new System.Windows.Forms.Padding(4);
            this.cbRate.Name = "cbRate";
            this.cbRate.Size = new System.Drawing.Size(80, 24);
            this.cbRate.TabIndex = 23;
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(103, 33);
            this.cbPortName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(87, 24);
            this.cbPortName.TabIndex = 21;
            this.cbPortName.SelectedIndexChanged += new System.EventHandler(this.cbPortName_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Port Name";
            // 
            // ConnectionSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 282);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bSaveSettings);
            this.Name = "ConnectionSettings";
            this.Text = "ConnectionSettings";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bSaveSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.Label lCaption;
        private System.Windows.Forms.Label lManufacturer;
        private System.Windows.Forms.Label lDeviceID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbFlowControl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbRate;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Label label6;
    }
}