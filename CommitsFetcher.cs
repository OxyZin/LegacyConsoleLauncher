using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

public class CommitsFetcher
{
    private readonly FlowLayoutPanel newsFlow;

    public CommitsFetcher(FlowLayoutPanel flow)
    {
        newsFlow = flow;
    }

    public async Task LoadCommits()
    {
        string url = "https://api.github.com/repos/smartcmd/MinecraftConsoles/commits?per_page=10";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", "LegacyConsoleLauncher");

            string json = await client.GetStringAsync(url);

            JArray commits = JArray.Parse(json);

            foreach (var commit in commits)
            {
                string sha = commit["sha"].ToString();
                string message = commit["commit"]["message"].ToString();

                AddCommit(sha, message);
            }
        }
    }

    private void AddCommit(string sha, string message)
    {
        string shortSha = sha.Substring(0, 7);

        LinkLabel commitLink = new LinkLabel();
        commitLink.Text = "#" + shortSha;
        commitLink.AutoSize = true;
        commitLink.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        commitLink.LinkColor = Color.LightBlue;
        commitLink.BackColor = Color.Transparent;

        commitLink.Click += (s, e) =>
        {
            string url = $"https://github.com/smartcmd/MinecraftConsoles/commit/{sha}";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        };

        Label commitText = new Label();
        commitText.Text = message;
        commitText.AutoSize = true;
        commitText.Font = new Font("Segoe UI", 9);
        commitText.ForeColor = Color.White;
        commitText.BackColor = Color.Transparent;

        newsFlow.Controls.Add(commitLink);
        newsFlow.Controls.Add(commitText);
    }
}