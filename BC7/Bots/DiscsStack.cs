using System;
using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    public class DiscsStack : DiscsParent
    {
        internal List<Disc> Discs { get; private set; } = new();

        internal override int Flowers => Discs.Count(f => f == Disc.Flower);
        internal override int Skulls => Discs.Count(f => f == Disc.Skull);

        internal override void AddFlower()
        {
            Discs.Add(Disc.Flower);
        }

        internal override void AddSkull()
        {
            Discs.Add(Disc.Skull);
        }

        internal Disc MoveTopTo(DiscsParent target)
        {
            Disc disc = Discs[Discs.Count - 1];
            Discs.RemoveAt(Discs.Count - 1);
            target.Add(disc);
            return disc;
        }

        internal DiscsStack Clone()
        {
            DiscsStack clone = (DiscsStack)MemberwiseClone();

            clone.Discs = Discs.ToList();

            return clone;
        }
    }
}
