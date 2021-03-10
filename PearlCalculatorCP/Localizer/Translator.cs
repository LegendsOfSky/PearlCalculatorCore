using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using JetBrains.Annotations;

namespace PearlCalculatorCP.Localizer
{
    public partial class Translator : INotifyPropertyChanged
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";

        public const string Fallbacklanguage = "en(fallback)";

        public event Action? OnLanguageChanged;
        
        private static Translator? _instance;
        public static Translator Instance => _instance ??= new Translator();


        private Dictionary<string, string>? _translateDict = null;
        
        
        public string CurrentLanguage { get; private set; } = string.Empty;

        public List<string> Languages { get; private set; } = new List<string>()
        {
            "zh_cn",
            "zh_tw",
            "en"
        };
        
        private Translator()
        {
        }

        public bool LoadLanguage(string language, Action<string>? exceptionMessageSender = null)
        {
            if (language == Fallbacklanguage)
            {
                LoadFallback();
                return true;
            }

            var path = Path.Combine(ProgramInfo.BaseDirectory, $"Assets/i18n/{language}.json");
            if (!File.Exists(path))
            {
                if (CurrentLanguage == string.Empty) //at app launch load i18n file
                    LoadFallback();
                return false;
            }
            
            bool isLoaded = false;
            using var sr = new StreamReader(path, Encoding.UTF8);
            
            try
            {
                _translateDict = JsonSerializer.Deserialize<Dictionary<string, string>>(sr.ReadToEnd());
                CurrentLanguage = language;
                Invalidate();
                OnLanguageChanged?.Invoke();
                isLoaded = true;
            }
            catch (Exception)
            {
                exceptionMessageSender?.Invoke($"\"{Path.GetRelativePath(ProgramInfo.BaseDirectory, path)}\" is not a qualified json file");
                exceptionMessageSender?.Invoke("it maybe not is json file or not is only string-string kv");
            }
            finally
            {
                if (CurrentLanguage == string.Empty)
                    LoadFallback();
            }
            
            return isLoaded;
        }

        private void LoadFallback()
        {
            if (CurrentLanguage == Fallbacklanguage) return;

            _translateDict = new Dictionary<string, string>();
            CurrentLanguage = Fallbacklanguage;
            Invalidate();
            OnLanguageChanged?.Invoke();
        }

        public object? this[string key]
        {
            get
            {
                if (_translateDict != null && _translateDict.TryGetValue(key, out var res) && 
                    !(string.IsNullOrWhiteSpace(res) || string.IsNullOrEmpty(res)))
                    return res;
                return _fallbackTranslateDict[key];
            }
        }

        public bool Exists(string? language)
        {
            if (language == CurrentLanguage)
                return true;
            
            return !string.IsNullOrWhiteSpace(language) && !string.IsNullOrEmpty(language) &&
                   Languages.Exists((opt) => opt.Equals(language));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerArrayName));
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}