using System;
using System.Collections.Generic;

namespace BC7
{
    internal class SkullGameContainer : IDraw, IUpdate
    {
        private readonly SkullGame game;
        private readonly KeyInput keys;
        private readonly IResolution resolution;
        private readonly Ref<SpriteFont> font;
        private readonly IEnumerator<LoopAction> gameEnumerator;
        public Action? OnEnd;
        public bool Ended { get; private set; }
        private bool waitForEnterInput;

        public SkullGameContainer(SkullGame game, KeyInput keys, IResolution resolution, Ref<SpriteFont> font)
        {
            this.game = game;
            this.keys = keys;
            this.resolution = resolution;
            this.font = font;
            gameEnumerator = game.GameLoop().GetEnumerator();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            game.Draw(spriteBatch);

            if (waitForEnterInput)
            {
                font.Value.Draw(spriteBatch, "[Enter]", Anchor.Bottom(new Vector2(resolution.Resolution.X / 2f, resolution.Resolution.Y) + new Vector2(0f, -32f)), Color.Black);
            }
        }

        public void Update()
        {
            if (Ended)
            {
                return;
            }

            if (waitForEnterInput)
            {
                if (keys.Space.Pressed || keys.Enter.Pressed)
                {
                    waitForEnterInput = false;
                }
            }
            else
            {
                if (gameEnumerator.MoveNext())
                {
                    if (gameEnumerator.Current == LoopAction.WaitForEnter)
                    {
                        waitForEnterInput = true;
                    }
                }
                else
                {
                    Ended = true;
                    OnEnd?.Invoke();
                }
            }
        }
    }
}
