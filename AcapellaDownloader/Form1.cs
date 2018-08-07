using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using AcapellaDownloader.Properties;
using AcapellaDownloader.SoundTouch;
using NAudio.MediaFoundation;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;

namespace AcapellaDownloader
{
    public partial class Form1 : Form
    {
        private VarispeedSampleProvider speedControl;
        public Form1()
        {
            InitializeComponent();

        }

        public static bool UseTempo;
        public static float Speed = 1f;
        public static float Octave;
        private IWavePlayer wavePlayer;
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
                dialog.Filter = "MP3 File Windows >7 (*.mp3)|*.mp3"; // |WAV (*.wav)|*.wav
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

                //var mf = new MediaFoundationReader(link);
                //if (UseTempo)
                //{
                //    speedControl = new VarispeedSampleProvider(mf.ToSampleProvider(), 100,
                //        new SoundTouchProfile(true, true) {PitchOctaves = Octave});
                //}
                //else
                //{
                //    speedControl = new VarispeedSampleProvider(mf.ToSampleProvider(), 100,
                //        new SoundTouchProfile(false, true));
                //}

                //var src = speedControl;
                //var ext = Path.GetExtension(dialog.FileName);
                //if (ext == ".wav")
                //{


                //    WaveFileWriter.CreateWaveFile("ok.wav", speedControl.ToWaveProvider());
                //    Debug.WriteLine("OK");
                //}
            

        
                //        if (ext == ".mp3")
                //        {
                //                if (Environment.OSVersion.Version.Major < 6)
                //                {
                //                    MessageBox.Show("MP3 encoding on your OS is unsupported");
                //                    return;
                                   
                //                }
                //                if (Environment.OSVersion.Version.Major == 6 &&
                //                    Environment.OSVersion.Version.Minor < 2)
                //                {
                //                    MessageBox.Show("MP3 encoding on your OS is unsupported");
                //                    return;
                //                }
                //            //Save MP3
                               
                //            using (var enc = new MediaFoundationEncoder(MediaFoundationEncoder.SelectMediaType(
                //                    AudioSubtypes.MFAudioFormat_MP3,
                //                    new WaveFormat(22050, 1),
                //                    0)))
                //                {
                //                   enc.Encode(dialog.FileName,speedControl.ToWaveProvider());
                                   
                //                }
                //        }
                        using (var wc = new WebClient())
                {
                    wc.DownloadFile(link, dialog.FileName);
                }

                        MessageBox.Show("Downloaded!");
                    
                }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
            Voices.Load();
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
                if (UseTempo)
                {
                    speedControl = new VarispeedSampleProvider(mf.ToSampleProvider(), 100,
                        new SoundTouchProfile(true, true) {PitchOctaves = Octave});
                }
                else
                {
                    speedControl = new VarispeedSampleProvider(mf.ToSampleProvider(), 100,
                        new SoundTouchProfile(false, true) );
                }

                speedControl.PlaybackRate = Speed;
                
                wo.DeviceNumber = WaveOutDeviceId;
                wo.Init(mf);
                wo.Init(speedControl);
                wo.Play();
               
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }
            }
        }
        private void EnableControls(bool isPlaying)
        {
            button1.Enabled = !isPlaying;
         
        }
        private void WavePlayerOnPlaybackStopped(object sender, StoppedEventArgs stoppedEventArgs)
        {
            if (stoppedEventArgs.Exception != null)
            {
                MessageBox.Show(stoppedEventArgs.Exception.Message, "Playback Stopped Unexpectedly");
            }
            EnableControls(false);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 350) //Actually its 350 symbols, not 300
            { 
                textBox1.ForeColor = Color.Red;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                
                textBox1.ForeColor = Color.Black;
                button1.Enabled =true;
                button2.Enabled =true;
            }
        }
        private void trackBar1_ValueChanged_1(object sender, EventArgs e)
        {

            Speed = 0.5f + trackBar1.Value * 0.1f;
            Debug.WriteLine(Speed);

            label2.Text = $"Speed: {Speed*100}%";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            
            Octave = trackBar2.Value * 0.1f;
            label3.Text = $"Octave: {Octave*10}";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                UseTempo = true;
                trackBar2.Visible = true;
                label3.Visible = true;
            }
            else
            {
                UseTempo =false;
                trackBar2.Visible = false;
                label3.Visible = false;
            }
        }
    }
}
