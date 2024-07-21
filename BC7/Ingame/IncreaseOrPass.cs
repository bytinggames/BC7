namespace BC7
{
    public class IncreaseOrPass
    {
        public int BetAmount { get; }

        private IncreaseOrPass(int betAmount)
        {
            BetAmount = betAmount;
        }

        public static IncreaseOrPass Pass()
        {
            return new IncreaseOrPass(0);
        }
        public static IncreaseOrPass Bet(int amount)
        {
            if (amount < 0)
            {
                amount = 0;
            }
            return new IncreaseOrPass(amount);
        }
    }

    // decision 1:
    // one of your cards OR say a number [1; infinity)

    // decision 2:
    // say no number (0) OR increase the number (>0)
}
