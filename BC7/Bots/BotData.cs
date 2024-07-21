namespace BC7
{
    internal class BotData
    {
        public int Successes { get; set; } = 0;
        public Discs DiscsInHand { get; } = new(3, 1);
        public DiscsStack DiscsPlayed { get; } = new();
        public DiscsStack DiscsRevealed { get; } = new();
        public Discs DiscsDestroyed { get; } = new();
        public bool Passed { get; set; } = false;
        public int ID { get; }
        public bool Alive { get; set; } = true;

        public BotData(int id)
        {
            ID = id;
        }
    }
}
