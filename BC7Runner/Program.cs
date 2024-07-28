using BC7Runner.Round1;
using System;
using System.Collections.Generic;

namespace BC7Runner
{
    internal class Program
    {
        public static void Main()
        {
            // the participants
            List<Type> bots = new List<Type>()
            {
                typeof(Human),
                typeof(Example_1),
            };

            // run the game
            BC7.Program.Run(bots);
        }
    }
}
