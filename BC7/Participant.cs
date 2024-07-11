using System;

namespace BC7
{
    internal class Participant
    {
        public Type BotType { get; }

        public Participant(Type botType)
        {
            BotType = botType;
        }
    }
}