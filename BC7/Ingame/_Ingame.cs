using System;
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
                botTypes = participants.Take(2).Select(f => f.BotType).ToArray();
            }
            match = new Match(botTypes);

            Env env = new Env();

            Scene scene = new Scene();

            //Map scene = new Map(env, input.Mouse, input.Keys, levelData.Entities, levelData.Street, updateSpeed, drawSpeed, match, settingsManager);
            //env.Map = scene;
            //for (int i = 0; i < env.Bots.Count; i++)
            //{
            //    env.Bots[i].OnMapLoaded(env, i, settingsManager);
            //}


            //scene.OnMatchFinished = MatchFinished;
            
            //if (settings.KeyShortcuts)
            //    scene.Add(new UpdateTrigger(() => input.Keys.Enter.Pressed, scene.ForceMatchEnd));

            return scene;
        }

        private void MatchFinished()
        {
            if (graph != null)
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