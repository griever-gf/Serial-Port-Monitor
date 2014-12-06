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
        private ByteQueue Queue;

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
                Queue = new ByteQueue();
                port.DataReceived += DataReceived;
                EnableGUI(true);
                AddLog("Serial connection opened (" + spCOM.PortName + ")", Color.Green);
                tmrSendASCIIString.Start();
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, Color.Red);
            }
        }

        void EnableGUI(bool IsConnected)
        {
            bConnect.Enabled = !IsConnected;
            bDisconnect.Enabled = bSendASCIIString.Enabled = bSendInt32.Enabled = IsConnected;
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            CloseSerialPort(ref spCOM);
        }

        void CloseSerialPort(ref ExSerialPort port)
        {
            tmrSendASCIIString.Stop();
            Queue.Dispose();
            Queue = null;
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
                port.DataReceived -= DataReceived;
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

        #region Logging Textbox
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
            if (rtbLog.Lines.Length > 150)
            {
                rtbLog.Select(0, rtbLog.GetFirstCharIndexFromLine(rtbLog.Lines.Length - 150));
                rtbLog.SelectedText = "";
            }
            rtbLog.SelectionStart = rtbLog.Text.Length;

            rtbLog.SelectionColor = Color;
            if (!(rtbLog.Text.EndsWith("\n") || rtbLog.Text.Length == 0)) rtbLog.AppendText("\n");

            rtbLog.SelectionColor = Color;
            rtbLog.AppendText("[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "]: " + Text + Environment.NewLine);

            rtbLog.ScrollToCaret();
        }

        public void AddLogWithoutExtra(string Text, Color Color)
        {
            if (this.InvokeRequired)
            {
                AddLogDelegate Delegate = new AddLogDelegate(AddLogWithoutExtraSub);
                this.BeginInvoke(Delegate, new object[] { Text, Color });
            }
            else
                AddLogWithoutExtraSub(Text, Color);
        }

        private void AddLogWithoutExtraSub(string Text, Color Color)
        {
            if (rtbLog.Lines.Length > 150)
            {
                rtbLog.Select(0, rtbLog.GetFirstCharIndexFromLine(rtbLog.Lines.Length - 150));
                rtbLog.SelectedText = "";
            }

            String[] SplittedText = Text.Split(new char[] { '\n' });
            for (int i = 0; i < SplittedText.Length; i++)
            {
                rtbLog.SelectionStart = rtbLog.Text.Length;
                rtbLog.SelectionColor = Color;
                if (i != SplittedText.Length - 1) SplittedText[i] += "\n";
                rtbLog.AppendText(SplittedText[i]);
                rtbLog.ScrollToCaret();
            }
        }
        #endregion

        #region Send/ReceiveData

        private void bSendASCIIString_Click(object sender, EventArgs e)
        {
            spCOM.Write(Encoding.ASCII.GetBytes(tbASCIIString.Text), 0, tbASCIIString.Text.Length);
        }

        private void bSendInt32_Click(object sender, EventArgs e)
        {
            spCOM.Write(BitConverter.GetBytes((int)nudInt32Value.Value), 0, 4);
        }

        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] Buffer = new byte[1024];
            try
            {
                int Len = spCOM.Read(Buffer, 0, Buffer.Length);
                ProcessData(Buffer, Len);
            }
            catch (Exception ex)
            {
                AddLog("Error: " + ex.Message, Color.Red);
                spCOM.Close();
            }
        }

        private delegate void ProcessDataDelegate(byte[] Data, int Length);

        public void ProcessData(byte[] Data, int Length)
        {
            if (this.InvokeRequired)
            {
                ProcessDataDelegate Delegate = new ProcessDataDelegate(ProcessDataSub);
                this.Invoke(Delegate, new object[] { Data, Length });
            }
            else
                ProcessDataSub(Data, Length);
        }

        private void ProcessDataSub(byte[] Data, int Length)
        {
            try
            {
                String tmp_string = Encoding.ASCII.GetString(Data, 0, Length);
                AddLog("Incoming ASCII: " + Environment.NewLine + tmp_string, Color.Green);
                AddLog("Incoming RAW: " + Environment.NewLine + BitConverter.ToString(Data, 0, Length), Color.Green);
                //AddLogWithoutExtra(tmp_string, Color.Green);

                Queue.Push(Data, Length);
                String QueueString = Encoding.ASCII.GetString(Queue.Peek(Queue.Length()));
                int Index = 0, idx; ;

                while (Index < Queue.Length())
                {
                    //extracting packet, increasing Index
                    //temp
                    Index = Queue.Length();
                }
                Queue.Remove(Index);
            }
            catch (Exception E)
            {
                AddLog("Response process error: " + E.Message, Color.Red);
            }
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

        #region Debug Functions
        private int example_idx = 0;
        private void tmrSendASCIIString_Tick(object sender, EventArgs e)
        {
            String[] Examples = new String[]{"BEGIN SCAN GROUP 5   19 NOV 14  01:52:01" + Environment.NewLine +
            "FROST-BIOTROL" + Environment.NewLine +
            "    " + Environment.NewLine +
            "C 100                    26.1 C         " + Environment.NewLine +
            "C 101                     2.5 C         " + Environment.NewLine +
            "C 102                     2.5 C         " + Environment.NewLine +
            "C 103                     5.1 C         " + Environment.NewLine +
            "C 122                     0.8 C         " + Environment.NewLine +
            "C 1488    ZIEG HIEL         " + Environment.NewLine +
            "C49    OPENTC         " + Environment.NewLine +
            "C 50    OVER RANGE         " + Environment.NewLine +
            "C 123                     4.3 C         " + Environment.NewLine +
            "C 124                     4.9 C         " + Environment.NewLine +
            "C 125                 -   0.2 C         " + Environment.NewLine +
            "C 126                 -   0.2 C         " + Environment.NewLine +
            "C 127                -  13.3 C         " + Environment.NewLine +
            "C 128                     3.5 C         " + Environment.NewLine +
            "C 129                     0.0 C         " + Environment.NewLine +
            "C 130                     0.1 C         " + Environment.NewLine +
            "C 131               -122.9 C         " + Environment.NewLine +
            "C 132                     3.1 C         " + Environment.NewLine +
            "C 133                     1.8          " + Environment.NewLine +
            "C 147                     4.9          " + Environment.NewLine +
            "C 148                     5.2          " + Environment.NewLine +
            "C 149                     1.1 C         " + Environment.NewLine +
            "C 150                     0.5 C         " + Environment.NewLine +
            "C 151                     3.6 C         " + Environment.NewLine +
            "C 152                     2.5 C         " + Environment.NewLine +
            "C 153                     4.6 C         " + Environment.NewLine +
            "C 154                     6.3 C         " + Environment.NewLine +
            "C 190                  2.6159 V         " + Environment.NewLine +
            "C 195 MASSA1             0.9964 DC        " + Environment.NewLine +
            "C 198 SUCTION              1.568 VDC       " + Environment.NewLine +
            "C 199 DISCHARGE        1.5466 VDC       " + Environment.NewLine +
            "C1001                                    58 RH        " + Environment.NewLine +
            "C1002 SUCTION                 305 KPA       " + Environment.NewLine +
            "C1003 DISCHARGE             955 KPA       " + Environment.NewLine +
            "C1004 MASS FLOW      0.0000 KG/S      " + Environment.NewLine +
            "C 155                    23.6 C         " + Environment.NewLine +
            "C 156                    18.4 C         " + Environment.NewLine +
            "C666    XYNTA         " + Environment.NewLine +
            "    " + Environment.NewLine +
            "END SCAN GROUP 5     19 NOV 14  01:52:07" + Environment.NewLine +
            "    ",

            "BEGIN SCAN GROUP 5   04 DEC 14  17:16:01" + Environment.NewLine +
            "TK" + Environment.NewLine +
            "" + Environment.NewLine +
            "C   1 EXTERNAL TEMP       9.0 DEG C     " + Environment.NewLine +
            "C   2 EXTERNAL TEMP       8.9 DEG C     " + Environment.NewLine +
            "C   3 EXTERNAL TEMP       8.9 DEG C     " + Environment.NewLine +
            "C   4 EXTERNAL TEMP       9.0 DEG C     " + Environment.NewLine +
            "C   5 EXTERNAL TEMP       9.1 DEG C     " + Environment.NewLine +
            "C   6 EXTERNAL TEMP       9.2 DEG C     " + Environment.NewLine +
            "C   7 EXTERNAL TEMP       9.2 DEG C     " + Environment.NewLine +
            "C   8 EXTERNAL TEMP       9.3 DEG C     " + Environment.NewLine +
            "C   9 EXTERNAL TEMP       9.2 DEG C     " + Environment.NewLine +
            "C  10 EXTERNAL TEMP       9.1 DEG C     " + Environment.NewLine +
            "C  11 EXTERNAL TEMP       9.2 DEG C     " + Environment.NewLine +
            "C  12 EXTERNAL TEMP       9.1 DEG C     " + Environment.NewLine +
            "C  13                     9.3 C         " + Environment.NewLine +
            "C  14                     9.3 C         " + Environment.NewLine +
            "C  15                     9.2 C         " + Environment.NewLine +
            "C  16                     9.2 C         " + Environment.NewLine +
            "C  17                     9.2 C         " + Environment.NewLine +
            "C  18                     9.2 C         " + Environment.NewLine +
            "C  19                     9.3 C         " + Environment.NewLine +
            "C  20                     9.3 C         " + Environment.NewLine +
            "C  21                     9.3 C         " + Environment.NewLine +
            "C  22                     9.3 C         " + Environment.NewLine +
            "C  23                     9.3 C         " + Environment.NewLine +
            "C  24                     9.3 C         " + Environment.NewLine +
            "C  25                     9.4 C         " + Environment.NewLine +
            "C  26                     9.3 C         " + Environment.NewLine +
            "C  27                     9.4 C         " + Environment.NewLine +
            "C  28                     9.4 C         " + Environment.NewLine +
            "C  29                     9.4 C         " + Environment.NewLine +
            "C  30                     9.4 C         " + Environment.NewLine +
            "C  31                     9.5 C         " + Environment.NewLine +
            "C  32                     9.3 C         " + Environment.NewLine +
            "C  33                     9.4 C         " + Environment.NewLine +
            "C  34                     9.3 C         " + Environment.NewLine +
            "C  35                     9.3 C         " + Environment.NewLine +
            "C  36                     9.3 C         " + Environment.NewLine +
            "C  37                     9.4 C         " + Environment.NewLine +
            "C  38                     9.5 C         " + Environment.NewLine +
            "C  39                     9.5 C         " + Environment.NewLine +
            "C  40                     9.5 C         " + Environment.NewLine +
            "C  49                     9.2 C         " + Environment.NewLine +
            "C  50                     9.2 C         " + Environment.NewLine +
            "C  51                     9.3 C         " + Environment.NewLine +
            "C  52                     9.2 C         " + Environment.NewLine +
            "C  53                     9.4 C         " + Environment.NewLine +
            "C  54                     9.6 C         " + Environment.NewLine +
            "C  55                     9.7 C         " + Environment.NewLine +
            "C  56                     9.9 C         " + Environment.NewLine +
            "C  72                  0.0078 V         " + Environment.NewLine +
            "C  74 SUCTION           31.32 MV        " + Environment.NewLine +
            "C   0 DISCHARGE         11.81 MV        " + Environment.NewLine +
            "C1000 MAX EXT             9.3 C         " + Environment.NewLine +
            "C1001 MIN EXT             8.9 C         " + Environment.NewLine +
            "C1002 MEAN EXT            9.1 C         " + Environment.NewLine +
            "C1003 MAX INT             9.3 C         " + Environment.NewLine +
            "C1004 MIN INT             9.2 C         " + Environment.NewLine +
            "C1005 MEAN INT            9.3 C         " + Environment.NewLine +
            "C1006 EXT SPREAD          0.3 C         " + Environment.NewLine +
            "C1007 INT SPREAD          0.1 C         " + Environment.NewLine +
            "C1008 DELTA T         -   0.2 K         " + Environment.NewLine +
            "C1009 MWT                 9.2 C         " + Environment.NewLine +
            "C1010 SUCTION             190 KPA       " + Environment.NewLine +
            "C1011 DISCHARGE           202 KPA       " + Environment.NewLine +
            "C1012 SUCTION              28 PSI       " + Environment.NewLine +
            "C1013 DISCHARGE            29 PSI       " + Environment.NewLine +
            "C  48                 -0.2637 MV        " + Environment.NewLine +
            "C1014                 -    13 KPA       " + Environment.NewLine +
            "C  57                     9.2 C         " + Environment.NewLine +
            "C  58                     9.1 C         " + Environment.NewLine +
            "C  59                     9.2 C         " + Environment.NewLine +
            "C  60                     8.7 C         " + Environment.NewLine +
            "" + Environment.NewLine +
            "END SCAN GROUP 5     04 DEC 14  17:16:07" + Environment.NewLine +
            "    ",
            "            " + Environment.NewLine +
            "" + Environment.NewLine +
            "BEGIN SCAN GROUP 5   03 DEC 14  02:46:00" + Environment.NewLine +
            "TC5" + Environment.NewLine +
            "" + Environment.NewLine +
            "C 160                    17.6 C         " + Environment.NewLine +
            "C 161                    17.5 C         " + Environment.NewLine +
            "C 162                    18.7 C         " + Environment.NewLine +
            "C 163                         OPEN TC   " + Environment.NewLine +
            "C 164                         OPEN TC   " + Environment.NewLine +
            "C 165                         OPEN TC   " + Environment.NewLine +
            "C 166                    18.3 C         " + Environment.NewLine +
            "C 167                         OPEN TC   " + Environment.NewLine +
            "C 168                         OPEN TC   " + Environment.NewLine +
            "C 169                         OPEN TC   " + Environment.NewLine +
            "C 170                         OPEN TC   " + Environment.NewLine +
            "C 171                  .00000           " + Environment.NewLine +
            "C 172                  .00000           " + Environment.NewLine +
            "C 173                  .00000           " + Environment.NewLine +
            "C 174                  .00000           " + Environment.NewLine +
            "C 175                  .00000           " + Environment.NewLine +
            "C 176                  .00000           " + Environment.NewLine +
            "C 177                  .00000           " + Environment.NewLine +
            "C 178                  .00000           " + Environment.NewLine +
            "C 179                  .00000           " + Environment.NewLine +
            "C 180                  .00000           " + Environment.NewLine +
            "C 101                  .00000           " + Environment.NewLine +
            "C 102                  .00000           " + Environment.NewLine +
            "C 103                  .00000           " + Environment.NewLine +
            "C 104                  .00000           " + Environment.NewLine +
            "C 105                  .00000           " + Environment.NewLine +
            "C 106                  .00000           " + Environment.NewLine +
            "C 107                  .00000           " + Environment.NewLine +
            "C 108                  .00000           " + Environment.NewLine +
            "C 109                  .00000           " + Environment.NewLine +
            "C 110                  .00000           " + Environment.NewLine +
            "C 111                  .00000           " + Environment.NewLine +
            "C 112                  .00000           " + Environment.NewLine +
            "C 113                  .00000           " + Environment.NewLine +
            "C 114                  .00000           " + Environment.NewLine +
            "C 115                  .00000           " + Environment.NewLine +
            "C 116                  .00000           " + Environment.NewLine +
            "C 117                  .00000           " + Environment.NewLine +
            "C 118                  .00000           " + Environment.NewLine +
            "C 119                  .00000           " + Environment.NewLine +
            "C 120                  .00000           " + Environment.NewLine +
            "" + Environment.NewLine +
            "END SCAN GROUP 5     03 DEC 14  02:46:20" + Environment.NewLine +
            ""        };
            //AddLog(Example, Color.Green);
            byte[] bytesASCII = Encoding.ASCII.GetBytes(Examples[example_idx]);
            //byte[] bytesASCII = Encoding.ASCII.GetBytes(Examples[2]);
            example_idx = (example_idx + 1) % Examples.Length;
            spCOM.Write(bytesASCII, 0, bytesASCII.Length);
        }
        #endregion
    }
}
