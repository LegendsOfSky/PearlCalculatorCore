using System.Collections.Generic;

namespace PearlCalculatorCP.Localizer
{
    internal class FallbackTranslate
    {
        private static FallbackTranslate? _instance;
        public static FallbackTranslate Instance => _instance ??= new FallbackTranslate();

        private FallbackTranslate()
        {

        }

        private Dictionary<string, string> translateDict = new Dictionary<string, string>(50);

        public string this[string key] => translateDict[key];

        public void AddFallbackItem(string key, string value)
        {
            if (!translateDict.ContainsKey(key))
                translateDict.Add(key ,value);
        }
    }
}