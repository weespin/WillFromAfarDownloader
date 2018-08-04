using System;
using System.Diagnostics;
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
            var result = false;
            Voices.Load();
            foreach (var voicelist in Voices.VoiceList)
            {
                if (Utils.Parse("123 312", voicelist.VoiceFile) == "")
                {
                    TestContext.WriteLine(voicelist.Name);
                    return false;
                }
            }

            return true;
        }
    }
}
