using System;
using System.IO;

namespace LegacyConsoleLauncher
{
    public static class SettingsManager
    {
        private static readonly string LauncherDataDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LCELauncher");

        private static readonly string SettingsFile =
            Path.Combine(LauncherDataDir, "settings.txt");

        public static bool CheckGameUpdatesOnStartup { get; set; } = true;
        public static bool CheckLauncherUpdatesOnStartup { get; set; } = true;
        public static string AdditionalLaunchArgs { get; set; } = "";

        public static void Load()
        {
            Directory.CreateDirectory(LauncherDataDir);

            CheckGameUpdatesOnStartup = true;
            CheckLauncherUpdatesOnStartup = true;
            AdditionalLaunchArgs = "";

            if (!File.Exists(SettingsFile))
            {
                Save();
                return;
            }

            string[] lines = File.ReadAllLines(SettingsFile);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                {
                    continue;
                }

                string[] parts = line.Split(new[] { '=' }, 2);
                string key = parts[0].Trim();
                string value = parts[1].Trim();

                switch (key)
                {
                    case "CheckGameUpdatesOnStartup":
                        CheckGameUpdatesOnStartup = value.Equals("true", StringComparison.OrdinalIgnoreCase);
                        break;

                    case "CheckLauncherUpdatesOnStartup":
                        CheckLauncherUpdatesOnStartup = value.Equals("true", StringComparison.OrdinalIgnoreCase);
                        break;

                    case "AdditionalLaunchArgs":
                        AdditionalLaunchArgs = value;
                        break;
                }
            }
        }

        public static void Save()
        {
            Directory.CreateDirectory(LauncherDataDir);

            string[] lines =
            {
                "CheckGameUpdatesOnStartup=" + (CheckGameUpdatesOnStartup ? "true" : "false"),
                "CheckLauncherUpdatesOnStartup=" + (CheckLauncherUpdatesOnStartup ? "true" : "false"),
                "AdditionalLaunchArgs=" + (AdditionalLaunchArgs ?? "")
            };

            File.WriteAllLines(SettingsFile, lines);
        }

        public static void Reset()
        {
            CheckGameUpdatesOnStartup = true;
            CheckLauncherUpdatesOnStartup = true;
            AdditionalLaunchArgs = "";
            Save();
        }
    }
}