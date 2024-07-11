using System;
using System.Collections.Generic;
using System.IO;

namespace BC7
{
    public class Program
    {
        public static void Run(List<Type> bots)
        {
            string contentPath = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent!.Parent!.Parent!.FullName, "Content");
            Paths paths = new Paths(contentPath);
            SettingsManager<Settings> settingsManager = new SettingsManager<Settings>(paths);

            if (settingsManager.Settings.ShuffleBots)
                bots.Shuffle(new Random());

#if RELEASE
CrashLogger.TryRun(paths.CrashLogFile, "Fonts/MessageBox", () =>
{
#endif
            GlobalGame? globalGame = null;
            using var game = new GameWrapper(g => globalGame = new GlobalGame(g, paths, settingsManager, bots.ToArray()), settingsManager.Settings.MSAA);
            if (settingsManager.Settings.VisibleGame)
                game.Run();
            else
            {
                game.SuppressDraw();
                game.RunOneFrame();
                while (!globalGame!.End)
                {
                    globalGame!.UpdateActive(new GameTime());
                }
            }
#if RELEASE
});
#endif
        }
    }
}
