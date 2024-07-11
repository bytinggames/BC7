namespace BC7
{
    public static class Colors
    {
        public static readonly Color idk = Color.Black
            , player = ColorExtension.FromHex(0x1ab768)
            , friend = new Color(128, 0, 255)
            ;

        public static readonly Color[] botColors = new Color[]
            {
                Color.Red,
                Color.Black
            };

        public static Color TournamentBackground { get; } = ColorExtension.FromHex(0x222222);
    }
}
