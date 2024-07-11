namespace BC7
{
    public class Settings
    {
        public int? StartupScene { get; set; } = 1;
        public int? MSAA { get; set; } = 8;
        public bool Fullscreen { get; set; } = false;
        public bool ShuffleBots { get; set; } = true;
        public bool TakeGraphScreenshots { get; set; } = false;
        public bool FadeIn { get; set; } = false;
        public bool FadeOut { get; set; } = false;
        public bool KeyShortcuts { get; set; } = true;
        public bool CatchExceptions { get; set; } = false;
        public bool VisibleGame { get; set; } = true;
        public bool CustomDraw { get; set; } = true;
        public int? Seed { get; set; } = null;
    }
}
