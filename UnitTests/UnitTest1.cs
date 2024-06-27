using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using AcapellaDownloader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using TestContext = NUnit.Framework.TestContext;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [Test]
        public void CheckVoices()
        {
	        Assert.That(true, Is.EqualTo(ParseAllVoices()));
        }
        const int InvalidMP3Size = 906;
        static bool CheckValidAudio(string fileUrl)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] fileData = client.DownloadData(fileUrl);
                    return fileData.Length > InvalidMP3Size;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool ParseAllVoices()
        {
	        Random random = new Random();
	        for (int i = 0; i < Voices.VoiceList.Count; ++i)
            {
                Voice voicelist = Voices.VoiceList[i];
                StringBuilder randStr = new StringBuilder();
                for (int j = 0; j < 10; ++j)
                {
	                randStr.Append((char)(random.Next(1, 26) + 64));
                }
                string AudioLink = Utils.GetSoundLink(randStr.ToString(), voicelist.VoiceId);
                if (AudioLink == "" || CheckValidAudio(AudioLink) == false)
                {
                    TestContext.Progress.WriteLine($"[{i+1}/{Voices.VoiceList.Count}] {voicelist.Name} NOT PASSED");
                    return false;
                }
                else
                {
                    TestContext.Progress.WriteLine($"[{i+ 1}/{Voices.VoiceList.Count}] {voicelist.Name} HAS PASSED");
                }
                
            }

            return true;
        }
    }
}
