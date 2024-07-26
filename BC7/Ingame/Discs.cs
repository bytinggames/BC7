namespace BC7
{
    public class Discs : DiscsParent
    {
        protected int flowers;
        protected int skulls;

        internal override int Flowers => flowers;
        internal override int Skulls => skulls;

        public Discs(int flowers, int skulls)
        {
            this.flowers = flowers;
            this.skulls = skulls;
        }

        public Discs() : this(0, 0) { }

        internal bool Has(Disc disc)
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
        internal bool? TryMoveDiscTo(Disc disc, DiscsParent target)
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
                        flowers--;
                        target.AddFlower();
                        return i == 0;
                    }
                    else
                    {
                        skulls--;
                        target.AddSkull();
                        return i == 0;
                    }
                }

                disc = disc.GetInverse();
            }

            return null;
        }

        internal override void AddFlower() => flowers++;
        internal override void AddSkull() => skulls++;
    }
}
