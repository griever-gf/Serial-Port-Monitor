using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SerialConnectionUtils;

namespace SerialPortMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExceptionLogger logger = new ExceptionLogger();
            logger.AddLogger(new TextFileLogger());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
