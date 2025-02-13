﻿using System;
using System.Diagnostics;
using System.Linq;

namespace BC7
{
    internal record BotVisualAssets(
        Ref<Texture2D> TexMat0,
        Ref<Texture2D> TexMat1, 
        Ref<Texture2D> TexFlower,
        Ref<Texture2D> TexSkull, 
        Ref<Texture2D> TexBack,
        Ref<SpriteFont> Font,
        Ref<SpriteFont> FontBold,
        Ref<SpriteFont> FontBig,
        Ref<SpriteFont> FontBigBold,
        ShaderHueShift ShaderHueShift);

    internal class BotVisual
    {
        const int MaxDiscsPlayed = 4;
        private readonly BotVisualAssets assets;
        private readonly Sizes sizes;
        private readonly Action<string> speak;
        private string lastSpeech = "";

        public BotVisual(BotVisualAssets assets, Sizes sizes, Action<string> speak)
        {
            this.assets = assets;
            this.sizes = sizes;
            this.speak = speak;
        }

        public void Draw1(SpriteBatch spriteBatch, Vector2 pos, Bot bot)
        {
            var data = bot.Data;
            if (!data.Alive)
            {
                return;
            }


            float hueShift = ((bot.Brain.Color.ToHSV().hue + 360f - 194f /* default hue of discs */) / 360f) % 1f;
            using (assets.ShaderHueShift.HueShift.Use(hueShift))
            {
                assets.ShaderHueShift.HueShift.Apply();
                assets.ShaderHueShift.ApplyParameters();
                Color color = bot.Brain.ColorWithoutHue;

                var texMat = data.Successes == 0 ? assets.TexMat0 : assets.TexMat1;
                var matRect = Anchor.Center(pos).Rectangle(sizes.MatSize);
                DrawOutline(sizes.OutlineThickness, 8, color, (color, outlineOffset) =>
                {
                    var r = matRect.CloneRect();
                    r.Pos += outlineOffset;
                    texMat.Value.Draw(spriteBatch, r, color);
                });
                int playedCount = data.DiscsPlayed.Count();
                float discDiameter = sizes.MatSize / texMat.Value.Width;
                for (int i = 0; i < playedCount + data.DiscsRevealed.Count(); i++)
                {
                    float angle = MathHelper.TwoPi / MaxDiscsPlayed * i;
                    var tex = i < data.DiscsPlayed.Count() ? assets.TexBack
                        : data.DiscsRevealed.Discs[data.DiscsRevealed.Discs.Count - 1 - (i - playedCount)] == Disc.Flower ? assets.TexFlower
                        : assets.TexSkull;

                    Random rand = new Random(bot.Brain.GetType().Name[0] + i);
                    float rotation = rand.NextSingle() * MathHelper.TwoPi;
                    DrawOutline(sizes.OutlineThickness, 16, color, (color, outlineOffset) =>
                    {
                        tex.Value.Draw(spriteBatch,
                            Anchor.Center(pos + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * sizes.PlayedDiscsOnCircleRadius + outlineOffset),
                            color * (data.Passed ? 0.5f : 1f), null, new Vector2(discDiameter), rotation);
                    });
                }

                if (!string.IsNullOrWhiteSpace(bot.Brain.Thoughts))
                {
                    string wrapped = assets.Font.Value.WrapText(bot.Brain.Thoughts, sizes.ThoughtsWidth, 1f, out _);
                    assets.Font.Value.Draw(spriteBatch, wrapped, Anchor.Top(matRect.GetCenter() + new Vector2(0f, sizes.PlayedDiscsOnCircleRadius + discDiameter * assets.TexBack.Value.Height / 2f + sizes.SpaceToText)), Colors.Text);
                }

                // name
                Anchor anchor = Anchor.Bottom(matRect.TopV + new Vector2(0f, -sizes.SpaceToText));
                assets.FontBold.Value.Draw(spriteBatch, bot.Data.ID + " - " + bot.Brain.GetType().Name + " - " + new string('O', bot.Data.Lives), anchor, Colors.TextOuter);
                assets.Font.Value.Draw(spriteBatch, bot.Data.ID + " - " + bot.Brain.GetType().Name + " - " + new string('O', bot.Data.Lives), anchor, Colors.TextInner);

            }
        }

        public void Draw2(SpriteBatch spriteBatch, Vector2 pos, Bot bot, SkullGame game)
        {
            var matRect = Anchor.Center(pos).Rectangle(sizes.MatSize);

            if (bot.Data.LastBidThisRound != 0)
            {
                bool highestBid = game.BotsAlive.Select(f => f.Data.LastBidThisRound).Max() == bot.Data.LastBidThisRound;
                //Anchor anchor = matRect.GetCenterAnchor();
                Anchor anchor = Anchor.Bottom(matRect.TopV - new Vector2(0f, sizes.SpaceToText * 2f + assets.Font.Value.DefaultCharacterHeight));
                assets.FontBigBold.Value.Draw(spriteBatch, bot.Data.LastBidThisRound.ToString(), anchor, Colors.TextOuter * (highestBid ? 1f : 0.5f));
                assets.FontBig.Value.Draw(spriteBatch, bot.Data.LastBidThisRound.ToString(), anchor, Colors.TextInner * (highestBid ? 1f : 0.5f));
            }

            if (bot.Data.IsWinner())
            {
                assets.FontBigBold.Value.Draw(spriteBatch, "WINNER", matRect.GetCenterAnchor(), Colors.TextOuter);
                assets.FontBig.Value.Draw(spriteBatch, "WINNER", matRect.GetCenterAnchor(), Colors.TextInner);
            }
        }

        internal void UpdateSpeech(Bot bot)
        {
            if (lastSpeech != bot.Brain.Thoughts)
            {
                lastSpeech = bot.Brain.Thoughts;
                string name = bot.Brain.GetType().Name;
                int i = name.IndexOf('_');
                if (i != -1)
                {
                    name = name.Remove(i);
                }
                speak(name + " says: " + bot.Brain.Thoughts);
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

        public float MatSize => 200f * Scale;
        public float PlayerSize => 500f * Scale;
        public float ThoughtsWidth => PlayerSize;
        public float PlayedDiscsOnCircleRadius => MatSize * 0.3f;
        public float OutlineThickness => 4f * Scale;
        public float SpaceToText => 8f; // no scaling, as text is always the same size

        public Sizes(IResolution res)
        {
            this.res = res;
        }
    }

}
