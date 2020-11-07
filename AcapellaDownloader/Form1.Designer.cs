namespace AcapellaDownloader
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.txtTextIn = new System.Windows.Forms.TextBox();
			this.btnDownload = new System.Windows.Forms.Button();
			this.btnPlay = new System.Windows.Forms.Button();
			this.cbx_Devices = new System.Windows.Forms.ComboBox();
			this.tvLanguages = new System.Windows.Forms.TreeView();
			this.lbl_currentVoice = new System.Windows.Forms.Label();
			this.slVolume = new NAudio.Gui.VolumeSlider();
			this.lbl_Volume = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtTextIn
			// 
			this.txtTextIn.Location = new System.Drawing.Point(12, 12);
			this.txtTextIn.Multiline = true;
			this.txtTextIn.Name = "txtTextIn";
			this.txtTextIn.Size = new System.Drawing.Size(486, 215);
			this.txtTextIn.TabIndex = 0;
			this.txtTextIn.TextChanged += new System.EventHandler(this.txtTextIn_Changed);
			// 
			// btnDownload
			// 
			this.btnDownload.Location = new System.Drawing.Point(12, 233);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(97, 46);
			this.btnDownload.TabIndex = 1;
			this.btnDownload.Text = "Download";
			this.btnDownload.UseVisualStyleBackColor = true;
			this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
			// 
			// btnPlay
			// 
			this.btnPlay.Location = new System.Drawing.Point(115, 233);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(81, 46);
			this.btnPlay.TabIndex = 3;
			this.btnPlay.Text = "Play";
			this.btnPlay.UseVisualStyleBackColor = true;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// cbx_Devices
			// 
			this.cbx_Devices.FormattingEnabled = true;
			this.cbx_Devices.Location = new System.Drawing.Point(202, 234);
			this.cbx_Devices.Name = "cbx_Devices";
			this.cbx_Devices.Size = new System.Drawing.Size(184, 21);
			this.cbx_Devices.TabIndex = 4;
			// 
			// tvLanguages
			// 
			this.tvLanguages.Location = new System.Drawing.Point(504, 12);
			this.tvLanguages.Name = "tvLanguages";
			this.tvLanguages.Size = new System.Drawing.Size(240, 215);
			this.tvLanguages.TabIndex = 5;
			this.tvLanguages.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLanguages_AfterSelect);
			// 
			// lbl_currentVoice
			// 
			this.lbl_currentVoice.AutoSize = true;
			this.lbl_currentVoice.Location = new System.Drawing.Point(202, 266);
			this.lbl_currentVoice.Name = "lbl_currentVoice";
			this.lbl_currentVoice.Size = new System.Drawing.Size(0, 13);
			this.lbl_currentVoice.TabIndex = 6;
			// 
			// slVolume
			// 
			this.slVolume.Location = new System.Drawing.Point(635, 255);
			this.slVolume.Name = "slVolume";
			this.slVolume.Size = new System.Drawing.Size(109, 24);
			this.slVolume.TabIndex = 7;
			this.slVolume.VolumeChanged += new System.EventHandler(this.slVolume_VolumeChanged);
			// 
			// lbl_Volume
			// 
			this.lbl_Volume.AutoSize = true;
			this.lbl_Volume.Location = new System.Drawing.Point(584, 261);
			this.lbl_Volume.Name = "lbl_Volume";
			this.lbl_Volume.Size = new System.Drawing.Size(45, 13);
			this.lbl_Volume.TabIndex = 8;
			this.lbl_Volume.Text = "Volume:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(756, 291);
			this.Controls.Add(this.lbl_Volume);
			this.Controls.Add(this.slVolume);
			this.Controls.Add(this.lbl_currentVoice);
			this.Controls.Add(this.tvLanguages);
			this.Controls.Add(this.cbx_Devices);
			this.Controls.Add(this.btnPlay);
			this.Controls.Add(this.btnDownload);
			this.Controls.Add(this.txtTextIn);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "AcapellaDownloader";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTextIn;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ComboBox cbx_Devices;
        private System.Windows.Forms.TreeView tvLanguages;
        private System.Windows.Forms.Label lbl_currentVoice;
		private NAudio.Gui.VolumeSlider slVolume;
		private System.Windows.Forms.Label lbl_Volume;
	}
}

