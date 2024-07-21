﻿namespace BC7
{
    internal interface IDiscs
    {
        public int Flowers { get; }
        public int Skulls { get; }

        public void AddFlower();
        public void AddSkull();
    }

    internal static class IDsiscsExtension
    {
        public static int Count(this IDiscs discs) => discs.Flowers + discs.Skulls;
        public static void Add(this IDiscs discs, Disc disc)
        {
            if (disc == Disc.Flower)
            {
                discs.AddFlower();
            }
            else
            {
                discs.AddSkull();
            }
        }
    }
}
