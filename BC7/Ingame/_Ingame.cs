using System;
using System.Collections.Generic;
using System.Linq;

namespace BC7
{
    public partial class GlobalGame
    {
        private Scene CreateIngame()
        {
            Type[] botTypes;

            if (graph != null)
            {

                if (graph.IsDone())
                    throw new Exception("tournament is done");

                int[]? matchParticipants = graph.GetParticipantsForNextMatch();
                if (matchParticipants == null)
                    throw new Exception("something went wrong");

                botTypes = matchParticipants.Select(f => participants[f].BotType).ToArray();
            }
            else
            {
                botTypes = participants.Select(f => f.BotType).ToArray();
            }
            match = new Match(botTypes);

            //List<PublicBot> publicBots = new List<PublicBot>();
            //for (int i = 0; i < botTypes.Length; i++)
            //{
            //    publicBots.Add(new PublicBot(i, 
            //}
            //publicGame = new PublicGame(botTypes.Length);
            //publicGame.UpdateState();
            //new(), new(), new());

            List<BotBrain> brains = new();
            for (int i = 0; i < botTypes.Length; i++)
            {
                object? instance;
                if (botTypes[i] == typeof(Human))
                {
                    instance = new Human(input.Keys);
                }
                else
                {
                    instance = Activator.CreateInstance(botTypes[i]);
                }
                if (instance is BotBrain bot)
                {
                    brains.Add(bot);
                }
            }

            var texs = Content.Textures;
            SkullGame game = new SkullGame(brains, windowManager, 
                new(texs.Mat_0Tex, texs.Mat_1Tex, texs.FlowerTex, texs.SkullTex, texs.BackTex,
                Content.Fonts.TahomaFont, Content.Fonts.Tahoma_boldFont, Content.Fonts.TahomaBigFont, Content.Fonts.TahomaBig_boldFont, shaders.HueShift));

            SkullGameContainer gameContainer = new(game, input.Keys, windowManager, Content.Fonts.TahomaBigFont);

            Scene scene = new SceneIngame(shaders.HueShift);
            scene.Add(gameContainer);

            //Map scene = new Map(game, input.Mouse, input.Keys, levelData.Entities, levelData.Street, updateSpeed, drawSpeed, match, settingsManager);
            //game.Map = scene;
            //for (int i = 0; i < game.BotsAlive.Count; i++)
            //{
            //    game.BotsAlive[i].OnMapLoaded(game, i, settingsManager);
            //}


            //scene.OnMatchFinished = MatchFinished;

            //if (settings.KeyShortcuts)
            //    scene.Add(new UpdateTrigger(() => input.Keys.Enter.Pressed, scene.ForceMatchEnd));

            return scene;
        }

        private void MatchFinished()
        {
            if (graph != null && match != null)
            {
                graph.MatchFinished(match.Scores);

                ChangeScene(CreateGraphScene);
            }
            else
            {
                MyExit();
            }
        }
    }
}