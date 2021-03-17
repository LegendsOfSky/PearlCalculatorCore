using System.Diagnostics;

namespace PearlCalculatorCP
{
    static class UrlStartInfo
    {
        public static readonly ProcessStartInfo GithubUrlInfo = new ProcessStartInfo
        {
            FileName = "https://github.com/LegendsOfSky/PearlCalculatorCore",
            UseShellExecute = true
        };

        public static readonly ProcessStartInfo VideoUrlInfo = new ProcessStartInfo
        {
            FileName = null,
            UseShellExecute = true
        };

        public static readonly ProcessStartInfo DiscordUrlInfo = new ProcessStartInfo
        {
            FileName = "https://discord.gg/MMzsVuuSxT",
            UseShellExecute = true
        };
    }
}