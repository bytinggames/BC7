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
                typeof(Example_1),
                typeof(Example_1),
            };

            // run the game
            BC7.Program.Run(bots);
        }
    }
}
