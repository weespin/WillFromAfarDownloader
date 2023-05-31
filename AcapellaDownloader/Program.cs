using System;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AcapellaDownloader
{
	static class Program
    {
        public static bool bOldWindows = false;
	    [STAThread]
        static void Main()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            var OsVersion = Environment.OSVersion.Version;
            if (OsVersion.Major <= 6 && OsVersion.Minor <= 1)
            {
                bOldWindows = true;
            }
            if (Registry.CurrentUser.OpenSubKey("Software\\Wine", false) != null)
            {
                //Wine!
                bOldWindows = true;
            }
            string[] commandLineArgs = Environment.GetCommandLineArgs();
	        if (commandLineArgs.Length > 1)
	        {
		        CLI.PassCLI(commandLineArgs);
				return;
	        }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
