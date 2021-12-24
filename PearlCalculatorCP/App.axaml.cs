using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using PearlCalculatorCP.Localizer;
using PearlCalculatorCP.Models;
using PearlCalculatorCP.ViewModels;
using PearlCalculatorCP.Views;

using static PearlCalculatorCP.AppCommandLineArgDefinitions;
using static PearlCalculatorCP.ConstantDefinitions;

namespace PearlCalculatorCP
{
    public class App : Application
    {
        private CustomFontManagerImpl? _fontManager;

        public App()
        {
            InitAppRuntimeSettings();
            LoadLanguageSetting();
            CommandReg.Register();
        }
        
        public override void Initialize()
        {
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
                
                desktop.MainWindow.Closed += (_, _) =>
                    AppSettings.Save();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public override void RegisterServices()
        {
            if (!AppRuntimeSettings.UseSystemFont)
            {
                _fontManager = new CustomFontManagerImpl();
                AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(_fontManager);
            }

            base.RegisterServices();
        }

        private void LoadLanguageSetting()
        {
            var lang = AppSettings.Instance.Language;
            if (!string.IsNullOrEmpty(lang) && !string.IsNullOrWhiteSpace(lang))
            {
                var model = Translator.Instance.Languages.Find(e => e.Language == lang);
                if (model is not null && model.CanLoad())
                {
                    Translator.Instance.LoadLanguage(lang);
                    return;
                }
            }
            
            if (!Translator.Instance.LoadLanguage(DefaultLanguage))
                Translator.Instance.LoadLanguage(Translator.FallbackLanguage);
        }

        private void InitAppRuntimeSettings()
        {
            if (AppCommandLineArgs.Args.TryGetValue(Scale, out var s) && double.TryParse(s, out var r))
                AppRuntimeSettings.Scale = r;

            AppRuntimeSettings.UseSystemFont = AppCommandLineArgs.Args.ContainsKey(UseSystemFont);

        }
    }
}
