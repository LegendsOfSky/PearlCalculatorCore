using System.Collections.Generic;
using PearlCalculatorCP.Localizer;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class AppSettingsPanelViewModel : ViewModelBase
    {
        public class LanguageComboBoxModel
        {
            public string DisplayName { get; private set; }
            public string CommandOption { get; private set; }

            public LanguageComboBoxModel(string? displayName, string? commandOption)
            {
                DisplayName = displayName ?? "Empty";
                CommandOption = commandOption ?? string.Empty;
            }
        }
        
        private bool _enableChunkMode;
        public bool EnableChunkMode
        {
            get => _enableChunkMode;
            set
            {
                _enableChunkMode = value;
                EventManager.PublishEvent(this, "switchChunkMode", new SwitchChunkModeArgs("AppSettings", value));
            }
        }

        public List<LanguageComboBoxModel> Languages { get; private set; }

        private LanguageComboBoxModel _curSelectLanguage;
        public LanguageComboBoxModel CurSelectLanguage
        {
            get => _curSelectLanguage;
            set
            {
                if (_curSelectLanguage != value)
                    RaiseAndSetProperty(ref _curSelectLanguage, value);
                ChangeLanguageOptional(value.CommandOption);
            }
        }

        public AppSettingsPanelViewModel()
        {
            Languages = new List<LanguageComboBoxModel>(Translator.Instance.Languages.Count + 1);
            
            foreach (var lang in Translator.Instance.Languages)
            {
                Languages.Add(new(lang.DisplayName, lang.FileName));
            }
            Languages.Add(new("EN (Fallback)", Translator.FallbackLanguage));

            var cur = Translator.Instance.CurrentLanguage;
            _curSelectLanguage = Languages.Find(e => e.CommandOption == cur)!;

            Translator.Instance.OnLanguageChanged += lang =>
            {
                var langOptModel = Languages.Find(e => e.CommandOption == lang)!;
                RaiseAndSetProperty(ref _curSelectLanguage, langOptModel, nameof(CurSelectLanguage));
            };
        }
        
        public void ChangeLanguageOptional(string lang)
        {
            CommandManager.Instance.ExecuteCommand(
                Translator.Instance.CurrentLanguage == lang
                    ? $"setDefaultLang {lang}"
                    : $"changeLang {lang}");
        }
    }
}