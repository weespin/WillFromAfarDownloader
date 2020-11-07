using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace AcapellaDownloader
{

    static class Program
    {

	    [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	    private static extern IntPtr GetStdHandle(int nStdHandle);
	    [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	    private static extern int AllocConsole();
	    [DllImport("kernel32.dll", EntryPoint = "AttachConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	    private static extern int AttachConsole(int dwProcessId);
	    [DllImport("kernel32.dll", EntryPoint = "FreeConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	    private static extern bool FreeConsole();

		class CommandLineUtils
	    {
		    public CommandLineUtils(string dataIn,string[] keys)
		    {
				//Store keys
				foreach (var key in keys)
				{
					m_Result.Add(key, "");
				}
				
			   
			    foreach (var key in m_Result.Keys.ToList())
			    {
				    int index = dataIn.IndexOf(key);
				    if (index == -1)
				    {
					    continue;
				    }
				    int end = dataIn.Length;
					//Find next key
					foreach (var anotherkey in m_Result.Keys.Where(n=>n != key).ToList())
					{
						int anotherindex = dataIn.IndexOf(anotherkey);
						if (anotherindex > index)
						{
							end = Math.Min(end, anotherindex);
						}
					}
					
					string value = dataIn.Substring(index + key.Length , end - index - key.Length  );
					value = value.Trim();
					m_Result[key] = value;

			    }
		    }

		    public string GetValue(string key)
		    {
			    if (m_Result.ContainsKey(key))
			    {
				    return m_Result[key];
			    }

			    return "";
		    }


		 
			Dictionary<string, string> m_Result = new Dictionary<string, string>();

	    }
		
        [STAThread]
        static void Main()
        {
	        string[] CommandLineArgs = Environment.GetCommandLineArgs();

	        if (CommandLineArgs.Length > 1)
	        {
				//Cli mode
				if (AttachConsole(-1) == 0)
				{
					AllocConsole();
				}
				IntPtr stdHandle = GetStdHandle(-11);
				StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
				standardOutput.AutoFlush = true;
				Console.SetOut(standardOutput);
				//-t "sample text" -v voicename -o sample.mp3

				var arguments = CommandLineArgs.ToList();
				arguments.RemoveAt(0);
				string argstring = string.Join(" ", arguments.ToArray());
				CommandLineUtils utils = new CommandLineUtils(argstring, new string[] {"-v ","-o ","-t "});
				
				if (argstring.Contains("--voice-list"))
				{
					foreach (var Voice in Voices.VoiceList)
					{
						Console.WriteLine(
							$"Name: {Voice.Name} Lang: {Voice.Lang} Gender {Voice.Gender} Code: {Voice.VoiceFile}");
					}

					
					return;
				}
				string text = utils.GetValue("-t ");
				string path = utils.GetValue("-o ");
				string voice = utils.GetValue("-v ");
				if (path.Length == 0)
				{
					Console.WriteLine("No path specified, using output.mp3 instead.");
					path = "output.mp3";
				}
				if (text.Length == 0)
				{
					Console.WriteLine("No text specified. Args -t (text) -o (output file) -v (voice code) --voice-list (get all voice codes)");
					return;
				}

				if (voice.Length == 0)
				{
					Console.WriteLine("No voice specified. Try to launch with --voice-list to get all voices");
					return;
				}
				else
				{
					if (Voices.VoiceList.FirstOrDefault(n => n.VoiceFile == voice) == null)
					{
						Console.WriteLine("Voice code is not valid. Try to launch with --voice-list to get all voices");
					}
				}
				string link = Utils.Parse(text, Voices.VoiceList.First(n => n.VoiceFile == voice).VoiceFile);
				if (link == "")
				{ 
					Console.WriteLine("Can't download.");
					return;
				}
				using (var web = new WebClient())
				{
					web.DownloadFile(link, path);
					Console.WriteLine("Done");
				}

				return;


	        }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
