using System;

namespace PearlCalculatorCP.Models
{
    public class TranslateFileModel
    {
        public string? Language { get; init; }
        public string? FileName { get; init; }
        public string? DisplayName { get; init; }
        public TranslateFileLoadTypes LoadTypes { get; init; }

        public bool CanLoad(bool isSystemFont)
        {
            return isSystemFont
                ? (LoadTypes & TranslateFileLoadTypes.SystemFont) != 0
                : (LoadTypes & TranslateFileLoadTypes.BuiltInFont) != 0;
        }
    }

    [Flags]
    public enum TranslateFileLoadTypes
    {
        BuiltInFont = 1,
        SystemFont = 2,
        All = BuiltInFont | SystemFont
    }
}