using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    internal class GraphBot
    {
        public string Name { get; }
        public int Index { get; }
        public List<float> Scores { get; internal set; } = new();

        public float GetTotalScore(int round)
        {
            return Scores.Take(round).Sum();
        }

        public GraphBot(string name, int index)
        {
            int underscoreIndex = name.IndexOf('_');
            if (underscoreIndex != -1)
                name = name.Remove(underscoreIndex);
            if (name.Length > 12)
                name = name.Insert(12, "\n");
            if (name.Length > 25)
                name = name.Insert(25, "\n");
            if (name.Length > 36)
                name = name.Remove(36);
            Name = name;
            Index = index;
        }
    }
}
