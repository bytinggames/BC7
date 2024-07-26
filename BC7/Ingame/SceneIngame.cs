namespace BC7
{
    class SceneIngame(ShaderHueShift shaderHueShift) : Scene
    {
        private readonly ShaderHueShift shaderHueShift = shaderHueShift;

        protected override void Begin(SpriteBatch spriteBatch)
        {
            shaderHueShift.ApplyParameters();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: shaderHueShift.Effect.Value, sortMode: SpriteSortMode.Immediate);
        }
    }
}