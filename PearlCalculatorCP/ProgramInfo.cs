using System;
using System.Reflection;

#nullable disable

namespace PearlCalculatorCP
{
    public static class ProgramInfo
    {
        public static string Version { get; private set; }

        public static string Title => $"PearlCalculator v{Version}";

        public static string BaseDirectory { get; private set; }

        static ProgramInfo()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()[..^4];
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
