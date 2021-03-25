using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PearlCalculatorCP
{
    class CustomFontManagerImpl : IFontManagerImpl
    {
        private readonly Typeface[] _customTypefaces;
        private readonly string _defaultFamilyName;

        //需要填充这个字段的值为嵌入的字体资源路径
        private readonly Typeface _defaultTypeface = new Typeface("resm:PearlCalculatorCP.Assets.Fonts.SourceHanSansSC-Normal#Source Han Sans SC");

        public CustomFontManagerImpl()
        {
            _customTypefaces = new[] { _defaultTypeface };
            _defaultFamilyName = _defaultTypeface.FontFamily.FamilyNames.PrimaryFamilyName;
        }

        public IGlyphTypefaceImpl CreateGlyphTypeface(Typeface typeface)
        {
            var skTypeface = SKTypeface.FromFamilyName(_defaultTypeface.FontFamily.Name);
            return new GlyphTypefaceImpl(skTypeface);
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
