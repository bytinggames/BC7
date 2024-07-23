using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    public class PublicGame
    {
        public List<PublicBot> BotsAlive { get; private set; }
        public List<PublicBot> BotsDead { get; private set; }
        /// <summary>The index of each bot in this list is the same as the ID of the bot. BotsAll[id].ID == id is always true.</summary>
        public List<PublicBot> BotsAll { get; private set; }

        public PublicGame(List<PublicBot> botsAlive, List<PublicBot> botsDead, List<PublicBot> botsAll)
        {
            BotsAlive = botsAlive;
            BotsDead = botsDead;
            BotsAll = botsAll;
        }

        public PublicGame(int botCount)
        {
        }

        //internal void UpdateState(SkullGame game)
        //{
        //    BotsAlive = UpdateBotsList(game.BotsAlive);
        //    UpdateBotsList(game);
        //    BotsDead = new();
        //    BotsAll = new();
        //}

        //private List<PublicBot> UpdateBotsList(List<Bot> botsSource)
        //{
        //    return botsSource.Select(
        //        f => new PublicBot(
        //            f.Data.ID,
        //            f.Data.DiscsInHand.Count(),
        //            f.Data.DiscsPlayed.Count(),
        //            f.Data.DiscsRevealed.Clone(),
        //            f.Data.DiscsDestroyed.Count(),
        //            f.Data.LastBidThisRound
        //        )
        //    ).ToList();
        //}

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
