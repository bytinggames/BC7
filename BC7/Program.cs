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
            CreateExampleYaml(settingsManager);

            if (settingsManager.Settings.ShuffleBotsOnce)
                bots.Shuffle(new Random());

#if RELEASE
CrashLogger.TryRun(paths.CrashLogFile, "Fonts/MessageBox", () =>
{
#endif
            GlobalGame? globalGame = null;
            using var game = new GameWrapper(g => globalGame = new GlobalGame(g, paths, settingsManager, bots.ToArray()), settingsManager.Settings.MSAA);
            if (settingsManager.Settings.GameSpeed < 2)
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

        private static void CreateExampleYaml(SettingsManager<Settings> settingsManager)
        {
            string? cSharpSettingsFilePath = null;
#if DEBUG
            cSharpSettingsFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (cSharpSettingsFilePath != null)
            {
                cSharpSettingsFilePath = Path.Combine(cSharpSettingsFilePath, "Settings.cs");
            }
#endif

            settingsManager.CreateExampleYamlFileIfNotExisting(cSharpSettingsFilePath);
        }
    }
}
