﻿using System;
using System.Collections.Generic;

namespace BC7
{
    public abstract class BotBrain
    {
        protected readonly PublicGame game;

        protected Discs Hand { get; private set; }
        protected Discs Played { get; private set; }
        protected Discs Revealed { get; private set; }
        protected Discs Destroyed { get; private set; }

        public BotBrain(PublicGame game)
        {
            this.game = game;
        }

        public abstract Disc Step1_FirstDisc();
        public abstract DiscOrChallenge Step2A_DiscOrChallenge();
        public abstract IncreaseOrPass Step2B_IncreaseOrPass(int heighestBet);
        /// <summary>
        /// Return the ID of the chosen player. This is only executed, after all your own discs have been revealed and if there are more than 1 possible players to choose from.
        /// </summary>
        public abstract int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom);
        public abstract Disc OnFail_ChooseOwnDiscToDestroy();
    }
}
