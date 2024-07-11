using System;

namespace BC7
{
    internal abstract class Updraw : IDisposable
    {
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public abstract bool Update(GameTime gameTime);

        public virtual void Dispose()
        {
        }

        public virtual void DrawScreen(SpriteBatch spriteBatch) { }
    }
}
