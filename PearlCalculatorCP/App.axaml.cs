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
        private CustomFontManagerImpl _fontManager = new CustomFontManagerImpl();

        private Dictionary<string, string?> _argsDict = new Dictionary<string, string?>();

        private Action _resetWindowScaleAction;

        public override void Initialize()
        {
            LoadLanguageSetting();
            CommandReg.Register();

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(ref _resetWindowScaleAction),
                };
                
                desktop.MainWindow.Closed += (sender, args) =>
                    AppSettings.Save();

                desktop.Startup += (sender, e) =>
                {
                    if (e.Args is null || e.Args.Length == 0) return;
                    
                    var args = e.Args;

                    for (int i = 0; i < args.Length; i++)
                    {
                        var k = args[i];

                        
                        if (_argsDict.ContainsKey(k)) continue;
                        
                        if (k.StartsWith('-'))
                        {
                            _argsDict.Add(k[1..], args[i + 1]);
                            i++;
                        }
                        else
                        {
                            _argsDict.Add(k, null);
                        }
                    }
                    
                    if (_argsDict.TryGetValue("scale", out var s) &&
                        double.TryParse(s, out var r))
                        APPRuntimeSettings.Settings.Add("scale", r);
                    
                    _resetWindowScaleAction.Invoke();
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public override void RegisterServices()
        {
            AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(_fontManager);
            base.RegisterServices();
        }

        private void LoadLanguageSetting()
        {
            var lang = AppSettings.Instance.Lanuage;
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
