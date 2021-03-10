using System.Collections.Generic;

#nullable disable

namespace PearlCalculatorCP.Localizer
{
    public partial class Translator
    {
        
        private Dictionary<string, string> _fallbackTranslateDict = new Dictionary<string, string>(50);


        public void AddFallbackTranslate(string key, string value)
        {
            if (key is null) return;
 
            _fallbackTranslateDict.TryAdd(key, value);
        }

        public bool TryGetFallbackTranslate(string key, out string value) => _fallbackTranslateDict.TryGetValue(key, out value);
        
    }
}