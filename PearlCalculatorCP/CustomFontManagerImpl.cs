using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PearlCalculatorCP
{
    class CustomFontManagerImpl : IFontManagerImpl
    {
        private readonly Typeface[] _customTypefaces;
        private readonly string _defaultFamilyName;

        private readonly Typeface _defaultTypeface;

        private SKTypeface _defaultSKT;

        public CustomFontManagerImpl()
        {
            var filePath = Path.Combine(ProgramInfo.BaseDirectory, "Assets/Fonts/SourceHanSansSC-Normal.otf");

            _defaultSKT = SKTypeface.FromFile(filePath);
            _defaultTypeface = new Typeface($"{filePath}#{_defaultSKT.FamilyName}");

            _customTypefaces = new[] { _defaultTypeface };
            _defaultFamilyName = _defaultTypeface.FontFamily.FamilyNames.PrimaryFamilyName;
        }

        public IGlyphTypefaceImpl CreateGlyphTypeface(Typeface typeface)
        {
            return new GlyphTypefaceImpl(_defaultSKT);
        }

        public string GetDefaultFontFamilyName() => _defaultFamilyName;
        
        private readonly string[] _bcp47 = { CultureInfo.CurrentCulture.ThreeLetterISOLanguageName, CultureInfo.CurrentCulture.TwoLetterISOLanguageName };

        public IEnumerable<string> GetInstalledFontFamilyNames(bool checkForUpdates = false) =>
            _customTypefaces.Select(x => x.FontFamily.Name);

        public bool TryMatchCharacter(int codepoint, FontStyle fontStyle, FontWeight fontWeight, FontFamily fontFamily, CultureInfo culture, out Typeface typeface)
        {
            foreach (var customTypeface in _customTypefaces)
            {
                if (customTypeface.GlyphTypeface.GetGlyph((uint)codepoint) == 0)
                    continue;

                typeface = new Typeface(customTypeface.FontFamily.Name, fontStyle, fontWeight);

                return true;
            }

            typeface = new Typeface(_defaultFamilyName, fontStyle, fontWeight);

            return true;
        }
    }
}
