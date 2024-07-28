using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BC7
{
    public class SkullGame
    {
        /// <summary>Only contains bots that are still in the game -> alive.</summary>
        public List<Bot> BotsAlive { get; } = new();
        /// <summary>Every bot that was eliminated from the game.</summary>
        public List<Bot> BotsDead { get; } = new();
        /// <summary>All bots. This list doesn't change during a game.</summary>
        public List<Bot> BotsDeadAndAlive { get; }

        public Bot GetBotByID(int id)
        {
            return BotsDeadAndAlive[id];
        }

        /// <summary>Returns all alive bots but the one with the given 'myID'.</summary>
        public List<Bot> GetAliveOpponents(int myID)
        {
            return BotsAlive.Where(f => f.Data.ID != myID).ToList();
        }

        #region internal

        private Random rand = new();
        private readonly IResolution res;
        private readonly int gameIndex;
        private readonly Sizes sizes;

        internal SkullGame(List<BotBrain> brains, IResolution res, int gameIndex, BotVisualAssets botVisualAssets)
        {
            sizes = new(res);
            for (int i = 0; i < brains.Count; i++)
            {
                BotsAlive.Add(new Bot(brains[i], new BotData(i), new BotVisual(botVisualAssets, sizes)));
            }

            BotsDeadAndAlive = BotsAlive.ToList();

            for (int i = 0; i < brains.Count; i++)
            {
                brains[i].InitializeBase(this, i);
            }

            this.res = res;
            this.gameIndex = gameIndex;
        }

        internal IEnumerable<LoopAction> GameLoop()
        {
            int turnIndex = gameIndex % BotsDeadAndAlive.Count;
            do
            {
                // step 1 - turn preparation
                foreach (Bot bot in BotsAlive)
                {
                    if (bot.Brain is Human)
                    {
                        yield return LoopAction.Wait1Frame; // wait 1 frame or else all humans would react on the same input during a single frame
                    }
                    Disc disc;
                    while (true)
                    {
                        try
                        {
                            disc = bot.Brain.Step1_FirstDisc();
                        }
                        catch (Exception e) when (MySettings.SilentExceptions)
                        {
                            Debug.WriteLine(e.Message + "\n" + e.ToString());
                            disc = Disc.Skull;
                        }

                        if ((int)disc != int.MinValue)
                        {
                            break;
                        }
                        yield return LoopAction.Wait1Frame;
                    }
                    bot.Data.DiscsInHand.TryMoveDiscTo(disc, bot.Data.DiscsPlayed);
                }

                // step 2 - ADDING extra discs or CHALLENGE
                int highestPossibleBet;
                int bet = 0;
                Bot? challenger = null;
                while (true)
                {
                    yield return LoopAction.WaitForEnter; // before player taking step 2A

                    highestPossibleBet = BotsAlive.Sum(f => f.Data.DiscsPlayed.Count());
                    var bot = BotsAlive[turnIndex];

                    DiscOrBet? discOrChallenge = null;
                    while (true)
                    {
                        try
                        {
                            discOrChallenge = bot.Brain.Step2A_DiscOrBet();
                        }
                        catch (Exception e) when (MySettings.SilentExceptions)
                        {
                            Debug.WriteLine(e.Message + "\n" + e.ToString());
                            discOrChallenge = DiscOrBet.Bet(1);
                        }
                        if (discOrChallenge != null)
                        {
                            break;
                        }
                        yield return LoopAction.Wait1Frame;
                    }

                    if (discOrChallenge.DiscToPlay == null // either bot decided to not play a disk ...
                        || bot.Data.DiscsInHand.TryMoveDiscTo(discOrChallenge.DiscToPlay.Value, bot.Data.DiscsPlayed) == null) // or decided to and wasn't able, because he had no discs 
                    {
                        challenger = bot;
                        bet = discOrChallenge.ChallengeBetAmount;
                        if (bet > highestPossibleBet)
                        {
                            bet = highestPossibleBet;
                        }
                        else if (bet < 1)
                        {
                            bet = 1;
                        }
                        bot.Data.LastBidThisRound = bet;
                        break;
                    }

                    // next player
                    turnIndex = (turnIndex + 1) % BotsAlive.Count;
                }

                // next player
                turnIndex = (turnIndex + 1) % BotsAlive.Count;

                // step 2.5 - increase the bid or pass
                // loop until only all players but one have passed
                bool heighestPossibleBetIncreasedBy1 = false;
                if (bet == BotsAlive.Sum(f => f.Data.DiscsPlayed.Count()))
                {
                    // unlock the last bet
                    // allow for one last bet, even though the highest possible bet has already been done
                    highestPossibleBet++;
                    heighestPossibleBetIncreasedBy1 = true;
                }
                while (BotsAlive.Count(f => f.Data.Passed) != BotsAlive.Count - 1)
                {
                    yield return LoopAction.WaitForEnter; // before player taking step 2B

                    var bot = BotsAlive[turnIndex];

                    IncreaseOrPass? increaseOrPass = null;
                    while (true)
                    {
                        try
                        {
                            increaseOrPass = bot.Brain.Step2B_IncreaseOrPass(bet);
                        }
                        catch (Exception e) when (MySettings.SilentExceptions)
                        {
                            Debug.WriteLine(e.Message + "\n" + e.ToString());
                            increaseOrPass = IncreaseOrPass.Pass();
                        }

                        if (increaseOrPass != null)
                        {
                            break;
                        }
                        yield return LoopAction.Wait1Frame;
                    }

                    if (increaseOrPass.BetAmount == 0)
                    {
                        bot.Data.Passed = true;
                    }
                    else
                    {
                        challenger = bot;
                        bet = Math.Clamp(increaseOrPass.BetAmount, Math.Min(bet + 1, highestPossibleBet), highestPossibleBet);
                        bot.Data.LastBidThisRound = bet;

                        if (heighestPossibleBetIncreasedBy1)
                        {
                            break; // no more bets possible. the heighest was just bet.
                        }
                        else if (bet == highestPossibleBet)
                        {
                            // unlock the last bet
                            // allow for one last bet, even though the heighest possible bet has already been done
                            highestPossibleBet++;
                            heighestPossibleBetIncreasedBy1 = true;
                        }
                    }

                    // next player
                    turnIndex = (turnIndex + 1) % BotsAlive.Count;
                }

                // step 3 - the attempt
                Bot? failedOnPlayer = null;
                while (BotsAlive.Sum(f => f.Data.DiscsRevealed.Count()) < bet // you have still discs to flip around
                    && BotsAlive.Any(f => f.Data.DiscsPlayed.Count() > 0)) // there are still concealed discs available to flip
                {
                    yield return LoopAction.WaitForEnter; // before revealing a disc
                    BotData playerDataToRevealFrom;
                    if (challenger.Data.DiscsPlayed.Count() > 0)
                    {
                        // first reveal own discs
                        playerDataToRevealFrom = challenger.Data;
                    }
                    else
                    {
                        // then let him choose which discs to reveal next
                        List<int> optionsList = BotsAlive.Where(f => f.Data.DiscsPlayed.Count() > 0).Select(f => f.Data.ID).ToList();
                        optionsList.Shuffle(rand);
                        int[] options = optionsList.ToArray();

                        int playerIDToRevealFrom;
                        if (options.Length > 1)
                        {
                            while (true)
                            {
                                try
                                {
                                    playerIDToRevealFrom = challenger.Brain.Step3_ChoosePlayerToFlip1Disc(options.ToArray());
                                }
                                catch (Exception e) when (MySettings.SilentExceptions)
                                {
                                    Debug.WriteLine(e.Message + "\n" + e.ToString());
                                    playerIDToRevealFrom = options[0];
                                }

                                if (playerIDToRevealFrom != int.MinValue)
                                {
                                    break;
                                }
                                yield return LoopAction.Wait1Frame;
                            }

                            if (!options.Contains(playerIDToRevealFrom))
                            {
                                playerIDToRevealFrom = options[0];
                            }
                        }
                        else
                        {
                            playerIDToRevealFrom = options[0]; // automatically choose the only option available
                        }
                        playerDataToRevealFrom = GetBotByID(playerIDToRevealFrom).Data;
                    }
                    Disc revealed = playerDataToRevealFrom.DiscsPlayed.MoveTopTo(playerDataToRevealFrom.DiscsRevealed);
                    if (revealed == Disc.Skull)
                    {
                        failedOnPlayer = BotsDeadAndAlive[playerDataToRevealFrom.ID];
                        break;
                    }
                }

                foreach (Bot bot in BotsAlive)
                {
                    try
                    {
                        bot.Brain.OnRoundEnd();
                    }
                    catch (Exception e) when (MySettings.SilentExceptions)
                    {
                        Debug.WriteLine(e.Message + "\n" + e.ToString());
                    }
                }


                yield return LoopAction.WaitForEnter; // after the last disc is revealed

                // prepare all bots for next round
                foreach (Bot bot in BotsAlive)
                {
                    // reset bots Passed variable
                    bot.Data.Passed = false;
                    // take all discs back into hand
                    while (bot.Data.DiscsRevealed.Count() > 0)
                    {
                        bot.Data.DiscsRevealed.MoveTopTo(bot.Data.DiscsInHand);
                    }
                    while (bot.Data.DiscsPlayed.Count() > 0)
                    {
                        bot.Data.DiscsPlayed.MoveTopTo(bot.Data.DiscsInHand);
                    }
                    // reset LastBidThisRound variable
                    bot.Data.LastBidThisRound = 0;
                }

                // bot starts next turn per default
                turnIndex = BotsAlive.IndexOf(challenger);

                // punish or reward the bot
                if (failedOnPlayer == null)
                {
                    // success -> reward
                    challenger.Data.Successes++;
                }
                else
                {
                    // fail -> punish
                    if (challenger.Data.DiscsInHand.Count() == 1)
                    {
                        // player loses and gets removed from the Game
                        EliminatePlayer(challenger, ref turnIndex);

                        // if the challenger is removed from the Game, then the next players turn is the one whose disc the challenger died from
                        // if that disc was his own, the dying player chooses who's next (in our case we determine that randomly)
                        if (failedOnPlayer == challenger)
                        {
                            turnIndex = rand.Next(BotsAlive.Count);
                        }
                        else
                        {
                            turnIndex = BotsAlive.IndexOf(failedOnPlayer);
                        }
                    }
                    else
                    {
                        if (failedOnPlayer == challenger)
                        {
                            Disc discToDestroy;
                            while (true)
                            {
                                try
                                {
                                    discToDestroy = challenger.Brain.OnFail_ChooseOwnDiscToDestroy();
                                }
                                catch (Exception e) when (MySettings.SilentExceptions)
                                {
                                    Debug.WriteLine(e.Message + "\n" + e.ToString());
                                    discToDestroy = Disc.Flower;
                                }
                                if ((int)discToDestroy != int.MinValue)
                                {
                                    break;
                                }
                                yield return LoopAction.Wait1Frame;
                            }

                            challenger.Data.DiscsInHand.TryMoveDiscTo(discToDestroy, challenger.Data.DiscsDestroyed);
                        }
                        else
                        {
                            // remove one disc at random
                            int randomIndex = rand.Next(challenger.Data.DiscsInHand.Count());
                            Disc remove = randomIndex == 0 ? Disc.Skull : Disc.Flower;
                            challenger.Data.DiscsInHand.TryMoveDiscTo(remove, challenger.Data.DiscsDestroyed);
                        }
                    }

                    // if the skull that was revealed was the only disc of that player, then eliminate that player
                    if (failedOnPlayer.Data.Lives == 1 && failedOnPlayer != challenger)
                    {
                        EliminatePlayer(failedOnPlayer, ref turnIndex);
                    }
                }
            } while (BotsAlive.All(f => f.Data.Successes < 2) && BotsAlive.Count > 1);

            if (BotsAlive.Count == 1)
            {
                BotsAlive[0].Data.LastSurvivor = true;
            }
        }

        private void EliminatePlayer(Bot bot, ref int turnIndex)
        {
            int indexOfDyingPlayer = BotsAlive.IndexOf(bot);
            if (turnIndex > indexOfDyingPlayer)
            {
                turnIndex--;
            }
            else if (turnIndex == indexOfDyingPlayer && turnIndex == BotsAlive.Count - 1)
            {
                turnIndex = 0;
            }

            while (bot.Data.DiscsInHand.Count() > 0)
            {
                bot.Data.DiscsInHand.TryMoveDiscTo(Disc.Flower /* any */, bot.Data.DiscsDestroyed);
            }
            while (bot.Data.DiscsRevealed.Count() > 0)
            {
                bot.Data.DiscsRevealed.MoveTopTo(bot.Data.DiscsDestroyed);
            }
            while (bot.Data.DiscsPlayed.Count() > 0)
            {
                bot.Data.DiscsPlayed.MoveTopTo(bot.Data.DiscsDestroyed);
            }
            bot.Data.Alive = false;
            BotsAlive.Remove(bot);
            BotsDead.Add(bot);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            GetPositionsPerBot(spriteBatch).ForEvery(((int i, Vector2 pos) input) =>
            {
                BotsDeadAndAlive[input.i].Visual.Draw1(spriteBatch, input.pos, BotsDeadAndAlive[input.i]);
            });
            GetPositionsPerBot(spriteBatch).ForEvery(((int i, Vector2 pos) input) =>
            {
                BotsDeadAndAlive[input.i].Visual.Draw2(spriteBatch, input.pos, BotsDeadAndAlive[input.i], this);
            });
        }

        private IEnumerable<(int, Vector2)> GetPositionsPerBot(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < BotsDeadAndAlive.Count; i++)
            {
                float angle = i * MathHelper.TwoPi / BotsDeadAndAlive.Count;
                Vector2 pos = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
                pos = pos * 0.5f + new Vector2(0.5f);
                pos.X *= res.Resolution.X - sizes.PlayerSize;
                pos.Y *= res.Resolution.Y - sizes.PlayerSize;
                pos.X += sizes.PlayerSize / 2f;
                pos.Y += sizes.PlayerSize / 2f;

                yield return (i, pos);
            }
        }

        #endregion
    }
}
