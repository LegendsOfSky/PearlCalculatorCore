using System;
using System.Collections.Generic;
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
        private CustomFontManagerImpl? _fontManager;

        public App()
        {
            LoadLanguageSetting();
            CommandReg.Register();
            
            if (AppCommandLineArgs.Args.TryGetValue("scale", out var s) && double.TryParse(s, out var r))
                AppRuntimeSettings.Settings.Add("scale", r);
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
            if (!AppCommandLineArgs.Args.ContainsKey("useSystemFont"))
            {
                _fontManager = new CustomFontManagerImpl();
                AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(_fontManager);
            }

            base.RegisterServices();
        }

        private void LoadLanguageSetting()
        {
            var lang = AppSettings.Instance.Language;
            if (lang != string.Empty &&
                Translator.Instance.Exists(lang))
            {
                Translator.Instance.LoadLanguage(lang);
            }
            else if (!Translator.Instance.LoadLanguage("en"))
                Translator.Instance.LoadLanguage(Translator.FallbackLanguage);
        }
    }
}
