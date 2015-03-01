namespace SerialPortMonitor
{
    partial class MainForm
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
            this.bDisconnect = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tbASCIIString = new System.Windows.Forms.TextBox();
            this.bSendASCIIString = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bSendInt32 = new System.Windows.Forms.Button();
            this.nudInt32Value = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrSendASCIIString = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudInt32Value)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bDisconnect
            // 
            this.bDisconnect.Enabled = false;
            this.bDisconnect.Location = new System.Drawing.Point(146, 64);
            this.bDisconnect.Name = "bDisconnect";
            this.bDisconnect.Size = new System.Drawing.Size(100, 33);
            this.bDisconnect.TabIndex = 13;
            this.bDisconnect.Text = "Disconnect";
            this.bDisconnect.UseVisualStyleBackColor = true;
            this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(29, 64);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(100, 33);
            this.bConnect.TabIndex = 12;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(12, 166);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(695, 230);
            this.rtbLog.TabIndex = 10;
            this.rtbLog.Text = "";
            // 
            // tbASCIIString
            // 
            this.tbASCIIString.Location = new System.Drawing.Point(366, 54);
            this.tbASCIIString.Name = "tbASCIIString";
            this.tbASCIIString.Size = new System.Drawing.Size(200, 22);
            this.tbASCIIString.TabIndex = 14;
            // 
            // bSendASCIIString
            // 
            this.bSendASCIIString.Enabled = false;
            this.bSendASCIIString.Location = new System.Drawing.Point(588, 43);
            this.bSendASCIIString.Name = "bSendASCIIString";
            this.bSendASCIIString.Size = new System.Drawing.Size(100, 33);
            this.bSendASCIIString.TabIndex = 15;
            this.bSendASCIIString.Text = "Send";
            this.bSendASCIIString.UseVisualStyleBackColor = true;
            this.bSendASCIIString.Click += new System.EventHandler(this.bSendASCIIString_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(366, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "ASCII string";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(366, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Int32 Value";
            // 
            // bSendInt32
            // 
            this.bSendInt32.Enabled = false;
            this.bSendInt32.Location = new System.Drawing.Point(588, 102);
            this.bSendInt32.Name = "bSendInt32";
            this.bSendInt32.Size = new System.Drawing.Size(100, 33);
            this.bSendInt32.TabIndex = 19;
            this.bSendInt32.Text = "Send";
            this.bSendInt32.UseVisualStyleBackColor = true;
            this.bSendInt32.Click += new System.EventHandler(this.bSendInt32_Click);
            // 
            // nudInt32Value
            // 
            this.nudInt32Value.Location = new System.Drawing.Point(366, 113);
            this.nudInt32Value.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudInt32Value.Name = "nudInt32Value";
            this.nudInt32Value.Size = new System.Drawing.Size(120, 22);
            this.nudInt32Value.TabIndex = 21;
            this.nudInt32Value.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(719, 26);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogWindowToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(55, 22);
            this.connectionToolStripMenuItem.Text = "Tools";
            // 
            // clearLogWindowToolStripMenuItem
            // 
            this.clearLogWindowToolStripMenuItem.Name = "clearLogWindowToolStripMenuItem";
            this.clearLogWindowToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.clearLogWindowToolStripMenuItem.Text = "Clear Log Box";
            this.clearLogWindowToolStripMenuItem.Click += new System.EventHandler(this.clearLogWindowToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.settingsToolStripMenuItem.Text = "Connection Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tmrSendASCIIString
            // 
            this.tmrSendASCIIString.Interval = 1000;
            //this.tmrSendASCIIString.Tick += new System.EventHandler(this.tmrSendASCIIString_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 408);
            this.Controls.Add(this.nudInt32Value);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bSendInt32);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bSendASCIIString);
            this.Controls.Add(this.tbASCIIString);
            this.Controls.Add(this.bDisconnect);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Serial Port Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.nudInt32Value)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bDisconnect;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.TextBox tbASCIIString;
        private System.Windows.Forms.Button bSendASCIIString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bSendInt32;
        private System.Windows.Forms.NumericUpDown nudInt32Value;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogWindowToolStripMenuItem;
        private System.Windows.Forms.Timer tmrSendASCIIString;
    }
}

