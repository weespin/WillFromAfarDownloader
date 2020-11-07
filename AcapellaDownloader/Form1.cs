using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace AcapellaDownloader
{
    public partial class Form1 : Form
    {
	    private float VoiceVolume = 1;
	    private const string _noText = "You did not enter the text";
	    private const string _noVoice = "Please select a voice";
	    public const string downloadError = "A download error has occurred";
	    public const string downloaded = "Done!";
        public Form1()
        {
            InitializeComponent();
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
	        string soundLink = GetGUISoundLink();
	        SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "MP3 File (*.mp3)|*.mp3";
            dialog.FileName = "tts.mp3";
            var s = dialog.ShowDialog();
            if (s == DialogResult.OK)
            {
	            using (var web = new WebClient())
                {
                    web.DownloadFile(soundLink, dialog.FileName);
                    MessageBox.Show(downloaded);
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
                var node = tvLanguages.Nodes.Add(new CultureInfo(lang.Lang.Replace("_", "-")).DisplayName);
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
            using (var mf = new MediaFoundationReader(link))
            using (var wo = new WaveOutEvent())
            {
                wo.DeviceNumber = WaveOutDeviceId;
                wo.Init(mf);
                wo.Volume = VoiceVolume;
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
	                wo.Volume = VoiceVolume;
	                Thread.Sleep(500);
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

	}
}
