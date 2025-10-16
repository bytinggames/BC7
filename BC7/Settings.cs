namespace BC7
{
    public class Settings
    {
        /// <summary>Graphics Quality (Anti-Aliasing)</summary>
        public int? MSAA { get; set; } = 4;
        public bool Fullscreen { get; set; } = false;
        /// <summary>Random order of bots</summary>
        public bool ShuffleBotsOnce { get; set; } = true;
        /// <summary>Wether Exceptions should be caught. Set to false, when testing.</summary>
        public bool SilentExceptions { get; set; } = false;
        /// <summary>Speaks out the Bots Thoughts</summary>
        public bool Speech { get; set; } = false;
        /// <summary>0, 1 or 2</summary>
        public int GameSpeed { get; set; } = 0;
        public int AmountOfGames { get; set; } = 1;

        /// <summary>Use this for the actual competition. Sets SilentExceptions to true, Fullscreen to true and AmountOfGames to 3</summary>
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
