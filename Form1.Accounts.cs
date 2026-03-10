using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LegacyConsoleLauncher
{
    public partial class Form1
    {
        private void LoadAccounts()
        {
            usernameComboBox.Items.Clear();
            playtimeData.Clear();

            if (!File.Exists(LauncherPaths.AccountsFile))
            {
                return;
            }

            string[] lines = File.ReadAllLines(LauncherPaths.AccountsFile);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                string username = parts[0].Trim();

                if (string.IsNullOrWhiteSpace(username))
                {
                    continue;
                }

                int seconds = 0;

                if (parts.Length > 1)
                {
                    int.TryParse(parts[1], out seconds);
                }

                if (!playtimeData.ContainsKey(username))
                {
                    playtimeData[username] = seconds;
                    usernameComboBox.Items.Add(username);
                }
            }

            if (usernameComboBox.Items.Count > 0)
            {
                usernameComboBox.SelectedIndex = 0;
            }
        }

        private void SaveAccounts()
        {
            List<string> lines = new List<string>();

            foreach (var entry in playtimeData)
            {
                lines.Add(entry.Key + "|" + entry.Value);
            }

            Directory.CreateDirectory(LauncherPaths.DataDir);
            File.WriteAllLines(LauncherPaths.AccountsFile, lines);
        }

        private void AddAccount(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            if (!playtimeData.ContainsKey(username))
            {
                playtimeData[username] = 0;
                usernameComboBox.Items.Add(username);
            }

            usernameComboBox.SelectedItem = username;
            SaveAccounts();
        }

        private string FormatPlaytime(int totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return ((int)time.TotalHours) + "h " + time.Minutes + "m";
        }

        private void UpdatePlaytimeLabel()
        {
            string username = usernameComboBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                playtimeLabel.Text = "Playtime: 0h 0m";
                return;
            }

            if (!playtimeData.ContainsKey(username))
            {
                playtimeData[username] = 0;
            }

            playtimeLabel.Text = "Playtime: " + FormatPlaytime(playtimeData[username]);
        }
    }
}