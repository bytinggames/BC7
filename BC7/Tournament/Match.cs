using System;

namespace BC7
{
    public class Match
    {
        internal float[] Scores { get; }
        public Type[] BotTypes { get; }

        public Match(Type[] botTypes)
        {
            Scores = new float[botTypes.Length];
            BotTypes = botTypes;
        }
    }
}
