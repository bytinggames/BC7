using System;

namespace BC7
{
    public record BotVisualAssets(
        Ref<Texture2D> TexMat0,
        Ref<Texture2D> TexMat1, 
        Ref<Texture2D> TexFlower,
        Ref<Texture2D> TexSkull, 
        Ref<Texture2D> TexBack,
        Ref<SpriteFont> Font);

    public class BotVisual
    {
        public const float MatSize = 100f;
        public const float DiscDiameter = 70f;
        public const float Radius = 300f;
        const float PlayedDiscsOnCircleRadius = DiscDiameter * 0.3f;
        const int MaxDiscsPlayed = 4;
        private readonly BotVisualAssets assets;

        public BotVisual(BotVisualAssets assets)
        {
            this.assets = assets;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Bot bot)
        {
            var data = bot.Data;
            if (!data.Alive)
            {
                return;
            }

            var texMat = data.Successes == 0 ? assets.TexMat0 : assets.TexMat1;
            var matRect = Anchor.Center(pos).Rectangle(MatSize);
            texMat.Value.Draw(spriteBatch, matRect, bot.Brain.Color);

            int playedCount = data.DiscsPlayed.Count();
            for (int i = 0; i < playedCount + data.DiscsRevealed.Count(); i++)
            {
                float angle = MathHelper.TwoPi / MaxDiscsPlayed * i;
                var tex = i < data.DiscsPlayed.Count() ? assets.TexBack
                    : data.DiscsRevealed.Discs[data.DiscsRevealed.Discs.Count - 1 - (i - playedCount)] == Disc.Flower ? assets.TexFlower
                    : assets.TexSkull;
                tex.Value.Draw(spriteBatch,
                    Anchor.Center(pos + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * PlayedDiscsOnCircleRadius),
                    bot.Brain.Color, null, new Vector2(DiscDiameter / tex.Value.Width));
            }

            if (!string.IsNullOrWhiteSpace(bot.Brain.Thoughts))
            {
                assets.Font.Value.Draw(spriteBatch, bot.Brain.Thoughts, Anchor.Top(matRect.BottomV), Colors.Text);
            }

            if (bot.Data.LastBidThisRound != 0)
            {
                assets.Font.Value.Draw(spriteBatch, bot.Data.LastBidThisRound.ToString(), Anchor.Left(matRect.RightV), Colors.Text);
            }
        }
    }
}
