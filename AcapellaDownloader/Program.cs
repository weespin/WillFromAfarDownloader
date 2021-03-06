﻿using System;
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
