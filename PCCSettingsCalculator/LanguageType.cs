using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PCCSettingsGenerator
{
    public enum LanguageType
    {
        en,
        zh_TW,
        zh_CN
    }

    public static class LanguageTypeUtils
    {
        public static string ToString(LanguageType language)
        {
            return language.ToString().Replace("_" , "-");
        }

        public static bool TryPrase(string name , out LanguageType language)
        {
            return Enum.TryParse<LanguageType>(name.Replace("-" , "_") , out language);
        }
    }
}
