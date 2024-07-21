namespace BC7
{
    public class Human : BotBrain
    {
        private readonly IHumanInput input;

        public Human(PublicGame game, IHumanInput input)
            : base(game)
        {
            this.input = input;
        }

        protected override Disc Step1_FirstDisc()
        {
            return Disc.Flower;
        }

        protected override DiscOrChallenge Step2A_DiscOrChallenge()
        {
            return DiscOrChallenge.Flower();
        }

        protected override IncreaseOrPass Step2B_IncreaseOrPass()
        {
            return IncreaseOrPass.Pass();
        }

        protected override Disc OnFail_ChooseOwnDiscToDestroy()
        {
            return Disc.Flower;
        }
    }
}
