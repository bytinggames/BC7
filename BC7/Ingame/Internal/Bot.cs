namespace BC7
{
    public class Bot
    {
        internal BotBrain Brain { get; }
        public BotData Data { get; }
        internal BotVisual Visual { get; }

        internal Bot(BotBrain brain, BotData data, BotVisual visual)
        {
            Brain = brain;
            Data = data;
            Visual = visual;
        }
    }
}
