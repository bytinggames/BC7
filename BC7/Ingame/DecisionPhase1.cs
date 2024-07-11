using System;
using System.ComponentModel;

namespace BC7
{
    public class DecisionPhase1
    {
        public int Action { get; }

        private DecisionPhase1(int betAmount)
        {
            Action = betAmount;
        }

        public static DecisionPhase1 PlayFlower()
        {
            return new DecisionPhase1(0);
        }

        public static DecisionPhase1 PlaySkull()
        {
            return new DecisionPhase1(-1);
        }

        public static DecisionPhase1 Bet(int amount)
        {
            if (amount <= 0)
            {
                amount = 1;
            }
            return new DecisionPhase1(amount);
        }
    }

    public class DecisionPhase2
    {
        public int BetAmount { get; }

        private DecisionPhase2(int betAmount)
        {
            BetAmount = betAmount;
        }

        public static DecisionPhase2 Pass()
        {
            return new DecisionPhase2(0);
        }
        public static DecisionPhase2 Bet(int amount)
        {
            if (amount < 0)
            {
                amount = 0;
            }
            return new DecisionPhase2(amount);
        }
    }

    public enum _DecisionPhase1 : int
    {
        PlaySkull = -1,
        PlayFlower = 0,
        Bet1 = 1,
        Bet2 = 2,
        Bet3 = 3,
        Bet4 = 4,
        Bet5 = 5,
        Bet6 = 6,
        Bet7 = 7,
        Bet8 = 8,
        Bet9 = 9,
        Bet10 = 10,
        Bet11 = 11,
        Bet12 = 12,
        Bet13 = 13,
        Bet14 = 14,
        Bet15 = 15,
        Bet16 = 16,
        Bet17 = 17,
        Bet18 = 18,
        Bet19 = 19,
        Bet20 = 20,
        Bet21 = 21,
        Bet22 = 22,
        Bet23 = 23,
        Bet24 = 24,
        Bet25 = 25,
        Bet26 = 26,
        Bet27 = 27,
        Bet28 = 28,
        Bet29 = 29,
        Bet30 = 30,
        Bet31 = 31,
        Bet32 = 32,
        Bet33 = 33,
        Bet34 = 34,
        Bet35 = 35,
        Bet36 = 36,
        Bet37 = 37,
        Bet38 = 38,
        Bet39 = 39,
        Bet40 = 40,
        Bet41 = 41,
        Bet42 = 42,
        Bet43 = 43,
        Bet44 = 44,
        Bet45 = 45,
        Bet46 = 46,
        Bet47 = 47,
        Bet48 = 48,
        Bet49 = 49,
        Bet50 = 50,
        Bet51 = 51,
        Bet52 = 52,
        Bet53 = 53,
        Bet54 = 54,
        Bet55 = 55,
        Bet56 = 56,
        Bet57 = 57,
        Bet58 = 58,
        Bet59 = 59,
        Bet60 = 60,
        Bet61 = 61,
        Bet62 = 62,
        Bet63 = 63,
        Bet64 = 64,
        Bet65 = 65,
        Bet66 = 66,
        Bet67 = 67,
        Bet68 = 68,
        Bet69 = 69,
        Bet70 = 70,
        Bet71 = 71,
        Bet72 = 72,
        Bet73 = 73,
        Bet74 = 74,
        Bet75 = 75,
        Bet76 = 76,
        Bet77 = 77,
        Bet78 = 78,
        Bet79 = 79,
        Bet80 = 80,
        Bet81 = 81,
        Bet82 = 82,
        Bet83 = 83,
        Bet84 = 84,
        Bet85 = 85,
        Bet86 = 86,
        Bet87 = 87,
        Bet88 = 88,
        Bet89 = 89,
        Bet90 = 90,
        Bet91 = 91,
        Bet92 = 92,
        Bet93 = 93,
        Bet94 = 94,
        Bet95 = 95,
        Bet96 = 96,
        Bet97 = 97,
        Bet98 = 98,
        Bet99 = 99,
        Bet100 = 100
        // max player count (9) * 4 + 1
    }

    // decision 1:
    // one of your cards OR say a number [1; infinity)

    // decision 2:
    // say no number (0) OR increase the number (>0)
}
