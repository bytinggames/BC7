using System;

namespace BC7
{
    public partial class GlobalGame
    {

        private Scene CreateGraphScene()
        {
            var scene = new TournamentScene();

            if (graph == null)
                throw new Exception();

            scene.AddRange(graph);
            scene.Add(new UpdateTrigger(() => input.Keys.Enter.Pressed || !MySettings.VisibleGame, GraphDone));

            return scene;
        }

        private void GraphDone()
        {
            if (graph == null)
                throw new Exception();

            if (graph.IsDone())
            {
                MyExit();
                return;
            }

            ChangeScene(CreateIngame);
        }
    }
}
