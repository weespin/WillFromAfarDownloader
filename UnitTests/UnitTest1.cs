using System;
using System.Diagnostics;
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
                if (Utils.GetSoundLink(randStr.ToString(), voicelist.VoiceId) == "")
                {
                    TestContext.Progress.WriteLine($"[{i+1}/{Voices.VoiceList.Count}] {voicelist.Name} NOT PASSED");
                    return false;
                }
                else
                {
                    TestContext.Progress.WriteLine($"[{i+ 1}/{Voices.VoiceList.Count}]"+voicelist.Name + "HAS PASSED");
                }
                
            }

            return true;
        }
    }
}
