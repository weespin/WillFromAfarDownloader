using System;
using System.Windows.Forms;
namespace AcapellaDownloader
{
	static class Program
    {
	    [STAThread]
        static void Main()
        {
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
