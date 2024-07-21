namespace BC7
{
    internal class Discs : IDiscs
    {
        public int Flowers { get; set; }
        public int Skulls { get; set; }

        public Discs(int flowers, int skulls)
        {
            this.Flowers = flowers;
            this.Skulls = skulls;
        }

        public Discs() : this(0, 0) { }

        public bool Has(Disc disc)
        {
            if (disc == Disc.Flower)
            {
                return Flowers > 0;
            }
            else
            {
                return Skulls > 0;
            }
        }

        /// <summary>
        /// true: success
        /// false: played other disc
        /// null: played none, because none available
        /// </summary>
        public bool? TryMoveDiscTo(Disc disc, IDiscs target)
        {
            // loop:
            // first iteration: try to play the given disc
            // second iteration: try to play the non-given disc
            // if none could be played, play no disc
            for (int i = 0; i < 2; i++)
            {
                if (Has(disc))
                {
                    if (disc == Disc.Flower)
                    {
                        Flowers--;
                        target.AddFlower();
                        return i == 0;
                    }
                    else
                    {
                        Skulls--;
                        target.AddSkull();
                        return i == 0;
                    }
                }

                disc = disc.GetInverse();
            }

            return null;
        }

        public void AddFlower() => Flowers++;
        public void AddSkull() => Skulls++;
    }
}
