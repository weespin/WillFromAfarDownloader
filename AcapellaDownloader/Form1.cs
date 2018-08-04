using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
       
      
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("No text");
                return;
            }

            if (string.IsNullOrEmpty(SelectedLang))
            {
                MessageBox.Show("Please select voice");
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "MP3 File (*.mp3)|*.mp3";
                dialog.FileName = "tts.mp3";
                var s =dialog.ShowDialog();
                if (s == DialogResult.OK)
                {
                    string link = Utils.Parse(textBox1.Text,
                        Voices.VoiceList.First(n => n.Name == SelectedLang).VoiceFile);
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
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Voices.Load();;
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
              
                WaveOutCapabilities WOC = WaveOut.GetCapabilities(i);
                comboBox2.Items.Add(WOC.ProductName);
            }
            comboBox2.Text = WaveOut.GetCapabilities(0).ProductName; //Returns default device
            //Draw tree
            var langs = Voices.VoiceList.GroupBy(i => i.Lang).Select(y => y.FirstOrDefault());
            foreach (var lang in langs)
            {
                //  treeView1.Nodes.Add(new CultureInfo(lang.Lang.Replace("_","-")).DisplayName);
                var node = treeView1.Nodes.Add(new CultureInfo(lang.Lang.Replace("_", "-")).DisplayName);
                foreach (var v in Voices.VoiceList.Where(n=>n.Lang==lang.Lang).ToArray())
                {
                    node.Nodes.Add(v.Name);
                }
            }

           // treeView1.Nodes.Add()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("No text");
                return;
            }

            if (string.IsNullOrEmpty(SelectedLang))
            {
                MessageBox.Show("Please select voice");
                return;
            }
            string link = Utils.Parse(textBox1.Text,
                Voices.VoiceList.First(n => n.Name == SelectedLang).VoiceFile);
            if (link == "")
            {
                MessageBox.Show("Can't download. Maybe this voice is paid.");
                return;
            }
            int id = comboBox2.SelectedIndex;
            Task.Run(() => Playsnd(link, id));
        }
        void Playsnd(string link, int WaveOutDeviceId)
        {
            using (var mf = new MediaFoundationReader(link))
            using (var wo = new WaveOutEvent())
            {
                wo.DeviceNumber = WaveOutDeviceId;
                wo.Init(mf);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Text != "" && treeView1.SelectedNode.Level != 0)
                {
                    SelectedLang = treeView1.SelectedNode.Text;
                    label1.Text = "Selected Voice = " + SelectedLang;
                }
            }
        }
        public static string SelectedLang { get; set; } 
    }
}
