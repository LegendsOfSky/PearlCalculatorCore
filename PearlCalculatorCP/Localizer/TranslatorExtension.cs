using System.Text;

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
            foreach (var lang in Translator.Instance.Languages)
                sb.Append($"\"{lang}\" , ");

            _langOpt = sb.ToString();
            return _langOpt;
        }
    }
}