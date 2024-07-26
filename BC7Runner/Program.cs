using BC7Runner.Round1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BC7Runner
{
    internal class Program
    {
        public static void Main()
        {
            Debug.WriteLine("test");
            // the participants
            List<Type> bots = new List<Type>()
            {
                typeof(Human),
                typeof(Human),
            };

            // set current directory to the BC7 output path, so the Game can run
            GotoGameDirectory("BC7");
            BC7.Program.Run(bots);
        }

        private static void GotoGameDirectory(string projectName)
        {
            Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", projectName, "bin", "Debug", "net8.0");
        }
    }
}
