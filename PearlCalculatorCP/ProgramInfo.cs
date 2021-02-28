using System.Reflection;

namespace PearlCalculatorCP
{
    public static class ProgramInfo
    {
        public static string Version { get; set; }

        public static string Title => $"PearlCalculator v{Version}";

        public static void Init()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()[0..^4];
        }
    }
}
