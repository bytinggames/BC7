﻿//using System;
//using System.Collections.Generic;

//namespace BC7
//{
//    public class Tournament
//    {
//        int botsPerBattle;
//        int iterations;
//        Type envType;
//        Type[] botTypes;
//        bool visible;
//        OutputMode outputMode;
//        int? sameSeed;

//        public Tournament(Type[] bots, int botsPerBattle, int iterations, bool visible, OutputMode outputMode, int? sameSeed = null)
//        {
//            if (sameSeed.HasValue)
//                Console.WriteLine("seed: " + sameSeed.Value);

//            if (botsPerBattle > bots.Length)
//                botsPerBattle = bots.Length;

//            this.botTypes = bots;
//            this.botsPerBattle = botsPerBattle;
//            this.iterations = iterations;
//            this.visible = visible;
//            this.outputMode = outputMode;
//            this.sameSeed = sameSeed;
//        }

//        public float[] Run()
//        {
//            int j;

//            float[] totalScores = new float[botTypes.Length];

//            int[] currentBots = new int[botsPerBattle];
//            for (int i = 0; i < botsPerBattle; i++)
//                currentBots[i] = i;

//            do
//            {
//                Random rand = sameSeed == null ? new Random() : new Random(sameSeed.Value);

//                //int[] indices = new int[botsPerBattle];
//                List<Type> currentTypes = new List<Type>();
//                List<int> indices = new List<int>();
//                for (int i = 0; i < botsPerBattle; i++)
//                {
//                    j = rand.Next(i);
//                    indices.Insert(j, currentBots[i]);
//                    currentTypes.Insert(j, botTypes[currentBots[i]]);
//                }



//                RunManager runManager = new RunManager(envType, currentTypes.ToArray(), iterations, visible, outputMode);
//                float[] scores = runManager.Loop();

//                if (outputMode >= OutputMode.ResultOfEveryBattle)
//                {
//                    Console.ForegroundColor = ConsoleColor.White;
//                    Console.WriteLine("\n_________________________________________________\nBattle Result:");
//                    for (int i = 0; i < scores.Length; i++)
//                    {
//                        string name = currentTypes[i].Name;
//                        Console.WriteLine(name + ": " + scores[i]);
//                    }
//                }

//                if (scores.Length > 1)
//                {
//                    for (int i = 0; i < scores.Length; i++)
//                    {
//                        int index = indices[i];

//                        for (j = 0; j < scores.Length; j++)
//                        {
//                            if (i != j && scores[i] > scores[j])
//                            {
//                                totalScores[index]++;
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    int index = indices[0];
//                    totalScores[index] += scores[0];
//                }

//                if (!visible)
//                {
//                    Console.WriteLine("<Enter>");
//                    Console.ReadLine();
//                }
//                int max = botTypes.Length;
//                for (j = botsPerBattle - 1; j >= 0; j--)
//                {
//                    currentBots[j]++;
//                    if (currentBots[j] >= max)
//                        max--;
//                    else
//                        break;
//                }

//                if (j == -1)
//                    break;

//                for (j++; j < botsPerBattle; j++)
//                {
//                    currentBots[j] = currentBots[j - 1] + 1;
//                }

//            } while (true);

//            if (outputMode >= OutputMode.ResultOfEveryBattle)
//            {
//                Console.ForegroundColor = ConsoleColor.Green;
//                Console.WriteLine("\n_________________________________________________________\nTournament Result:");
//                for (int i = 0; i < totalScores.Length; i++)
//                {
//                    string name = botTypes[i].Name;
//                    Console.WriteLine(name + ": " + totalScores[i]);
//                }
//            }

//            return totalScores;
//        }
//    }
//}
