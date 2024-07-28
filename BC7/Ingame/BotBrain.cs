using System;
using System.Security.Cryptography;
using System.Text;

namespace BC7
{
    public abstract class BotBrain
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected SkullGame Game { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected int ID { get; private set; }
        public Color Color { get; protected set; }
        internal Color ColorWithoutHue => Color.ToHSV().SetSaturation(0f).TimesValue(100f / 77f /* default value of the blue disc color */).ToRGB();
        public string Thoughts { get; protected set; } = "";
        public BotData Data => Game.GetBotByID(ID).Data;
        protected DiscsPublic DiscsInHand => new DiscsPublic(Data.DiscsInHand);
        protected DiscsStackPublic DiscsPlayed => new DiscsStackPublic(Data.DiscsPlayed);
        protected DiscsPublic DiscsDestroyed => new DiscsPublic(Data.DiscsDestroyed);

        internal void InitializeBase(SkullGame game, int id)
        {
            this.Game = game;
            this.ID = id;

            int seed;
            using (var sha = SHA1.Create())
            {
                var result = sha.ComputeHash(Encoding.UTF8.GetBytes(GetType().Name));
                seed = BitConverter.ToInt32(result);
            }
            Random rand = new Random(seed);
            Color = new HSVColor(rand.NextSingle() * 360f, 1f, 1f).ToRGB();// rand.NextColor();

            Initialize();
        }

        protected abstract void Initialize();

        /// <summary>Everyone has to choose a disc to play as their first one.</summary>
        public abstract Disc Step1_FirstDisc();
        /// <summary>Either play another disc or be the one who starts to bet he can flip X amount of flowers.</summary>
        public abstract DiscOrBet Step2A_DiscOrBet();
        /// <summary>Bet you can flip more flowers or pass.</summary>
        public abstract IncreaseOrPass Step2B_IncreaseOrPass(int highestBet);
        /// <summary>
        /// Choose a player you want to flip a disc from.
        /// Return the ID of the chosen player. This is only executed, if you have a choice.
        /// </summary>
        public abstract int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom);

        /// <summary>Gets executed before the challenger is punished (loses a disc) or rewarded (gains a point).</summary>
        public abstract void OnRoundEnd();

        /// <summary>When you fail because you flipped your own skull, you have the advantage of choosing what disc to destroy.</summary>
        public abstract Disc OnFail_ChooseOwnDiscToDestroy();
    }
}
