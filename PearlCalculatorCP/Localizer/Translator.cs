using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using JetBrains.Annotations;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Localizer
{
    public partial class Translator : INotifyPropertyChanged
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";

        public const string FallbackLanguage = "en(fallback)";

        public event Action<string>? OnLanguageChanged;
        
        private static Translator? _instance;
        public static Translator Instance => _instance ??= new Translator();


        private Dictionary<string, string>? _translateDict = null;
        
        
        public string CurrentLanguage { get; private set; } = string.Empty;

        public string CurrentActivedI18nFile => CurrentLanguage == FallbackLanguage || CurrentLanguage == string.Empty
            ? string.Empty
            : $"{CurrentLanguage}.json";

        public List<TranslateFileModel> Languages { get; private set; } = new()
        {
            new(){FileName = "en", Language = "en", DisplayName = "EN"},
            new(){FileName = "zh_cn", Language = "zh_cn", DisplayName = "中文(简体)"},
            new(){FileName = "zh_tw", Language = "zh_tw", DisplayName = "中文(繁体)"},
        };
        
        private Translator()
        {
        }

        public bool LoadLanguage(string language, Action<string>? exceptionMessageSender = null)
        {
            if (language == FallbackLanguage)
            {
                LoadFallback();
                return true;
            }

            var model = Languages.Find(e => e.Language == language);
            var path = model is null ? string.Empty : Path.Combine(ProgramInfo.BaseDirectory, $"Assets/i18n/{model.FileName}.json");
            
            if (!File.Exists(path))
            {
                if (CurrentLanguage == string.Empty) //at app launch load i18n file
                    LoadFallback();
                else
                    exceptionMessageSender?.Invoke($"file \"{language}.json\" not found");
                
                return false;
            }
            
            bool isLoaded = false;
            using var sr = new StreamReader(path, Encoding.UTF8);
            
            try
            {
                _translateDict = JsonSerializer.Deserialize<Dictionary<string, string>>(sr.ReadToEnd());
                CurrentLanguage = language;
                OnLanguageChanged?.Invoke(language);
                Invalidate();
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
            if (CurrentLanguage == FallbackLanguage) return;

            _translateDict = new Dictionary<string, string>();
            CurrentLanguage = FallbackLanguage;
            OnLanguageChanged?.Invoke(FallbackLanguage);
            Invalidate();
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

        public bool TryAddTranslate(string? key, string? value)
        {
            return key is { } && _translateDict!.TryAdd(key, value ?? string.Empty);
        }

        public bool Exists(string? language)
        {
            if (language == CurrentLanguage)
                return true;
            
            return !string.IsNullOrWhiteSpace(language) && !string.IsNullOrEmpty(language) &&
                   Languages.Exists(opt => opt.Language!.Equals(language));
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