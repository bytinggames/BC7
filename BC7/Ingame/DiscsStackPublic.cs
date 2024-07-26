using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    public class DiscsStackPublic : DiscsStack
    {
        public DiscsStackPublic() { }
    
        public DiscsStackPublic(DiscsStack discsPlayed)
        {
            Discs = discsPlayed.Discs.ToList();
        }

        public List<Disc> GetDiscs() => Discs.ToList(); // clone
    }
}
