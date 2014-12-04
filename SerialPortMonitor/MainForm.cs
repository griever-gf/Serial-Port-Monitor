using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using SerialConnectionUtils;

namespace SerialPortMonitor
{
    public partial class MainForm : Form
    {
        ExSerialPort spCOM;

        public MainForm()
        {
            InitializeComponent();
            EnableGUI(false);
        }

        #region Connection Open/Close
        private void bConnect_Click(object sender, EventArgs e)
        {
            OpenSerialPort(ref spCOM);
        }

        void OpenSerialPort(ref ExSerialPort port)
        {
            string port_name = Properties.Settings.Default.PortName;
            try
            {
                SerialPortFixer.Execute(port_name);
            }
            catch { }
            port = new ExSerialPort(port_name);
            port.PortName = port_name;
            port.ReadTimeout = port.WriteTimeout = SerialPort.InfiniteTimeout;
            port.BaudRate = Properties.Settings.Default.PortBaudRate;
            port.Parity =   Properties.Settings.Default.PortParity;
            port.StopBits = Properties.Settings.Default.PortStopBits;
            port.DataBits = Properties.Settings.Default.PortDataBits;
            port.Handshake = Properties.Settings.Default.PortFlowControl;
            port.RtsEnable = false;
            try
            {
                port.Open();
                GC.SuppressFinalize(port.BaseStream);
                EnableGUI(true);
                AddLog("Serial connection opened (" + spCOM.PortName + ")", Color.Green);
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, Color.Red);
            }
        }

        void EnableGUI(bool IsConnected)
        {
            bConnect.Enabled = !IsConnected;
            bDisconnect.Enabled = IsConnected;
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            CloseSerialPort(ref spCOM);
        }

        void CloseSerialPort(ref ExSerialPort port)
        {
            if (port == null)
                return;
            bool IsPortExists = false;
            foreach (String pn in SerialPort.GetPortNames())
                if (pn == port.PortName)
                {
                    IsPortExists = true;
                    break;
                }
            if ((IsPortExists) && (port.IsOpen))
            {
                try
                {
                    port.BaseStream.Flush();
                    System.Threading.Thread.Sleep(100);
                    GC.ReRegisterForFinalize(port.BaseStream);
                    port.Close();
                }
                catch (Exception ex)
                {
                    AddLog(ex.Message, Color.Red);
                }
            }
            EnableGUI(false);
            AddLog("Serial connection closed.", Color.Green);
            port = null;
        }
        #endregion

        #region Logging
        private delegate void AddLogDelegate(string Text, Color Color);

        public void AddLog(string Text, Color Color)
        {
            if (this.InvokeRequired)
            {
                AddLogDelegate Delegate = new AddLogDelegate(AddLogSub);
                this.BeginInvoke(Delegate, new object[] { Text, Color });
            }
            else
                AddLogSub(Text, Color);
        }

        private void AddLogSub(string Text, Color Color)
        {
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.SelectionColor = Color;
            rtbLog.AppendText("[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "]: " + Text + Environment.NewLine);
            rtbLog.ScrollToCaret();
        }
        #endregion

        #region Send/ReceiveData
        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string DataString = spCOM.ReadExisting();
            AddLog("COM in: " + DataString + " (Raw: " + BitConverter.ToString(Encoding.ASCII.GetBytes(DataString)) + ")", Color.Green);
        }

        private void bSendASCIIString_Click(object sender, EventArgs e)
        {
            spCOM.Write(Encoding.ASCII.GetBytes(tbASCIIString.Text), 0, tbASCIIString.Text.Length);
        }

        private void bSendInt32_Click(object sender, EventArgs e)
        {
            spCOM.Write(BitConverter.GetBytes((int)nudInt32Value.Value), 0, 4);
        }
        #endregion

        #region Menu Items
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String aboutString = "Serial Port Monitor Software\n";
            aboutString += "Version " + Application.ProductVersion + Environment.NewLine;
            aboutString += "Build date: " + RetrieveLinkerTimestamp().ToString("MM/dd/yyyy hh:mm:ss");
            MessageBox.Show(aboutString, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionSettings cs = new ConnectionSettings();
            cs.ShowDialog();
        }

        private void clearLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }

        #endregion
    }
}
