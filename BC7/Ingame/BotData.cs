namespace BC7
{
    public class BotData
    {
        public int Successes { get; internal set; } = 0;
        public Discs DiscsInHand { get; } = new(3, 1);
        public DiscsStack DiscsPlayed { get; } = new();
        public DiscsStackPublic DiscsRevealed { get; } = new();
        public Discs DiscsDestroyed { get; } = new();
        public bool Passed { get; internal set; } = false;
        public int LastBidThisRound { get; internal set; } = 0;
        public int ID { get; }
        public bool Alive { get; internal set; } = true;
        public bool LastSurvivor { get; internal set; }
        public int Lives => DiscsInHand.Count() + DiscsPlayed.Count() + DiscsRevealed.Count();

        internal BotData(int id)
        {
            ID = id;
        }
    }
}
