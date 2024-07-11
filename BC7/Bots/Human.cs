namespace BC7
{
    public class Human : Bot
    {
        private readonly IHumanInput input;

        public Human(IHumanInput input)
        {
            this.input = input;
        }

        protected override DecisionPhase1 GetDecisionPhase1()
        {
            return DecisionPhase1.PlayFlower();
        }

        protected override DecisionPhase2 GetDecisionPhase2()
        {
            return DecisionPhase2.Pass();
        }
    }
}
