using System.Collections.Generic;

namespace PearlCalculatorCP.Localizer
{
    public partial class Translator
    {
        private class FallbackTranslate
        {
            public Dictionary<string, string> TranslateDict { get; set; } = new Dictionary<string, string>(50);

            public string this[string key] => TranslateDict[key];

            public void AddFallbackItem(string key, string value)
            {
                if (!TranslateDict.ContainsKey(key))
                    TranslateDict.Add(key ,value);
            }
        }
    }
}