using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using Newtonsoft.Json;

namespace AcapellaDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

                Console.WriteLine(m.Groups[1]);
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
        List<VoiceMap> maplist = new List<VoiceMap>();
        private void button1_Click(object sender, EventArgs e)
        {
           
                //JustWill
              //  Parse();
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "MP3 File (*.mp3)|*.mp3";
                dialog.FileName = "tts.mp3";
                var s =dialog.ShowDialog();
                if (s == DialogResult.OK)
                {
                    string link = "";
                    switch (comboBox1.GetItemText(comboBox1.SelectedItem))
                    {
                    case "WillFromAfar":
                         link = Parse(textBox1.Text, "willfromafar22k_hq");
                        break;
                    case "WillLittleCreature":
                        link = Parse(textBox1.Text, "willlittlecreature22k_hq");
                        break;
                        
                }
                 
                   
                    if (link == "")
                    {
                        MessageBox.Show("Can't download. Maybe this voice is paid.");
                        return;
                    }
                    using (var web = new WebClient())
                    {
                        web.DownloadFile(link ,dialog.FileName);
                        MessageBox.Show("Downloaded!");
                    }
                }

            //} Maybe in next update.
            //else
            //{
            //    var picked = maplist.Where(n => n.name == comboBox1.Text).FirstOrDefault();
            //    if (picked == null)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        SaveFileDialog dialog = new SaveFileDialog();
            //        dialog.Filter = "MP3 File (*.mp3)|*.mp3";
            //        dialog.FileName = "tts.mp3";
            //        var s = dialog.ShowDialog();
            //        if (s == DialogResult.OK)
            //        {
            //            var link = Parse(textBox1.Text,picked.code);
            //            if (link == "")
            //            {
            //                MessageBox.Show("Can't download. Maybe this voice is paid.");
            //                return;
            //            }
            //            using (var web = new WebClient())
            //            {

            //                web.DownloadFile(link, dialog.FileName);
            //                MessageBox.Show("Downloaded!");
            //            }
            //        }
            //    }

        
    }

     
        public class VoiceMap
        {
            public string name { get; set; }
            public string code { get; set; }
            public string gender { get; set; }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //try
            //{
            //    maplist = JsonConvert.DeserializeObject<List<VoiceMap>>(File.ReadAllText("./voice.map"));
            //    foreach (var voice in maplist)
            //    {
            //        comboBox1.Items.Add(voice.name);
            //    }
            //}
            //catch (Exception)
            //{
            //   // Console.WriteLine("Caaaaant read voicemap");

            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string link = "";
            switch (comboBox1.GetItemText(comboBox1.SelectedItem))
            {
                case "WillFromAfar":
                    link = Parse(textBox1.Text, "willfromafar22k_hq");
                    break;
                case "WillLittleCreature":
                    link = Parse(textBox1.Text, "willlittlecreature22k_hq");
                    break;

            }


            if (link == "")
            {
                MessageBox.Show("Can't download. Maybe this voice is paid.");
                return;
            }

            Task.Run(() => Playsnd(link));
        }

        void Playsnd(string link)
        {
            using (var mf = new MediaFoundationReader(link))
            using (var wo = new WaveOutEvent())
            {
                wo.Init(mf);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }

    }
}
