using BytingLib.Markup;
using System;

namespace BC7
{
    internal class MenueUI : IUpdate, IDraw, IDisposable
    {
        private readonly UIAssets assets;
        private readonly KeyInput keys;
        private readonly Action<int?> changeScene;
        private readonly int maxSceneID;
        private readonly MarkupRoot markupRoot;
        private bool loading;
        private Action? doNextUpdate;

        public MenueUI(UIAssets assets, KeyInput keys, Action<int?> changeScene, int maxSceneID, Creator creator)
        {
            this.assets = assets;
            this.keys = keys;
            this.changeScene = changeScene;
            this.maxSceneID = maxSceneID;
            markupRoot = new MarkupRoot(creator, () => Loca.Menue.text, Loca.L);
        }

        public void Update()
        {
            if (doNextUpdate != null)
            {
                doNextUpdate.Invoke();
                doNextUpdate = null;
            }
            if (!loading)
            {
                for (int i = 0; i <= maxSceneID; i++)
                {
                    if (keys.Number(i).Pressed)
                    {
                        loading = true;
                        doNextUpdate = () => changeScene(i);
                        return;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //if (loading)
            //    assets.Font.Value.Draw(spriteBatch, "Loading...", Anchor.TopLeft(32f, 32f), Color.Black);
            Anchor anchor = Anchor.Center(assets.WindowManager.Resolution.ToVector2() / 2f);
            markupRoot.Draw(new MarkupSettings(spriteBatch, assets.Font, anchor, Color.Black, 0.5f));
        }

        public void Dispose()
        {
            markupRoot.Dispose();
        }
    }
}
