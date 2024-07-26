namespace BC7
{
    public class DiscsPublic : Discs
    {
        public DiscsPublic(Discs discsInHand)
        {
            flowers = discsInHand.Flowers;
            skulls = discsInHand.Skulls;
        }

        public new int Flowers => flowers;
        public new int Skulls => skulls;
        public new bool Has(Disc disc) => base.Has(disc);
    }
}
