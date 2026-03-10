using System;
using System.IO;

namespace LegacyConsoleLauncher
{
    public static class LauncherPaths
    {
        public static readonly string DataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LCELauncher");
        public static readonly string GameDir = Path.Combine(DataDir, "game");
        public static readonly string SkinsDir = Path.Combine(DataDir, "skins");
        public static readonly string AccountsFile = Path.Combine(DataDir, "accounts.txt");
        public static readonly string GamePathFile = Path.Combine(DataDir, "gamepath.txt");
        public static readonly string ReleaseInfoFile = Path.Combine(GameDir, "releaseinfo.txt");
    }
}
