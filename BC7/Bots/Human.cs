using System;
using System.Linq;

namespace BC7
{
    public class Human : BotBrain
    {
        private readonly KeyInput keys;

        int numberInput = 0;

        public Human(KeyInput keys)
        {
            this.keys = keys;
        }

        protected override void Initialize()
        {

        }

        public override Disc Step1_FirstDisc()
        {
            if (keys.F.Pressed)
            {
                Thoughts = "";
                return Disc.Flower;
            }
            else if (keys.S.Pressed)
            {
                Thoughts = "";
                return Disc.Skull;
            }
            Thoughts = "Play [f]lower or [s]kull?";
            return (Disc)int.MinValue;
        }

        public override DiscOrBet Step2A_DiscOrBet()
        {
            if (keys.F.Pressed)
            {
                numberInput = 0;
                Thoughts = "";
                return DiscOrBet.Flower();
            }
            else if (keys.S.Pressed)
            {
                numberInput = 0;
                Thoughts = "";
                return DiscOrBet.Skull();
            }
            else if (keys.Enter.Pressed)
            {
                int n = numberInput;
                numberInput = 0;
                Thoughts = "";
                return DiscOrBet.Bet(n);
            }
            else
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (keys.Number(i).Pressed)
                    {
                        numberInput *= 10;
                        numberInput += i;
                    }
                }
            }

            Thoughts = "Play [f]lower, [s]kull or Challenge [amount]?";
            return null;
        }

        public override IncreaseOrPass Step2B_IncreaseOrPass(int heighestBet)
        {
            if (keys.P.Pressed)
            {
                numberInput = 0;
                Thoughts = "";
                return IncreaseOrPass.Pass();
            }
            else if (keys.Enter.Pressed)
            {
                int n = numberInput;
                numberInput = 0;
                Thoughts = "";
                return IncreaseOrPass.Bet(n);
            }
            else
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (keys.Number(i).Pressed)
                    {
                        numberInput *= 10;
                        numberInput += i;
                    }
                }
            }

            Thoughts = "Increase bet to [amount] or [p]ass?";
            return null;
        }

        public override int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom)
        {

            for (int i = 0; i <= 9; i++)
            {
                if (keys.Number(i).Pressed)
                {
                    if (playerIDsToChooseFrom.Contains(i))
                    {
                        Thoughts = "";
                        return i;
                    }
                }
            }
            Thoughts = "Which player to flip? " + string.Join(", ", playerIDsToChooseFrom);
            return int.MinValue;
        }

        public override Disc OnFail_ChooseOwnDiscToDestroy()
        {
            if (keys.F.Pressed)
            {
                Thoughts = "";
                return Disc.Flower;
            }
            else if (keys.S.Pressed)
            {
                Thoughts = "";
                return Disc.Skull;
            }
            Thoughts = "Destroy [f]lower or [s]kull?";
            return (Disc)int.MinValue;
        }
    }
}
