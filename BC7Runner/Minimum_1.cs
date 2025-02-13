﻿namespace BC7Runner
{
    internal class Minimum_1 : BotBrain
    {
        protected override void Initialize()
        {
        }

        public override Disc Step1_FirstDisc()
        {
            return Disc.Skull;
        }

        public override DiscOrBet Step2A_DiscOrBet()
        {
            return DiscOrBet.Bet(3);
        }

        public override IncreaseOrPass Step2B_IncreaseOrPass(int highestBet)
        {
            return IncreaseOrPass.Pass();
        }

        public override int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom)
        {
            return playerIDsToChooseFrom[0];
        }

        public override Disc OnFail_ChooseOwnDiscToDestroy()
        {
            return Disc.Flower;
        }

        public override void OnRoundEnd()
        {
        }
    }
}
