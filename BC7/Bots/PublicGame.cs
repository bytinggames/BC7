using System.Collections.Generic;

namespace BC7
{
    public class PublicGame
    {
        public List<PublicBot> Bots { get; }

        public PublicGame(List<PublicBot> bots)
        {
            Bots = bots;
        }
    }
}
