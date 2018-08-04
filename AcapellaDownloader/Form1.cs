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
using WillFromAfarDownloader;

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
                         link = Utils.Parse(textBox1.Text, "enu_willhappy_22k_ns.bvcu");
                        break;
                    case "WillLittleCreature":
                        link = Utils.Parse(textBox1.Text, "enu_willlittlecreature_22k_ns.bvcu");
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
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
              
                WaveOutCapabilities WOC = WaveOut.GetCapabilities(i);
                comboBox2.Items.Add(WOC.ProductName);
            }
            comboBox2.Text = WaveOut.GetCapabilities(0).ProductName; //Returns default device
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("No text");
                return;
            }
            string link = "";
            switch (comboBox1.GetItemText(comboBox1.SelectedItem))
            {
                case "WillFromAfar":
                    link = Utils.Parse(textBox1.Text, "enu_willhappy_22k_ns.bvcu");
                    break;
                case "WillLittleCreature":
                    link = Utils.Parse(textBox1.Text, "enu_willlittlecreature_22k_ns.bvcu");
                    break;
            }
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
    }
}
