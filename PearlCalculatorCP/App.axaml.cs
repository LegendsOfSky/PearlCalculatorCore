using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using PearlCalculatorCP.Localizer;
using PearlCalculatorCP.ViewModels;
using PearlCalculatorCP.Views;

namespace PearlCalculatorCP
{
    public class App : Application
    {
        CustomFontManagerImpl _fontManager = new CustomFontManagerImpl();

        public override void Initialize()
        {
            LoadLanuageSetting();
            CommandReg.Register();

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };

                desktop.MainWindow.Closed += (sender, args) =>
                    AppSettings.Save();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public override void RegisterServices()
        {
            AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(_fontManager);
            base.RegisterServices();
        }

        private void LoadLanuageSetting()
        {
            var lang = AppSettings.Instance.Lanuage;
            if (lang != string.Empty &&
                Translator.Instance.Exists(lang))
            {
                Translator.Instance.LoadLanguage(lang);
            }
            else if (!Translator.Instance.LoadLanguage("en"))
                Translator.Instance.LoadLanguage(Translator.Fallbacklanguage);
        }
    }

#nullable disable
    class AppSettings
    {
        private static readonly string FilePath = $"{ProgramInfo.BaseDirectory}AppSettings.json";

        private static AppSettings _instance;

        public static AppSettings Instance
        {
            get
            {
                if (_instance is { }) return _instance;
                
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    _instance = JsonSerializer.Deserialize<AppSettings>(json);
                    return _instance;
                }

                return _instance = new AppSettings();
            }
        }

        private bool _isModify;

        private string _lanuage = string.Empty;
        private string _defaultLoadSettingsFile = string.Empty;


        public string Lanuage
        {
            get => _lanuage;
            set
            {
                _lanuage = value;
            }
        }

        public string DefaultLoadSettingsFile
        {
            get => _defaultLoadSettingsFile;
            set
            {
                _defaultLoadSettingsFile = value;
            }
        }

        private AppSettings() { }

        public static void Save()
        {
            if (!Instance._isModify && File.Exists(FilePath)) return;

            var json = JsonSerializer.SerializeToUtf8Bytes(Instance);
            using var fs = File.OpenWrite(FilePath);
            fs.SetLength(0);
            fs.Write(json);
        }

        public void OnModifyProperty() => _isModify = true;
    }
}
