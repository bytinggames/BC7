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

    internal class BotVisual
    {
        const int MaxDiscsPlayed = 4;
        private readonly BotVisualAssets assets;
        private readonly Sizes sizes;

        public BotVisual(BotVisualAssets assets, Sizes sizes)
        {
            this.assets = assets;
            this.sizes = sizes;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Bot bot)
        {
            var data = bot.Data;
            if (!data.Alive)
            {
                return;
            }

            var texMat = data.Successes == 0 ? assets.TexMat0 : assets.TexMat1;
            var matRect = Anchor.Center(pos).Rectangle(sizes.MatSize);
            DrawOutline(sizes.OutlineThickness, 8, bot.Brain.Color, (color, outlineOffset) =>
            {
                var r = matRect.CloneRect();
                r.Pos += outlineOffset;
                texMat.Value.Draw(spriteBatch, r, color);
            });
            int playedCount = data.DiscsPlayed.Count();
            for (int i = 0; i < playedCount + data.DiscsRevealed.Count(); i++)
            {
                float angle = MathHelper.TwoPi / MaxDiscsPlayed * i;
                var tex = i < data.DiscsPlayed.Count() ? assets.TexBack
                    : data.DiscsRevealed.Discs[data.DiscsRevealed.Discs.Count - 1 - (i - playedCount)] == Disc.Flower ? assets.TexFlower
                    : assets.TexSkull;
                float discDiameter = sizes.MatSize / texMat.Value.Width;

                Random rand = new Random(bot.Brain.GetType().Name[0] + i);
                float rotation = rand.NextSingle() * MathHelper.TwoPi;
                DrawOutline(sizes.OutlineThickness, 16, bot.Brain.Color, (color, outlineOffset) =>
                {
                    tex.Value.Draw(spriteBatch,
                        Anchor.Center(pos + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * sizes.PlayedDiscsOnCircleRadius + outlineOffset),
                        color, null, new Vector2(discDiameter), rotation);
                });
            }

            if (!string.IsNullOrWhiteSpace(bot.Brain.Thoughts))
            {
                assets.Font.Value.Draw(spriteBatch, bot.Brain.Thoughts, Anchor.Top(matRect.BottomV), Colors.Text);
            }

            // name
            assets.Font.Value.Draw(spriteBatch, bot.Brain.GetType().Name, Anchor.Bottom(matRect.TopV), Colors.Text);

            if (bot.Data.LastBidThisRound != 0)
            {
                assets.Font.Value.Draw(spriteBatch, bot.Data.LastBidThisRound.ToString(), Anchor.Left(matRect.RightV), Colors.Text);
            }

            if (bot.Data.Successes >= 2 || bot.Data.LastSurvivor)
            {
                assets.Font.Value.Draw(spriteBatch, "WINNER", Anchor.Right(matRect.LeftV), Colors.Text);
            }
        }

        private void DrawOutline(float outlineThickness, int outlineQuality, Color color, Action<Color, Vector2> drawAction)
        {
            for (int i = 0; i < outlineQuality; i++)
            {
                float angle = i * MathHelper.TwoPi / outlineQuality;
                Vector2 offset = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * outlineThickness;
                drawAction(Colors.Outline, offset);
            }
            drawAction(color, Vector2.Zero);
        }
    }

    internal class Sizes
    {
        private readonly IResolution res;
        public float Scale => Math.Min(res.Resolution.X, res.Resolution.Y) / 1080f;

        public float MatSize => 250f * Scale;
        public float PlayerSize => 500f * Scale;
        public float PlayedDiscsOnCircleRadius => MatSize * 0.3f;
        public float OutlineThickness => 4f * Scale;

        public Sizes(IResolution res)
        {
            this.res = res;
        }
    }

}
