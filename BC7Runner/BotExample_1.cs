namespace BC7Runner.Round1
{
    internal class BotExample_1 : BotBrain
    {
        protected override void Initialize()
        {
            // initialization if you want some
        }

        public override Disc Step1_FirstDisc()
        {
            // TODO

            // example: always return a skull if there are less than 3 bots alive:
            if (Game.GetAliveOpponentBots(ID).Count < 3)
            {
                return Disc.Skull;
            }
            else
            {
                return Disc.Flower;
            }
        }

        public override DiscOrBet Step2A_DiscOrBet()
        {
            // TODO

            return DiscOrBet.Bet(3);
            //              .Flower();
            //              .Skull();
        }

        public override IncreaseOrPass Step2B_IncreaseOrPass(int heighestBet)
        {
            // TODO

            return IncreaseOrPass.Pass();
            //                   .Bet(3);
        }

        public override int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom)
        {
            // TODO

            return playerIDsToChooseFrom[0];
        }

        public override Disc OnFail_ChooseOwnDiscToDestroy()
        {
            // not that important... I think? Not sure, you decide!

            return Disc.Flower;
            //         .Skull;
        }
    }
}
