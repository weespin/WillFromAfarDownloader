using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using WillFromAfarDownloader;
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
            var result = false;
           WillFromAfarDownloader.Voices.Load();
            foreach (var voicelist in Voices.VoiceList)
            {
                if (WillFromAfarDownloader.Utils.Parse("123 312", voicelist.VoiceFile) == "")
                {
                    TestContext.WriteLine(voicelist.Name);
                    
                }
            }

            return true;
        }
    }
}
