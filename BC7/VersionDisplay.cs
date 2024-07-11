using System.Reflection;

namespace BC7
{
    internal class VersionDisplay : IDraw
    {
        private readonly UIAssets assets;
        private string versionStr;

        public VersionDisplay(UIAssets assets)
        {
            this.assets = assets;
            versionStr = "v" + Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            assets.FontSmall.Value.Draw(spriteBatch, versionStr, Anchor.BottomRight(assets.WindowManager.Resolution.ToVector2() - new Vector2(4)), Color.Black, Vector2.One);
        }
    }
}
