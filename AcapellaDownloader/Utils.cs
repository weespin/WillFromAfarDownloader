using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace AcapellaDownloader
{
    public static class Utils
    {
        private const string _NonceEndpoint = "https://acapelavoices.acapela-group.com/index/getnonce/";
        private const string _SynthesizerEndpoint = "https://www.acapela-group.com:8443/Services/Synthesizer";
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
            synthesizerRequest.UserAgent = "Dalvik/2.1.0 (Linux; U; Android 14; CPH2591 Build/UP1A.230620.001)";
            var parameters = new Dictionary<string, string>
            {
                 { "cl_vers", "1-30" },
                 { "req_text", text },
                 { "cl_login", "AcapelaGroup" },
                 { "cl_app", "AcapelaGroup_WebDemo_Android" },
                 { "req_comment", $"{{\"nonce\":\"{_CachedNonce}\",\"user\":\"{_CachedEmail}\"}}" },
                 { "prot_vers", "2" },
                 { "cl_env", "ACAPELA_VOICES" },
                 { "cl_pwd", "" },
                 { "req_voice", voiceid },
                 { "req_echo", "ON" },
            };

            var parametersList = parameters
                .Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}")
                .ToList();

            var synthesizerRequestString = string.Join("&", parametersList);
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
