
using System;
using System.Security.Cryptography;
using System.Text;

namespace BC7
{
    public abstract class BotBrain
    {
        protected Discs Hand { get; private set; }
        protected Discs Played { get; private set; }
        protected Discs Revealed { get; private set; }
        protected Discs Destroyed { get; private set; }
        protected SkullGame Game { get; private set; }
        protected int ID { get; private set; }
        public Color Color { get; protected set; }
        internal Color ColorWithoutHue => Color.ToHSV().SetSaturation(0f).TimesValue(100f / 77f /* default value of the blue disc color */).ToRGB();
        public string Thoughts { get; protected set; } = "";

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

        public abstract Disc Step1_FirstDisc();
        public abstract DiscOrBet Step2A_DiscOrBet();
        public abstract IncreaseOrPass Step2B_IncreaseOrPass(int heighestBet);
        /// <summary>
        /// Return the ID of the chosen player. This is only executed, after all your own discs have been revealed and if there are more than 1 possible players to choose from.
        /// </summary>
        public abstract int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom);
        public abstract Disc OnFail_ChooseOwnDiscToDestroy();
    }
}
