using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VoiceMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is a small tool to check voices;
            //Yes all voice ids are in /Debug/voices.json file
            var VoiceList = JsonConvert.DeserializeObject<JsonData.RootObject>(File.ReadAllText("voices.json"));
            foreach (var voice in VoiceList.voices)
            {
                Console.WriteLine($"Checking..  {voice.name}: ID: {voice.id}");
                var res = CheckVoice("lulu22k_hq");
                if (res)
                {
                    Console.WriteLine("Working!");
                }
                else
                {
                    Console.WriteLine("Not Working! :(");
                }
            }
            Console.Read();
        }

        public static bool CheckVoice(string voiceid)
        {
            if (Parse("testing", voiceid) != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string Parse(string text, string voiceid)
        {
            voiceid = voiceid.Replace("22k_hq", "_22k_ns.bvcu");//enu_willhappy_22k_ns.bvcu
            HttpClient client = new HttpClient();
            Random rnd = new Random();
            int length = 20;
            var str = "{\"googleid\":\"";
            var email = "";
            for (var i = 0; i < length; i++)
            {
                email += ((char)(rnd.Next(1, 26) + 64)).ToString();
            }

            email += "@gmail.com";
            var values = new Dictionary<string, string>
            {
                {"json", str + email + "\"}"}

            };
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync("https://acapelavoices.acapela-group.com/index/getnonce/", content).Result
                .Content.ReadAsStringAsync().Result;
            var re = new Regex(@"^\{\""nonce\""\:\""(.+)\""\}$");
            var m = re.Match(response);
            if (m.Groups.Count > 1)
            {
                var request =
                    (HttpWebRequest)WebRequest.Create(
                        "http://www.acapela-group.com:8080/webservices/1-34-01-Mobility/Synthesizer");
                var g = "{\"nonce\":\"" + m.Groups[1] + ",\"user\":\"" + email + "\"}";

                var enc =
                    $"req_voice=enu_{voiceid}&cl_pwd=&cl_vers=1-30&req_echo=ON&cl_login=AcapelaGroup&req_comment=%7B%22nonce%22%3A%22{m.Groups[1]}%22%2C%22user%22%3A%22{email}%22%7D&req_text={Uri.EscapeDataString(text)}&cl_env=ACAPELA_VOICES&prot_vers=2&cl_app=AcapelaGroup_WebDemo_Android";

                var data = Encoding.ASCII.GetBytes(enc.ToString());
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var responses = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(responses.GetResponseStream()).ReadToEnd();
                var reg = new Regex("snd_url=(.+)&snd_size");
                var regs = reg.Match(responseString);
                if (regs.Success)
                {
                    return regs.Groups[1].Value;
                }

                return "";

            }

            return "";
        }
    }

    public class JsonData //lazy to make, used from http://json2csharp.com/
    {
    public class Package
    {
        public string format { get; set; }
    }

    public class Voice
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lang { get; set; }
        public string ms { get; set; }
        public string description { get; set; }
        public string gender { get; set; }
        public string price { get; set; }
        public List<object> samples { get; set; }
        public string rights { get; set; }
        public List<Package> packages { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public List<Voice> voices { get; set; }
    }
    }
}
