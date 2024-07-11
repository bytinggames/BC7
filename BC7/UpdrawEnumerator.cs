using System.Collections.Generic;

namespace BC7
{
    internal class UpdrawEnumerator
    {
        private IEnumerator<Updraw> enumerator;
        public Updraw Current { get; set; }

        public UpdrawEnumerator(IEnumerable<Updraw> enumerable)
        {
            enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            Current = enumerator.Current;
        }

        public bool Update(GameTime gameTime)
        {
            if (Current == null)
                return false;

            if (!Current.Update(gameTime))
            {
                Current.Dispose();
                if (!enumerator.MoveNext())
                    return false;
                Current = enumerator.Current;
            }
            return true;
        }
    }
}
