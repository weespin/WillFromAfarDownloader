using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace AcapellaDownloader
{
   public static class Utils
   {
	    private const string _NonceEndpoint = "https://acapelavoices.acapela-group.com/index/getnonce/";
        private const string _SynthesizerEndpoint = "http://www.acapela-group.com:8080/webservices/1-34-01-Mobility/Synthesizer";
        private static string _CachedNonce = "";
        private static string _CachedEmail = "";
        private static bool _LastFailed = false;
        public static bool UpdateNonceToken()
        {
            HttpClient httpClient = new HttpClient();
            Random random = new Random();

            int emailLength = random.Next(10, 20);

            StringBuilder fakeEmail = new StringBuilder();
            for (var i = 0; i < emailLength; i++)
            {
                fakeEmail.Append((char)(random.Next(1, 26) + 64));
            }

            fakeEmail.Append("@gmail.com");

            var nonceRequestValues = new Dictionary<string, string>
            {
                { "json", "{\"googleid\":\"" + fakeEmail.ToString() + "\"}" }

            };

            var nonceRequestContent = new FormUrlEncodedContent(nonceRequestValues);
            var nonceResponse = httpClient.PostAsync(_NonceEndpoint, nonceRequestContent).Result.Content
                .ReadAsStringAsync().Result;
            var nonceRegex = new Regex(@"^\{\""nonce\""\:\""(.+)\""\}$");
            var nonceRegexMatch = nonceRegex.Match(nonceResponse);
            if (nonceRegexMatch.Groups.Count > 1)
            {
                _CachedNonce = nonceRegexMatch.Groups[1].Value;
                _CachedEmail = fakeEmail.ToString();
                _LastFailed = false;
                return true;
            }

            return false;
        }

        public static string GetSoundLink(string text, string voiceid)
        {
            if (_CachedEmail == "" || _CachedNonce == "" || _LastFailed)
            {
                UpdateNonceToken();
            }
            var synthesizerRequest = (HttpWebRequest)WebRequest.Create(_SynthesizerEndpoint);
            var synthesizerRequestString = $"req_voice={voiceid}&cl_pwd=&cl_vers=1-30&req_echo=ON&cl_login=AcapelaGroup&req_comment=%7B%22nonce%22%3A%22{_CachedNonce}%22%2C%22user%22%3A%22{_CachedEmail}%22%7D&req_text={Uri.EscapeDataString(text)}&cl_env=ACAPELA_VOICES&prot_vers=2&cl_app=AcapelaGroup_WebDemo_Android";
            var data = Encoding.ASCII.GetBytes(synthesizerRequestString);
            synthesizerRequest.Method = "POST";
            synthesizerRequest.ContentType = "application/x-www-form-urlencoded";
            synthesizerRequest.ContentLength = data.Length;

            using (var stream = synthesizerRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var synthesizerResponseStream = synthesizerRequest.GetResponse().GetResponseStream();
            if (synthesizerResponseStream == null)
            { 
                _LastFailed = true; 
                return "";
            }
            var synthesizerResponseString = new StreamReader(synthesizerResponseStream).ReadToEnd();
            var synthesizerRegex = new Regex("snd_url=(.+)&snd_size");
            var synthesizerMatch = synthesizerRegex.Match(synthesizerResponseString);

            if (synthesizerMatch.Success)
            {
                return synthesizerMatch.Groups[1].Value;
            }
            _LastFailed = true;
            return "";
        }
    }
}
