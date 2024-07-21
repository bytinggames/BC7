using System;
using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    internal class SkullGame : IDraw
    {
        public List<Bot> Bots { get; } = new();
        public List<Bot> BotsDead { get; } = new();
        public List<Bot> BotByID { get; }
        private Random rand = new();

        public SkullGame(List<BotBrain> brains)
        {
            for (int i = 0; i < brains.Count; i++)
            {
                Bots.Add(new Bot(brains[i], new BotData(i)));
            }

            BotByID = Bots.ToList();
        }

        internal IEnumerable<object?> DoGameStep()
        {
            int turnIndex = rand.Next(Bots.Count);
            do
            {
                // step 1 - turn preparation
                foreach (Bot bot in Bots)
                {
                    Disc disc = bot.Brain.Step1_FirstDisc();
                    bot.Data.DiscsInHand.TryMoveDiscTo(disc, bot.Data.DiscsPlayed);
                }

                // step 2 - ADDING extra discs or CHALLENGE
                int heighestPossibleBet = Bots.Sum(f => f.Data.DiscsPlayed.Count());
                int bet = 0;
                Bot? challenger = null;
                while (true)
                {
                    yield return null; // before player taking step 2A

                    var bot = Bots[turnIndex];
                    var discOrChallenge = bot.Brain.Step2A_DiscOrBet();

                    if (discOrChallenge.DiscToPlay == null // either bot decided to not play a disk
                        || bot.Data.DiscsInHand.TryMoveDiscTo(discOrChallenge.DiscToPlay.Value, bot.Data.DiscsPlayed) == null) // or decided to and wasn't able, because he had no discs 
                    {
                        challenger = bot;
                        bet = discOrChallenge.ChallengeBetAmount;
                        if (bet > heighestPossibleBet)
                        {
                            bet = heighestPossibleBet;
                        }
                        else if (bet < 1)
                        {
                            bet = 1;
                        }
                        break;
                    }

                    // next player

                    turnIndex = (turnIndex + 1) % Bots.Count;
                }

                // step 2.5 - increase the bid or pass
                // loop until only all players but one have passed
                bool heighestPossibleBetIncreasedBy1 = false;
                while (Bots.Count(f => f.Data.Passed) == Bots.Count - 1)
                {
                    yield return null; // before player taking step 2B

                    var bot = Bots[turnIndex];
                    var increaseOrPass = bot.Brain.Step2B_IncreaseOrPass(bet);
                    if (increaseOrPass.BetAmount == 0)
                    {
                        bot.Data.Passed = true;
                    }
                    else
                    {
                        challenger = bot;
                        bet = Math.Clamp(increaseOrPass.BetAmount, bet + 1, heighestPossibleBet);

                        if (heighestPossibleBetIncreasedBy1)
                        {
                            break; // no more bets possible. the heighest was just bet.
                        }
                        else if (bet == heighestPossibleBet)
                        {
                            // unlock the last bet
                            // allow for one last bet, even though the heighest possible bet has already been done
                            heighestPossibleBet++;
                            heighestPossibleBetIncreasedBy1 = true;
                        }

                    }
                }

                // step 3 - the attempt
                Bot? failedOnPlayer = null;
                while (Bots.Sum(f => f.Data.DiscsRevealed.Count()) < bet // you have still discs to flip around
                    && Bots.Any(f => f.Data.DiscsPlayed.Count() > 0)) // there are still concealed discs available to flip
                {
                    yield return null; // before revealing a disc
                    BotData playerDataToRevealFrom;
                    if (challenger.Data.DiscsPlayed.Count() > 0)
                    {
                        // first reveal own discs
                        playerDataToRevealFrom = challenger.Data;
                    }
                    else
                    {
                        // then let him choose which discs to reveal next
                        int[] options = Bots.Where(f => f.Data.DiscsPlayed.Count() > 0).Select(f => f.Data.ID).ToArray();
                        options.Shuffle(rand);

                        int playerIDToRevealFrom;
                        if (options.Length > 1)
                        {
                            playerIDToRevealFrom = challenger.Brain.Step3_ChoosePlayerToFlip1Disc(options);
                            if (!options.Contains(playerIDToRevealFrom))
                            {
                                playerIDToRevealFrom = options[0];
                            }
                        }
                        else
                        {
                            playerIDToRevealFrom = options[0]; // automatically choose the only option available
                        }
                        playerDataToRevealFrom = Bots[playerIDToRevealFrom].Data;
                    }
                    Disc revealed = playerDataToRevealFrom.DiscsPlayed.MoveTopTo(playerDataToRevealFrom.DiscsRevealed);
                    if (revealed == Disc.Skull)
                    {
                        failedOnPlayer = BotByID[playerDataToRevealFrom.ID];
                        break;
                    }
                }

                // prepare all bots for next round
                foreach (Bot bot in Bots)
                {
                    // reset bots Passed variablex
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
                }

                // challenger starts next turn per default
                turnIndex = Bots.IndexOf(challenger);

                // punish or reward the challenger
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
                        // player loses and gets removed from the game
                        challenger.Data.DiscsInHand.TryMoveDiscTo(Disc.Flower /* any */, challenger.Data.DiscsDestroyed);
                        challenger.Data.Alive = false;
                        Bots.Remove(challenger);
                        BotsDead.Add(challenger);

                        // if the challenger is removed from the game, then the next players turn is the one whose disc the challenger died from
                        // if that disc was his own, the dying player chooses who's next (in our case we determine that randomly)
                        if (failedOnPlayer == challenger)
                        {
                            turnIndex = rand.Next(Bots.Count);
                        }
                        else
                        {
                            turnIndex = Bots.IndexOf(failedOnPlayer);
                        }
                    }
                    else
                    {
                        if (failedOnPlayer == challenger)
                        {
                            Disc discToDestroy = challenger.Brain.OnFail_ChooseOwnDiscToDestroy();
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
                }
            } while (Bots.All(f => f.Data.Successes < 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Yellow);
        }
    }
}
