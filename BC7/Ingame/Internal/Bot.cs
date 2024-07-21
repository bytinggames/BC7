namespace BC7
{
    internal class Bot
    {
        public BotBrain Brain { get; }
        public BotData Data { get; }

        public Bot(BotBrain brain, BotData data)
        {
            Brain = brain;
            Data = data;
        }
    }
}
