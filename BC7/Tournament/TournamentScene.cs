namespace BC7
{
    internal class TournamentScene : Scene
    {
        public TournamentScene()
        {
        }

        protected override void Begin(SpriteBatch spriteBatch)
        {
            Matrix transform = Matrix.CreateTranslation(ResX / 2f, ResY / 2f, 0f);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transform);
        }
    }
}
