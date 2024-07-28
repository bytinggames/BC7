namespace BC7
{
    public class Settings
    {
        public int? MSAA { get; set; } = 4;
        public bool Fullscreen { get; set; } = false;
        public bool ShuffleBots { get; set; } = true;
        public bool KeyShortcuts { get; set; } = true;
        public bool VisibleGame { get; set; } = true;
        /// <summary>Currently not working.</summary>
        public int? Seed { get; set; } = null;
        public bool SilentExceptions { get; set; } = false;

        public bool WaitForEnterToContinueGame { get; set; } = true;


        public bool SetRealGame
        {
            get => false;
            set
            {
                if (value)
                {
                    SilentExceptions = true;
                }
            }
        }
    }
}
