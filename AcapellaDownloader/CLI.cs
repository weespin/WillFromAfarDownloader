using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace AcapellaDownloader
{
	

	class CommandLineUtils
	{
		public CommandLineUtils(string dataIn, string[] keysIn)
		{
			//Store keys
			foreach (var key in keysIn)
			{
				_ConsoleKeyValues.Add(key, "");
			}

			foreach (var key in _ConsoleKeyValues.Keys.ToList())
			{
				int index = dataIn.IndexOf(key, StringComparison.Ordinal);
				if (index == -1)
				{
					continue;
				}
				int end = dataIn.Length;
				//Find next key
				foreach (var anotherkey in _ConsoleKeyValues.Keys.Where(n => n != key).ToList())
				{
					int anotherindex = dataIn.IndexOf(anotherkey);
					if (anotherindex > index)
					{
						end = Math.Min(end, anotherindex);
					}
				}

				string value = dataIn.Substring(index + key.Length, end - index - key.Length);
				value = value.Trim();
				_ConsoleKeyValues[key] = value;

			}
		}

		public string GetValue(string key)
		{
			if (_ConsoleKeyValues.ContainsKey(key))
			{
				return _ConsoleKeyValues[key];
			}
			return "";
		}

		Dictionary<string, string> _ConsoleKeyValues = new Dictionary<string, string>();
	}

	static class CLI
	{
		//WinAPI stuff
		[DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern int AllocConsole();
		[DllImport("kernel32.dll", EntryPoint = "AttachConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern int AttachConsole(int dwProcessId);
		
		
		public static void PassCLI(string[] commandLineArgs)
		{
			//Switch to CLI Mode
			if (AttachConsole(-1) == 0)
			{
				AllocConsole();
			}
			StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
			standardOutput.AutoFlush = true;
			Console.SetOut(standardOutput);

			var arguments = commandLineArgs.ToList();
			arguments.RemoveAt(0);

			string argString = string.Join(" ", arguments.ToArray());
			CommandLineUtils utils = new CommandLineUtils(argString, new string[] { "-v ", "-o ", "-t " });

			if (argString.Contains("--voice-list"))
			{
				foreach (var curVoice in Voices.VoiceList)
				{
					Console.WriteLine($"Name: {curVoice.Name} Lang: {curVoice.Lang} Gender {curVoice.Gender} VoiceId: {curVoice.VoiceId}");
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
				Console.WriteLine("No VoiceId specified. Try to launch with --voice-list to get all voices");
				return;
			}
			else
			{
				if (Voices.VoiceList.FirstOrDefault(n => n.VoiceId == voice) == null)
				{
					Console.WriteLine("Voice code is not valid. Try to launch with --voice-list to get all voices");
				}
			}
			string dlLink = Utils.GetSoundLink(text, voice);
			if (dlLink == "")
			{
				Console.WriteLine(Form1.downloadError);
				return;
			}
			using (var web = new WebClient())
			{
				web.DownloadFile(dlLink, path);
				Console.WriteLine(Form1.downloaded);
			}

			return;
		}

	}
}
