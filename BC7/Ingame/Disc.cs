namespace BC7
{
    public enum Disc : int
    {
        Skull = -2,
        Flower = -1
    }

    public static class DiscExtension
    {
        public static Disc GetInverse(this Disc disc)
        {
            return disc == Disc.Flower ? Disc.Skull : Disc.Flower;
        }
    }
}
