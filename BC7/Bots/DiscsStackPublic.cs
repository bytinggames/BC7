using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    public class DiscsStackPublic : DiscsStack
    {
        public List<Disc> GetDiscs() => Discs.ToList(); // clone
    }
}
