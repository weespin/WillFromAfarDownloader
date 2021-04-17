using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AcapellaDownloader
{
    public partial class Form1 : Form
    {
	    private float VoiceVolume = 1f;
	    private float Pitch = 1f;
        private const string _noText = "You did not enter any text";
	    private const string _noVoice = "Please select a voice";
	    public const string downloadError = "A download error has occurred";
	    public const string downloaded = "Done!";
	    SmbPitchShiftingSampleProvider PitchProvider;
        public Form1()
        {
	        InitializeComponent();
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
	        string soundLink = GetGUISoundLink();
            if (soundLink == "")
            {
                return;
            }
	        SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "MP3 File (*.mp3)|*.mp3";
            dialog.FileName = "tts.mp3";
            var s = dialog.ShowDialog();
            if (s == DialogResult.OK)
            {
                if (!Program.bOldWindows)
                {
                    using (var mf = new MediaFoundationReader(soundLink))
                    {
                        PitchProvider = new SmbPitchShiftingSampleProvider(mf.ToSampleProvider().ToMono());
                        PitchProvider.PitchFactor = Pitch;
                        MediaFoundationEncoder.EncodeToMp3(PitchProvider.ToWaveProvider(), dialog.FileName, 48000);
                        MessageBox.Show(downloaded);
                    }
                }
                else
                {
                    using (var wc = new WebClient())
                    {
                        wc.DownloadFile(soundLink, dialog.FileName);

                        MessageBox.Show(downloaded);

                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
	            WaveOutCapabilities WOC = WaveOut.GetCapabilities(i);
                cbx_Devices.Items.Add(WOC.ProductName);
            }
            cbx_Devices.Text = WaveOut.GetCapabilities(0).ProductName; //Returns default device
            var langs = Voices.VoiceList.GroupBy(i => i.Lang).Select(y => y.FirstOrDefault());
            foreach (var lang in langs)
            {
                TreeNode node;
                try
                {
                    node = tvLanguages.Nodes.Add(new CultureInfo(lang.Lang.Replace("_", "-")).DisplayName);
                }
                catch (Exception)
                {
                    //Wine Workaround
                    node = tvLanguages.Nodes.Add(lang.Lang);
                }
               
                foreach (var v in Voices.VoiceList.Where(n=>n.Lang==lang.Lang).ToArray())
                {
                    node.Nodes.Add(v.Name);
                }
            }

        }

        string GetGUISoundLink()
        {

	        if (string.IsNullOrEmpty(txtTextIn.Text))
	        {
		        MessageBox.Show(_noText); 
		        return "";
	        }

	        if (string.IsNullOrEmpty(SelectedLang))
	        {
		        MessageBox.Show(_noVoice);
		        return "";
	        }
	        string soundLink = Utils.GetSoundLink(txtTextIn.Text, Voices.VoiceList.First(n => n.Name == SelectedLang).VoiceId);

	        if (soundLink == "")
	        {
		        MessageBox.Show(downloadError);
		        return "";
	        }

	        return soundLink;
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
	        string soundLink = GetGUISoundLink();
	        if (string.IsNullOrEmpty(soundLink))
	        {
                return;
	        }
            int deviceId = cbx_Devices.SelectedIndex;
            Task.Run(() => PlaySound(soundLink, deviceId));
        }
        void PlaySound(string link, int WaveOutDeviceId)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(link).GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[4096];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                ms.Position = 0;
                using (WaveStream mf = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
                using (var wo = new WaveOutEvent())
                {
                    wo.DeviceNumber = WaveOutDeviceId;
                    PitchProvider = new SmbPitchShiftingSampleProvider(mf.ToSampleProvider().ToMono());
                    PitchProvider.PitchFactor = Pitch;
                    wo.Init(PitchProvider);
                    wo.Volume = VoiceVolume;
                    wo.Play();
                    while (wo.PlaybackState == PlaybackState.Playing)
                    {
                        PitchProvider.PitchFactor = Pitch;
                        wo.Volume = VoiceVolume;
                        Thread.Sleep(500);
                    }
                }
            }
        }
        private void tvLanguages_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvLanguages.SelectedNode != null)
            {
                if (tvLanguages.SelectedNode.Text != "" && tvLanguages.SelectedNode.Level != 0)
                {
                    SelectedLang = tvLanguages.SelectedNode.Text;
                    lbl_currentVoice.Text = "Selected Voice = " + SelectedLang;
                }
            }
        }
        public static string SelectedLang { get; set; }

        private void txtTextIn_Changed(object sender, EventArgs e)
        {
            if (txtTextIn.Text.Length > 350) //Actually its 350 symbols, not 300
            { 
                txtTextIn.ForeColor = Color.Red;
                btnDownload.Enabled = false;
                btnPlay.Enabled = false;
            }
            else
            {
                
                txtTextIn.ForeColor = Color.Black;
                btnDownload.Enabled =true;
                btnPlay.Enabled =true;
            }
        }

		private void slVolume_VolumeChanged(object sender, EventArgs e)
		{
			VoiceVolume = slVolume.Volume;
		}

		private void tbPitch_Scroll(object sender, EventArgs e)
		{
			if (tbPitch.Value > 11)
			{
				Pitch =( (tbPitch.Value - 1) / 10f);
			}
			else if (tbPitch.Value < 11)
			{
				Pitch = ( ((tbPitch.Value - 1) / 10f * 0.5f) + 0.5f);
			}
			else
			{
				Pitch = 1f;
			}

			lbl_pitchValue.Text = Pitch.ToString();

		}
	}
}
