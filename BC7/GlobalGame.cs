using System;
using System.Linq;

namespace BC7
{

    public partial class GlobalGame : GamePrototype
    {
        private readonly StuffDisposable stuff;
        private Scene currentScene;
        private Scene? nextScene;
        private readonly Random randGraphics = new Random();
        private readonly SettingsManager<Settings> settingsManager;
        private readonly Paths paths;
        private Func<Scene>? createCurrentScene;

        private Settings settings => settingsManager.Settings;

        public bool End { get; internal set; }
        public Color ClearColor { get; private set; } = Colors.TournamentBackground;

        private readonly Participant[] participants;
        private readonly Graph? graph;

        // global assets
        private readonly UIAssets uiAssets;
        private Match match;


        private void MyExit()
        {
            End = true;
            Exit();
        }

        public GlobalGame(GameWrapper g, Paths paths, SettingsManager<Settings> settingsManager, Type[] botTypes) : base(g, paths, new MyContentConverter(), true, false, true,
            false, true)
        {
            this.paths = paths;
            this.settingsManager = settingsManager;
            
            InitLocaReload();
            var load = new ContentLoader(contentCollector, Use(new DisposableContainer()));

            GlobalContent.Initialize(load, windowManager, gDevice, paths, settingsManager);
            uiAssets = new UIAssets(load, windowManager);

            // bot and tournament stuff
            participants = botTypes.Select(f => new Participant(f)).ToArray();
            if (botTypes.Length == 8)
            {
                graph = new Graph(botTypes.Select(f => f.Name).ToList());
                if (settings.TakeGraphScreenshots)
                    graph.TakeScreenshot += () => screenshotter.TakeScreenshot(false);
            }
            stuff = new StuffDisposable(typeof(IUpdate), typeof(IDrawBatch));

            Func<Scene> StartScene = graph != null ? CreateGraphScene : CreateIngame;

            if (settings.KeyShortcuts)
                stuff.Add(new UpdateTrigger(() => input.Keys.Escape.Pressed, Exit));
            //stuff.Add(new UpdateTrigger(() => input.Keys.Back.Pressed, () => ChangeScene(CreateMenue)));
            //stuff.Add(new UpdateTrigger(() => input.Keys.R.Pressed && input.Keys.Control.Down, () => ChangeScene(StartScene)));
            //stuff.Add(new UpdateTrigger(() => input.Keys.R.Pressed && !input.Keys.Control.Down && createCurrentScene != null, () => ChangeScene(createCurrentScene!)));

            stuff.Add(currentScene = StartScene());
            input.OnPlayInput += () => ChangeScene(StartScene);

            if (settings.Fullscreen)
                windowManager.ToggleFullscreen();
        }

        private const int MaxSceneID = 2;
        private Func<Scene> GetScene(int? id)
        {
            return id switch
            {
                1 => CreateGraphScene,
                MaxSceneID => CreateIngame,
                _ => CreateMenue
            };
        }

        private void InitLocaReload()
        {
            if (HotReloadContent != null)
            {
                HotReloadContent.OnTextReload += locaPath =>
                {
                    Loca.L.Reload(locaPath, Loca.L.LanguageKey);
                    Loca.Dict = Loca.L.GetDictionary();
                };
            }
        }

        protected override void UpdateIteration(GameTime gameTime)
        {
            updateSpeed.OnRefresh(gameTime);

            if (nextScene != null)
            {
                currentScene.Dispose();
                stuff.Remove(currentScene);
                currentScene = nextScene;
                nextScene = null;
                stuff.Add(currentScene);
            }

            stuff.Get<IUpdate>().ForEvery(f => f.Update());
        }

        protected override void DrawIteration(GameTime gameTime)
        {
            drawSpeed.OnRefresh(gameTime);

            spriteBatch.GraphicsDevice.Clear(ClearColor);

            stuff.ForEach<IDrawBatch>(f => f.DrawBatch(spriteBatch));
        }

        public override void Dispose()
        {
            stuff.Dispose();

            base.Dispose();
        }

        public void ChangeScene(Func<Scene> CreateScene)
        {
            createCurrentScene = CreateScene;
            // only change the scene to the first requested this Update
            if (nextScene == null)
                nextScene = CreateScene();
        }

        public override void OnActivate()
        {
            settingsManager.UpdateCheckChanges();

            base.OnActivate();
        }

        protected override Scene? GetTopmostScene()
        {
            return currentScene?.GetTopmostScene();
        }
    }
}