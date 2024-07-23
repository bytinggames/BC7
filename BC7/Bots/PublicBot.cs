namespace BC7
{
    public struct PublicBot
    {
        public int ID { get; }
        public int DiscsInHandCount { get; }
        public int DiscsPlayedCount { get; }
        public DiscsStack DiscsRevealed { get; }
        public int DiscsDestroyedCount { get; }
        /// <summary>
        /// 0 if no bid has been placed this round.
        /// </summary>
        public int LastBidThisRound { get; }

        public PublicBot(int id, int discsInHandCount, int discsPlayedCount, DiscsStack discsRevealed, int discsDestroyedCount, int lastBidThisRound)
        {
            ID = id;
            DiscsInHandCount = discsInHandCount;
            DiscsPlayedCount = discsPlayedCount;
            DiscsRevealed = discsRevealed;
            DiscsDestroyedCount = discsDestroyedCount;
            LastBidThisRound = lastBidThisRound;
        }
    }
}
