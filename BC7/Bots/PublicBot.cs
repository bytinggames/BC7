namespace BC7
{
    public struct PublicBot
    {
        public int DiscsInHandCount { get; }
        public int DiscsPlayedCount { get; }
        public Discs DiscsRevealed { get; }
        public int DiscsDestroyedCount { get; }
        /// <summary>
        /// 0 if no bid has been placed this round.
        /// </summary>
        public int LastBidThisRound { get; }

        public PublicBot(int discsInHandCount, int discsPlayedCount, Discs discsRevealed, int discsDestroyedCount, int lastBidThisRound)
        {
            DiscsInHandCount = discsInHandCount;
            DiscsPlayedCount = discsPlayedCount;
            DiscsRevealed = discsRevealed;
            DiscsDestroyedCount = discsDestroyedCount;
            LastBidThisRound = lastBidThisRound;
        }
    }
}
