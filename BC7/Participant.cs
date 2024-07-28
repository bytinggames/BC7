using System;

namespace BC7
{
    internal class Participant
    {
        public Type BotType { get; }
        public int Score { get; set; }

        public Participant(Type botType)
        {
            BotType = botType;
        }
    }
}