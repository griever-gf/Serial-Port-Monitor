using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;
using SerialConnectionUtils;

namespace SerialPortMonitor
{
    public partial class ConnectionSettings : Form
    {
        public ConnectionSettings()
        {
            InitializeComponent();
            RefreshSerialPortsComboBoxes();
            LoadSerialPortSettings();
        }

        private void RefreshSerialPortsComboBoxes()
        {
            string[] port_names = SerialPort.GetPortNames();
            Array.Sort(port_names, StringComparer.InvariantCulture);
            cbPortName.Items.Clear();
            cbPortName.Items.AddRange(port_names);
            if (cbPortName.Items.Count > 0)
                cbPortName.SelectedIndex = 0;
        }

        private void bSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSerialPortSettings();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Save/Load serial port settings
        private void LoadSerialPortSettings()
        {

            int idx = cbPortName.Items.IndexOf(Properties.Settings.Default.PortName);
            if (idx != -1) cbPortName.SelectedIndex = idx;

            idx = cbRate.Items.IndexOf(Properties.Settings.Default.PortBaudRate.ToString());
            if (idx != -1) cbRate.SelectedIndex = idx;

            idx = cbDataBits.Items.IndexOf(Properties.Settings.Default.PortDataBits.ToString());
            if (idx != -1) cbDataBits.SelectedIndex = idx;

            idx = cbParity.Items.IndexOf(Properties.Settings.Default.PortParity.ToString());
            if (idx != -1) cbParity.SelectedIndex = idx;

            string searchStr = "x";
            switch (Properties.Settings.Default.PortStopBits)
            {
                case StopBits.None:
                    searchStr = "0";
                    break;
                case StopBits.One:
                    searchStr = "1";
                    break;
                case StopBits.OnePointFive:
                    searchStr = "1.5";
                    break;
                case StopBits.Two:
                    searchStr = "2";
                    break;
            }
            idx = cbStopBits.Items.IndexOf(searchStr);
            if (idx != -1) cbStopBits.SelectedIndex = idx;

            switch (Properties.Settings.Default.PortFlowControl)
            {
                case Handshake.None:
                    searchStr = "None";
                    break;
                case Handshake.RequestToSend:
                    searchStr = "RTS/CTS";
                    break;
                case Handshake.XOnXOff:
                    searchStr = "XON/XOFF";
                    break;
            }

            idx = cbFlowControl.Items.IndexOf(searchStr);
            if (idx != -1) cbFlowControl.SelectedIndex = idx;
        }

        private void SaveSerialPortSettings()
        {

            if (cbPortName.SelectedItem != null)
                Properties.Settings.Default.PortName = cbPortName.SelectedItem.ToString();

            Properties.Settings.Default.PortBaudRate = Convert.ToInt32(cbRate.SelectedItem);
            Properties.Settings.Default.PortDataBits = Convert.ToInt32(cbDataBits.SelectedItem);

            System.IO.Ports.Parity tmpP = Parity.None;
            switch (cbParity.SelectedItem.ToString())
            {
                case "None":
                    tmpP = Parity.None;
                    break;
                case "Even":
                    tmpP = Parity.Even;
                    break;
                case "Odd":
                    tmpP = Parity.Odd;
                    break;
            }
            Properties.Settings.Default.PortParity = tmpP;

            StopBits tmpSB = StopBits.One;
            switch (cbStopBits.SelectedItem.ToString())
            {
                case "0":
                    tmpSB = StopBits.None;
                    break;
                case "1":
                    tmpSB = StopBits.One;
                    break;
                case "1.5":
                    tmpSB = StopBits.OnePointFive;
                    break;
                case "2":
                    tmpSB = StopBits.Two;
                    break;
            }
            Properties.Settings.Default.PortStopBits = tmpSB;

            Handshake tmpHS = Handshake.None;
            switch (cbFlowControl.SelectedItem.ToString())
            {
                case "None":
                    tmpHS = Handshake.None;
                    break;
                case "RTS/CTS":
                    tmpHS = Handshake.RequestToSend;
                    break;
                case "XON/XOFF":
                    tmpHS = Handshake.XOnXOff;
                    break;
            }

            Properties.Settings.Default.PortFlowControl = tmpHS;

            Properties.Settings.Default.Save();
        }
        #endregion

        //USB Plug/unplug checks
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case PInvoke.WM_DEVICECHANGE:
                    int EventCode = m.WParam.ToInt32();
                    switch (EventCode)
                    {
                        case PInvoke.DBT_DEVNODES_CHANGED:
                            RefreshSerialPortsComboBoxes();
                            break;
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
        
        
        private void cbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPortName.SelectedIndex != -1)
            {
                string sInstanceName = string.Empty;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["Caption"].ToString().Contains(cbPortName.SelectedItem.ToString()))
                    {
                        lCaption.Text = "Caption: " + queryObj["Caption"];
                        lManufacturer.Text = "Manufacturer: " + queryObj["Manufacturer"];
                        lDeviceID.Text = "DeviceID: " + queryObj["DeviceID"];
                        lDescription.Text = "Description: " + queryObj["Description"];

                        foreach (PropertyData prop in queryObj.Properties)
                        {
                            //mf.AddLog(prop.Name + " " + prop.Value, Color.Green);
                        }
                        break;
                    }
                    lCaption.Text = "Caption: N/A";
                    lManufacturer.Text = "Manufacturer: N/A";
                    lDeviceID.Text = "DeviceID: N/A";
                    lDescription.Text = "Description: N/A";
                }
            }
        }
    }
}
