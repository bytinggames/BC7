using System;

namespace BC7
{
    public class Human : BotBrain
    {
        public Human(PublicGame game, int id)
            : base(game, id)
        {
        }

        public override Disc Step1_FirstDisc()
        {
            Console.WriteLine("Play [f]lower or [s]kull?");
            return Console.ReadLine()?.ToUpper() == "F" ? Disc.Flower : Disc.Skull;
        }

        public override DiscOrBet Step2A_DiscOrBet()
        {
            Console.WriteLine("Play [f]lower, [s]kull or Challenge [amount]?");
            string? input = Console.ReadLine();
            if (input?.ToUpper() == "F")
            {
                return DiscOrBet.Flower();
            }
            else if (int.TryParse(input, out int number))
            {
                return DiscOrBet.Bet(number);
            }
            else
            {
                return DiscOrBet.Skull();
            }
        }

        public override IncreaseOrPass Step2B_IncreaseOrPass(int heighestBet)
        {
            Console.WriteLine("Increase bet to [amount] or [p]ass?");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                return IncreaseOrPass.Bet(number);
            }
            else
            {
                return IncreaseOrPass.Pass();
            }
        }

        public override int Step3_ChoosePlayerToFlip1Disc(int[] playerIDsToChooseFrom)
        {
            Console.WriteLine("Which player to flip? " + string.Join(", ", playerIDsToChooseFrom));
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                return number;
            }
            return playerIDsToChooseFrom[0];
        }

        public override Disc OnFail_ChooseOwnDiscToDestroy()
        {
            Console.WriteLine("Destroy [f]lower or [s]kull?");
            return Console.ReadLine()?.ToUpper() == "F" ? Disc.Flower : Disc.Skull;
        }
    }
}
