using System;
using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    internal class DiscsStack : IDiscs
    {
        public Stack<Disc> DiscStack { get; } = new();

        public int Flowers => DiscStack.Count(f => f == Disc.Flower);
        public int Skulls => DiscStack.Count(f => f == Disc.Skull);

        public void AddFlower()
        {
            DiscStack.Push(Disc.Flower);
        }

        public void AddSkull()
        {
            DiscStack.Push(Disc.Skull);
        }

        public Disc MoveTopTo(IDiscs target)
        {
            Disc disc = DiscStack.Pop();
            target.Add(disc);
            return disc;
        }
    }
}
