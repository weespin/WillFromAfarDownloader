namespace AcapellaDownloader.SoundTouch
{
    //https://github.com/naudio/varispeed-sample
    internal class SoundTouchProfile
    {
        public bool UseTempo { get; set; }
        public bool UseAntiAliasing { get; set; }
        public bool UseQuickSeek { get; set; }
        public float PitchOctaves { get; set; } = 0f;

        public SoundTouchProfile(bool useTempo, bool useAntiAliasing)
        {
            UseTempo = useTempo;
            UseAntiAliasing = useAntiAliasing;
        }
    }
}