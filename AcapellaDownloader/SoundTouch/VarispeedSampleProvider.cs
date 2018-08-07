using System;
using NAudio.Wave;
using SoundTouch;

//Main code from https://github.com/naudio/varispeed-sample
//Adapted to SoundTouch.Net by Weespin
namespace AcapellaDownloader.SoundTouch
{
    using TSampleType = System.Single;
    using TLongSampleType = System.Double;
    class VarispeedSampleProvider : ISampleProvider, IDisposable
    {

        private readonly ISampleProvider sourceProvider;
        private readonly SoundTouch<TSampleType, TLongSampleType> soundTouch;
        private readonly float[] sourceReadBuffer;
        private readonly float[] soundTouchReadBuffer;
        private readonly int channelCount;
        private float playbackRate = 1.0f;
        private SoundTouchProfile currentSoundTouchProfile;
        private bool repositionRequested;

        public VarispeedSampleProvider(ISampleProvider sourceProvider, int readDurationMilliseconds, SoundTouchProfile soundTouchProfile)
        {
            soundTouch = new SoundTouch<TSampleType, TLongSampleType>();
            // explore what the default values are before we change them:
            //Debug.WriteLine(String.Format("SoundTouch Version {0}", soundTouch.VersionString));
            //Debug.WriteLine("Use QuickSeek: {0}", soundTouch.GetUseQuickSeek());
            //Debug.WriteLine("Use AntiAliasing: {0}", soundTouch.GetUseAntiAliasing());
            currentSoundTouchProfile = soundTouchProfile;
            SetSoundTouchProfile(soundTouchProfile);
            this.sourceProvider = sourceProvider;
            soundTouch.SetSampleRate(WaveFormat.SampleRate);
            channelCount = WaveFormat.Channels;
            soundTouch.SetChannels(channelCount);
            sourceReadBuffer = new float[(WaveFormat.SampleRate * channelCount * (long)readDurationMilliseconds) / 1000];
            soundTouchReadBuffer = new float[sourceReadBuffer.Length * 10]; // support down to 0.1 speed
        //    currentSoundTouchProfile = soundTouchProfile;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (playbackRate == 0) // play silence
            {
                for (int n = 0; n < count; n++)
                {
                    buffer[offset++] = 0;
                }
                return count;
            }

            if (repositionRequested)
            {
                soundTouch.Clear();
                repositionRequested = false;
            }

            int samplesRead = 0;
            bool reachedEndOfSource = false;
            while (samplesRead < count)
            {

                if (soundTouch.AvailableSamples == 0)
                {
                    var readFromSource = sourceProvider.Read(sourceReadBuffer, 0, sourceReadBuffer.Length);
                    if (readFromSource > 0)
                    {
                        soundTouch.PutSamples(sourceReadBuffer, readFromSource / channelCount);
                    }
                    else
                    {
                        reachedEndOfSource = true;
                       
                        // we've reached the end, tell SoundTouch we're done
                        soundTouch.Flush();
                       // return 0;
                    }
                }
                var desiredSampleFrames = (count - samplesRead) / channelCount;
                
                var received = soundTouch.ReceiveSamples(soundTouchReadBuffer, desiredSampleFrames) * channelCount;
                    for (int n = 0; n < received; n++)
                    {
                        buffer[offset + samplesRead++] = soundTouchReadBuffer[n];
                    }
                    if (received == 0 && reachedEndOfSource) break;
                
               
                // use loop instead of Array.Copy due to WaveBuffer
                
            }
            return samplesRead;
        }

        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        public float PlaybackRate
        {
            get
            {
                return playbackRate;
            }
            set
            {
                if (playbackRate != value)
                {
                    UpdatePlaybackRate(value);
                    playbackRate = value;
                }
            }
        }

        private void UpdatePlaybackRate(float value)
        {
            if (value != 0)
            {
                if (currentSoundTouchProfile.UseTempo)
                {
                    soundTouch.SetTempo(value);
                }
                else
                {
                    soundTouch.SetRate(value);
                }
            }
        }

        public void Dispose()
        {
            soundTouch.Clear();

        }

        public void SetSoundTouchProfile(SoundTouchProfile soundTouchProfile)
        {
            if (currentSoundTouchProfile != null
             
             )
            {
                if (soundTouchProfile.UseTempo)
                {
                    soundTouch.SetRate(1.0f);
                    soundTouch.SetPitchOctaves(soundTouchProfile.PitchOctaves);
                    soundTouch.SetTempo(playbackRate);
                }
                else
                {
                    soundTouch.SetTempo(1.0f);
                    soundTouch.SetRate(playbackRate);
                }
            }
            this.currentSoundTouchProfile = soundTouchProfile;
            soundTouch.SetSetting(SettingId.UseQuickseek, soundTouchProfile.UseQuickSeek ? 1 : 0);
            soundTouch.SetSetting(SettingId.UseAntiAliasFilter, soundTouchProfile.UseAntiAliasing ? 1 : 0);

        }

        public void Reposition()
        {
            repositionRequested = true;
        }
    }
}