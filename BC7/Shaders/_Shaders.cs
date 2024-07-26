using System;

namespace BC7
{
    public partial class GlobalGame
    {
        private Shaders CreateShaders(ContentLoader load)
        {
            return new Shaders(
                CreateShaderHueShift(load)
            );
        }

        private static ShaderHueShift CreateShaderHueShift(ContentLoader load)
        {
            return new ShaderHueShift(load.Effects.D2.HueShiftFx,
                0f
            );
        }
    }
}
