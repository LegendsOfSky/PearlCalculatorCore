using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia;
using Avalonia.Data;
using Avalonia.Platform;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace PearlCalculatorCP.Localizer
{
    public class Translator : INotifyPropertyChanged
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";

        public const string Fallbacklanguage = "en(fallback)";

        public event Action? OnLanguageChanged;
        
        private static Translator? _instance;
        public static Translator Instance => _instance ??= new Translator();

        private Dictionary<string, string>? _translateDict = null;
        
        public string CurrentLanguage { get; private set; }

        public List<string> Languages { get; private set; } = new List<string>()
        {
            "zh_cn",
            "zh_tw",
            "en"
        };
        
        private Translator()
        { 
            
        }

        public bool LoadLanguage(string language)
        {
            if (language == Fallbacklanguage)
            {
                _translateDict = new Dictionary<string, string>();
                CurrentLanguage = language;
                Invalidate();
                OnLanguageChanged?.Invoke();
                return true;
            }
            
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}Assets/i18n/{language}.json";

            if (File.Exists(path))
            {
                using var sr = new StreamReader(path, Encoding.UTF8);
                _translateDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
                CurrentLanguage = language;
                Invalidate();
                OnLanguageChanged?.Invoke();
                return true;
            }
            return false;
        }

        public object? this[string key]
        {
            get
            {
                if (_translateDict != null && _translateDict.TryGetValue(key, out var res) && 
                    !(string.IsNullOrWhiteSpace(res) || string.IsNullOrEmpty(res)))
                    return res;
                return FallbackTranslate.Instance[key];
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerArrayName));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}