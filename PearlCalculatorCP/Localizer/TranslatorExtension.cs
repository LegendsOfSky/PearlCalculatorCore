using System.Text;

using static PearlCalculatorCP.AppCommandLineArgDefinitions;

namespace PearlCalculatorCP.Localizer
{
    public static class TranslatorExtension
    {
        private static string _langOpt = string.Empty;
        
        public static string GetLanguagesOptional(this Translator translator)
        {
            if (_langOpt != string.Empty)
                return _langOpt;
            
            StringBuilder sb = new StringBuilder();
            foreach (var lang in translator.Languages)
            {
                if (lang.CanLoad(AppRuntimeSettings.IsDefined(UseSystemFont)))
                    sb.Append($"\"{lang.Language}\"  ");
            }

            _langOpt = sb.ToString();
            return _langOpt;
        }
    }
}