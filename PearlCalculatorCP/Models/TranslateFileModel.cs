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

    public static class TranslateFileModelExtension
    {
        public static bool CanLoad(this TranslateFileModel model) => model.CanLoad(AppRuntimeSettings.UseSystemFont);
    }

    [Flags]
    public enum TranslateFileLoadTypes
    {
        BuiltInFont = 0b0001,
        SystemFont  = 0b0010,
        All         = 0b0011
    }
}