namespace BC7Runner.Round1
{
    internal class BotExample : BotBrain
    {
        public BotExample(PublicGame game, int id)
            : base(game, id)
        {
            // initialization if you want some
        }

        public override Disc Step1_FirstDisc()
        {
            // TODO: add some logic here

            // get data by typing "game."
            // example for getting all opponents: game.GetAliveOpponentBots(ID);

            return Disc.Skull;
            //return Disc.Flower;
        }

        public override DiscOrBet Step2A_DiscOrBet()
        {
            // TODO: add some nonsense here

            return DiscOrBet.Bet(3);
            //return DiscOrBet.Flower();
            //return DiscOrBet.Skull();
        }

        public override IncreaseOrPass Step2B_IncreaseOrPass(int heighestBet)
        {
            // TODO: add some sense here

            return IncreaseOrPass.Pass();
            //return IncreaseOrPass.Bet(3);
        }

        public override int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom)
        {
            // TODO: add some thing here

            return playerIDsToChooseFrom[0];
        }

        public override Disc OnFail_ChooseOwnDiscToDestroy()
        {
            // TODO: not that important... I think? Not sure, you decide!

            return Disc.Flower;
            //return Disc.Skull;
        }
    }
}
