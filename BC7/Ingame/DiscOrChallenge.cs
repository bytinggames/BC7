namespace BC7
{
    public class DiscOrChallenge
    {
        public Disc? DiscToPlay { get; }
        public int ChallengeBetAmount { get; }

        public DiscOrChallenge(Disc disc)
        {
            DiscToPlay = disc;
        }

        public DiscOrChallenge(int betAmount)
        {
            if (betAmount <= 0)
            {
                betAmount = 1;
            }
            ChallengeBetAmount = betAmount;
        }

        public static DiscOrChallenge Flower() => new DiscOrChallenge(Disc.Flower);
        public static DiscOrChallenge Skull() => new DiscOrChallenge(Disc.Skull);
        public static DiscOrChallenge Bet(int amount) => new DiscOrChallenge(amount);
    }


    // decision 1:
    // one of your cards OR say a number [1; infinity)

    // decision 2:
    // say no number (0) OR increase the number (>0)
}
