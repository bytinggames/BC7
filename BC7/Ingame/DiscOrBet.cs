namespace BC7
{
    public class DiscOrBet
    {
        public Disc? DiscToPlay { get; }
        public int ChallengeBetAmount { get; }

        public DiscOrBet(Disc disc)
        {
            DiscToPlay = disc;
        }

        public DiscOrBet(int betAmount)
        {
            if (betAmount <= 0)
            {
                betAmount = 1;
            }
            ChallengeBetAmount = betAmount;
        }

        public static DiscOrBet Flower() => new DiscOrBet(Disc.Flower);
        public static DiscOrBet Skull() => new DiscOrBet(Disc.Skull);
        public static DiscOrBet Bet(int amount) => new DiscOrBet(amount);
    }


    // decision 1:
    // one of your cards OR say a number [1; infinity)

    // decision 2:
    // say no number (0) OR increase the number (>0)
}
