using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    public class PublicGame
    {
        public List<PublicBot> BotsAlive { get; }
        public List<PublicBot> BotsDead { get; }
        /// <summary>The index of each bot in this list is the same as the ID of the bot. BotsAll[id].ID == id is always true.</summary>
        public List<PublicBot> BotsAll { get; }

        public PublicGame(List<PublicBot> botsAlive, List<PublicBot> botsDead, List<PublicBot> botsAll)
        {
            BotsAlive = botsAlive;
            BotsDead = botsDead;
            BotsAll = botsAll;
        }

        public PublicBot GetBotByID(int id)
        {
            return BotsAll[id];
        }

        public List<PublicBot> GetAliveOpponentBots(int myID)
        {
            return BotsAlive.Where(f => f.ID != myID).ToList();
        }
    }
}
