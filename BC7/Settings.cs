namespace BC7
{
    public class Settings
    {
        public int? MSAA { get; set; } = 4;
        public bool Fullscreen { get; set; } = false;
        public bool ShuffleBotsOnce { get; set; } = true;
        public bool KeyShortcuts { get; set; } = true;
        /// <summary>Currently not working.</summary>
        public int? Seed { get; set; } = null;
        public bool SilentExceptions { get; set; } = false;

        public int GameSpeed { get; set; } = 0;
        public int AmountOfGames { get; set; } = 1;


        public bool SetRealGame
        {
            get => false;
            set
            {
                if (value)
                {
                    SilentExceptions = true;
                    Fullscreen = true;
                    AmountOfGames = 3;
                }
            }
        }
    }
}
