using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BC7
{
    public partial class GlobalGame
    {
        private Scene CreateIngameScene()
        {
            List<BotBrain> brains = new();
            for (int i = 0; i < participants.Length; i++)
            {
                object? instance;
                if (participants[i].BotType == typeof(Human))
                {
                    instance = new Human(input.Keys);
                }
                else
                {
                    instance = Activator.CreateInstance(participants[i].BotType);
                }
                if (instance is BotBrain bot)
                {
                    brains.Add(bot);
                }
            }

            var texs = Content.Textures;
            SkullGame game = new SkullGame(brains, windowManager, gameIndex,
                new(texs.Mat_0Tex, texs.Mat_1Tex, texs.FlowerTex, texs.SkullTex, texs.BackTex,
                Content.Fonts.TahomaFont, Content.Fonts.Tahoma_boldFont, Content.Fonts.TahomaBigFont, Content.Fonts.TahomaBig_boldFont, shaders.HueShift),
                Speak);

            SkullGameContainer gameContainer = new(game, input.Keys, windowManager, Content.Fonts.TahomaBigFont);

            Scene scene = new SceneIngame(shaders.HueShift);
            scene.Add(gameContainer);

            gameContainer.OnMatchFinished += MatchFinished;

            return scene;
        }

        HashSet<string> alreadySpoken = new();

        private void Speak(string text)
        {
            if (settings.Speech && settings.GameSpeed == 0)
            {
                if (alreadySpoken.Add(text))
                {
                    synth.Speak(text);
                }
            }
        }

        private void MatchFinished(int? winnerID)
        {
            if (winnerID.HasValue)
            {
                participants[winnerID.Value].Score++;
            }

            if (settings.GameSpeed <= 1)
            {
                WriteScores();
            }
            if (gameIndex + 1 < settings.AmountOfGames)
            {
                gameIndex++;
                ChangeScene(CreateIngameScene);
            }
            else if (settings.GameSpeed == 2)
            {
                WriteScores();
                MyExit();
            }
        }

        private void WriteScores()
        {
            Debug.WriteLine("scores:\n" + string.Join('\n', participants.Select(f => f.Score.ToString().PadLeft(3) + " " + f.BotType.Name)) + "\n");
        }
    }
}