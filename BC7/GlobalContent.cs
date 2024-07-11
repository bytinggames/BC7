using System;

namespace BC7
{
    public static class GlobalContent
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static ContentLoader Content { get; private set; }

        public static Random RandGraphics { get; } = new Random();
        public static MyDepth Depth { get; } = new MyDepth();

        public static WindowManager WindowManager { get; private set; }
        public static int ResX => WindowManager.Resolution.X;
        public static int ResY => WindowManager.Resolution.Y;
        public static GraphicsDevice GDevice { get; private set; }
        public static Paths MyPaths { get; private set; }

        private static SettingsManager<Settings> _settingsManager;
        public static Settings MySettings => _settingsManager.Settings;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static void Initialize(ContentLoader load, WindowManager windowManager, GraphicsDevice gDevice, Paths paths, SettingsManager<Settings> settingsManager)
        {
            Content = load;
            WindowManager = windowManager;
            GDevice = gDevice;
            MyPaths = paths;

            _settingsManager = settingsManager;
        }
    }
}
