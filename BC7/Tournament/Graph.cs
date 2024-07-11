using System;
using System.Collections.Generic;

namespace BC7
{

    public class Graph : IDraw, IUpdate
    {
        private const float unit = 60;
        private const float space = unit;
        private static readonly Vector2 fieldSize = new Vector2(unit * 1.75f, unit * 0.75f);
        private const float fieldOutlineThickness = 1f;
        private const float colorTransparency = 0.2f;
        private static readonly Color colorLineBattle = Color.White;
        private static readonly Color colorLineWon = Color.White * 0.1f;
        private static readonly Color colorText = Color.White;
        private const float lineThickness = 1f;
        private static readonly Vector2 half = new Vector2(0.5f);
        public event Action? TakeScreenshot;

        private const float wonOffset = unit * 0.1f;
        private int doTakeScreenshot;

        public int Round { get; private set; } = 0;
        public int Match { get; private set; } = 0;

        private GraphBot[] bots = new GraphBot[8];
        private GraphBot?[,] botsPerRound = new GraphBot?[4, 8];

        // next round indices
        private int[,] botGotos = new int[,] // [from round, bot Index]
        {
            { 0, 4, 1, 5, 2, 6, 3, 7 }, // match 1: (win, loose), match 2: (win, loose), ...
            { 0, 2, 1, 3, 4, 6, 5, 7 },
            { 0, 1, 2, 3, 4, 5, 6, 7 },
        };

        private static SpriteFont Font => Content.Fonts.TahomaFont.Value;

        public Graph(IList<string> names)
        {
            if (names.Count != 8)
                throw new Exception("there must be 8 bots");

            for (int i = 0; i < names.Count; i++)
            {
                bots[i] = new GraphBot(names[i], i);
                botsPerRound[0, i] = bots[i];
            }
        }

        private void MatchFinished(int round, int botIndexWon)
        {
            int match = botIndexWon / 2;
            int botIndexLoose;
            if (match * 2 == botIndexWon)
                botIndexLoose = botIndexWon + 1;
            else
                botIndexLoose = botIndexWon - 1;

            botsPerRound[round + 1, botGotos[round, match * 2]] = botsPerRound[round, botIndexWon];
            botsPerRound[round + 1, botGotos[round, match * 2 + 1]] = botsPerRound[round, botIndexLoose];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rect field = new Rect(Vector2.Zero, new Vector2(fieldSize.X * 1.5f, fieldSize.Y));
            Rect? lastField = null;
            Color color;
            float height, x, y;

            // draw 1st
            if (Round >= 0)
            {
                color = Color.White * colorTransparency;
                float width = fieldSize.X * 8 + fieldSize.X * 7 /* default space */ + space * 2 /* space in the middle*/;
                x = -width / 2;
                y = -fieldSize.Y / 2;
                for (int i = 0; i < 8; i++)
                {
                    DrawFieldAndMaybeLine(0, fieldSize.Y * 3.5f, i);
                    x += fieldSize.X * 2f;
                    if (i == 3)
                        x += space * 2;
                }
            }

            // draw 2nd
            if (Round >= 1 || Round >= 0 && Match > 0)
            {
                color = ColorExtension.FromHex(0xaaaaff) * colorTransparency;
                y = -fieldSize.Y * 4.5f;
                int botIndex = 0;
                // top then bottom
                for (int i = 0; i < 2; i++)
                {
                    x = -(fieldSize.X * 6.5f + space);

                    for (int j = 0; j < 4; j++)
                    {
                        if (Round >= 1)
                            DrawFieldAndMaybeLine(1, fieldSize.Y * 1.5f, botIndex++);
                        else
                        {
                            DrawField(1, botIndex++);
                        }

                        x += fieldSize.X * 4f;
                        if (j == 1)
                            x += space * 2f;
                    }

                    y = fieldSize.Y * 3.5f;
                }
            }

            // draw 3rd
            if (Round >= 2 || Round >= 1 && Match > 0)
            {
                color = Color.Red * colorTransparency;
                int botIndex = 0;
                // top to bottom
                y = -fieldSize.Y * 6.5f;
                for (int i = 0; i < 4; i++)
                {
                    x = -(fieldSize.X * 4.5f + space);
                    for (int j = 0; j < 2; j++)
                    {
                        if (Round >= 2)
                            DrawFieldAndMaybeLine(2, fieldSize.Y / 2f, botIndex++);
                        else
                            DrawField(2, botIndex++);
                        x = fieldSize.X * 3.5f + space;
                    }

                    y += fieldSize.Y * 4f;
                }
            }

            // draw 4th
            if (Round >= 3 || Round >= 2 && Match > 0)
            {
                color = Color.Yellow * colorTransparency;
                height = fieldSize.Y * 8 + fieldSize.Y * 7;
                y = -height / 2;
                x = -fieldSize.X / 2;
                for (int i = 0; i < 8; i++)
                {
                    DrawField(3, i);
                    y += fieldSize.Y * 2f;
                }
            }

            if (doTakeScreenshot == 1)
            {
                doTakeScreenshot++;
            }


            void DrawFieldAndMaybeLine(int drawnRound, float betweenNextHalved, int botIndex)
            {
                bool secondGraphBot = false;
                if (lastField != null)
                    secondGraphBot = true;

                // draw field
                lastField = field.CloneRect();
                DrawField(drawnRound, botIndex);

                // draw battle line
                if (secondGraphBot)
                {
                    GraphBot? botWon = botsPerRound[drawnRound + 1, botGotos[drawnRound, botIndex]];

                    if (botWon == null)
                    {
                        DrawLineBattle();
                    }
                    else
                    {
                        GraphBot? botLast = botsPerRound[drawnRound, botIndex];
                        if (botLast != null)
                        {
                            bool lastGraphBotWon = botLast == botWon;
                            DrawLineWon(lastGraphBotWon, betweenNextHalved);
                        }
                    }
                    lastField = null;
                }
            }
            void DrawField(int drawnRound, int botIndex)
            {
                // draw field
                field.Pos = new Vector2(x, y);
                field.Outline().ThickenInside(fieldOutlineThickness).Draw(spriteBatch, color);
                field.Draw(spriteBatch, color);

                if (drawnRound != 3 && drawnRound == Round && botIndex / 2 == Match)
                {
                    field.Outline().ThickenOutside(fieldOutlineThickness * 10f).Draw(spriteBatch, Color.White);
                }

                // draw bot name
                GraphBot? bot = botsPerRound[drawnRound, botIndex];
                bool matchFinished = drawnRound < 3 && botsPerRound[drawnRound + 1, botGotos[drawnRound, botIndex]] != null;
                if (bot != null)
                {
                    var font = Font;

                    string text = bot.Name;
                    if (drawnRound > 0)
                        text += " +" + bot.Scores[drawnRound - 1] + "=" + bot.GetTotalScore(drawnRound);

                    if (font.MeasureString(text).X > field.Width - lineThickness * 2f)
                        font = Content.Fonts.TinyFont.Value;
                    font.Draw(spriteBatch, text, field.GetCenterAnchor(), colorText * (matchFinished ? 0.5f : 1f), roundPositionTo: 1f);
                }
            }
            void DrawLineBattle()
            {
                Vector2 dist = field.Pos - lastField.Pos;
                dist.Normalize();
                dist *= 0.5f;
                spriteBatch.DrawLine(lastField.GetPos(half + dist), field.GetPos(half - dist), colorLineBattle, lineThickness);
            }
            void DrawLineWon(bool lastWon, float betweenNextHalved)
            {
                Vector2 dist = field.Pos - lastField.Pos;
                dist.Normalize();
                dist *= 0.5f;
                Vector2 mid = (field.GetCenter() + lastField.GetCenter()) / 2f;

                float sign = lastWon ? -1f : 1f;
                Vector2 wonOffset2 = new Vector2(0, wonOffset * sign);

                spriteBatch.DrawLine(lastField.GetPos(half + dist) + wonOffset2, mid + wonOffset2, colorLineWon, lineThickness);

                spriteBatch.DrawLine(mid + wonOffset2, mid + new Vector2(0, betweenNextHalved * sign), colorLineWon, lineThickness);

                sign = -sign;
                wonOffset2 = new Vector2(0, wonOffset * sign);
                spriteBatch.DrawLine(field.GetPos(half - dist) + wonOffset2, mid + wonOffset2, colorLineWon, lineThickness);
                spriteBatch.DrawLine(mid + wonOffset2, mid + new Vector2(0, betweenNextHalved * sign), colorLineWon, lineThickness);
            }
        }

        public void Update()
        {
            //if (round < 3)
            //{
            //    if (keys.Left.Pressed)
            //    {
            //        MatchFinished(true);
            //    }
            //    if (keys.Right.Pressed)
            //    {
            //        MatchFinished(false);
            //    }
            //}

            //if (keys.Enter.Pressed)
            //{
            //    if (match >= 4)
            //    {
            //        match = 0;
            //        round++;

            //        if (round == 3)
            //        {
            //            doTakeScreenshot = 1;
            //        }
            //    }
            //}


            if (/*Round == 3 &&*/ doTakeScreenshot == 0)
            {
                doTakeScreenshot = 1;
            }
            if (doTakeScreenshot == 2)
            {
                TakeScreenshot?.Invoke();
                doTakeScreenshot = 3;
            }
        }

        private void MatchFinished(bool firstWon)
        {
            if (Match < 4)
            {
                MatchFinished(Round, Match * 2 + (firstWon ? 0 : 1));
                Match++;
                if (Match >= 4)
                {
                    Match = 0;
                    Round++;
                }
            }
        }

        public bool IsDone()
        {
            return Round >= 3;
        }

        public int[]? GetParticipantsForNextMatch()
        {
            GraphBot? bot1 = botsPerRound[Round, Match * 2];
            GraphBot? bot2 = botsPerRound[Round, Match * 2 + 1];

            if (bot1 == null || bot2 == null)
                return null;
            return new int[] { bot1.Index, bot2.Index };
        }

        public void MatchFinished(float[] scores)
        {
            var participants = GetParticipantsForNextMatch();
            if (participants == null)
                throw new Exception();

            for (int i = 0; i < scores.Length; i++)
            {
                bots[participants[i]].Scores.Add(scores[i]);
            }

            MatchFinished(scores[0] >= scores[1]);
        }
    }
}
