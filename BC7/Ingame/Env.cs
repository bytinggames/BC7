using System.Collections.Generic;

namespace BC7
{
    public class Env
    {
        public List<Bot> Bots { get; } = new();

        internal void AddBot(Bot bot)
        {
            Bots.Add(bot);
        }
    }
}
