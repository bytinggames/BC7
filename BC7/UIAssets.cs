namespace BC7
{
    internal class UIAssets
    {
        public Ref<SpriteFont> Font { get; }
        public Ref<SpriteFont> FontSmall { get; }
        public WindowManager WindowManager { get; }

        public UIAssets(ContentLoader load, WindowManager windowManager)
        {
            Font = load.Fonts.TahomaFont;
            FontSmall = load.Use<SpriteFont>("Fonts/MessageBox");
            WindowManager = windowManager;
        }
    }
}
