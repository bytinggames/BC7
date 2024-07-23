namespace BC7
{
    public abstract class DiscsParent
    {
        internal abstract int Flowers { get; }
        internal abstract int Skulls { get; }

        internal abstract void AddFlower();
        internal abstract void AddSkull();
    }

    public static class IDsiscsExtension
    {
        public static int Count(this DiscsParent discs) => discs.Flowers + discs.Skulls;
        public static void Add(this DiscsParent discs, Disc disc)
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
