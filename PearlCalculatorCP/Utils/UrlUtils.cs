using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace PearlCalculatorCP.Utils
{
    static class UrlUtils
    {
        private static ProcessStartInfo UrlStartInfo = new ProcessStartInfo { UseShellExecute = true };

        private static readonly Regex UrlRegex = new Regex(@"^(https?|ftp|file|ws)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");


        public static bool OpenUrl(string? url)
        {
            if (!Check(url)) return false;

            UrlStartInfo.FileName = url;
            Process.Start(UrlStartInfo);
            return true;
        }

        public static bool Check(string? url) => !string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url) && UrlRegex.IsMatch(url);
    }
}
