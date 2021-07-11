using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using PearlCalculatorCP.Utils;
using PearlCalculatorCP.ViewModels;
using PearlCalculatorLib.General;
using System.Text;
using System.Text.Json;

#nullable disable

namespace PearlCalculatorCP.Views
{
    public class MainWindow : Window
    {

        private static readonly List<FileDialogFilter> FileDialogFilter = new List<FileDialogFilter>
        {
            new FileDialogFilter
            {
                Name = "json",
                Extensions = {"json"}
            },
        };
        
        private static readonly SettingsJsonConverter JsonConverter = new SettingsJsonConverter();
        
        private static readonly JsonSerializerOptions WriteSerializerOptions = new JsonSerializerOptions
        {
            Converters = { JsonConverter },
            WriteIndented = true
        };

        private static readonly JsonSerializerOptions ReadSerializerOptions = new JsonSerializerOptions
        {
            Converters = { JsonConverter }
        };
        
        private bool _isLoadDefaultSettings;
        
        private MainWindowViewModel _vm;

        private Button _moreInfoBtn;
        
        
        public MainWindow()
        {
            InitializeComponent();

            DataContextChanged += (sender, args) =>
            {
                _vm = DataContext as MainWindowViewModel;
                
                if (!_isLoadDefaultSettings)
                {
                    var path = AppSettings.Instance.DefaultLoadSettingsFile;
                    if (File.Exists(path))
                        ImportSettings(path);
                    _isLoadDefaultSettings = true;
                }
            };

            Title = ProgramInfo.Title;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            //this.FindControl<RadioButton>("MixedWeightRB").IsChecked = true;

            _moreInfoBtn = this.FindControl<Button>("MoreInfoBtn");
        }

        private void Window_OnTapped(object sender, RoutedEventArgs e)
        {
            if (!(e.Source is TextPresenter))
                this.Focus();
        }

        #region Settings Import/Export

        private async void ImportSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filters = FileDialogFilter, AllowMultiple = false};
            var result = await dialog.ShowAsync(this);

            if (result == null || result.Length == 0 || !File.Exists(result[0])) return;
            ImportSettings(result[0]);
        }

        public async void ImportSettings(string path)
        {
            var json = await File.ReadAllTextAsync(path, Encoding.UTF8);
            try
            {
                _vm.LoadDataFormSettings(JsonSerializer.Deserialize<Settings>(json, ReadSerializerOptions));
            }
            catch (Exception)
            {
                LogUtils.Error("settings json file format error");
            }
        }
        
        private async void SaveSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog {Filters = FileDialogFilter};
            var path = await dialog.ShowAsync(this);

            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrEmpty(path)) return;
            SaveSettings(path);
        }
        
        private async void SaveSettings(string path)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(Settings.CreateSettingsFormData(), WriteSerializerOptions);

            using var sr = File.OpenWrite(path);
            sr.SetLength(0);
            await sr.WriteAsync(json.AsMemory());
        }

        #endregion

        private void NumericUpDownToUInt_OnValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
        {
            NumericBoxUtils.ToUInt(sender as NumericUpDown, e);
        }

        private void MoreInfoBtn_OnPointerEnter(object sender, PointerEventArgs e) => _vm.MoreInfoBrush = MainWindowMoreInfoColor.OnEnterMoreInfoBrush;

        private void MoreInfoBtn_OnPointerLeave(object sender, PointerEventArgs e) => _vm.MoreInfoBrush = MainWindowMoreInfoColor.DefaultMoreInfoBrush;

        private void MoreInfoBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _moreInfoBtn.ContextMenu.PlacementTarget = (Control)sender;
            _moreInfoBtn.ContextMenu.PlacementMode = PlacementMode.Bottom;
            _moreInfoBtn.ContextMenu.Open();
        }

        //not link
        private void OpenVideoLink(object sender, RoutedEventArgs e) => UrlUtils.OpenUrl(null);
        
        private void OpenGithubLink(object sender, RoutedEventArgs e) => UrlUtils.OpenUrl("https://github.com/LegendsOfSky/PearlCalculatorCore");

        private void OpenAboutWindow(object sender, RoutedEventArgs e) => AboutWindow.OpenWindow(this);

        private async void SetDefaultSettings(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filters = FileDialogFilter};
            var result = await dialog.ShowAsync(this);

            if (result == null || result.Length == 0 || !File.Exists(result[0])) return;

            CommandManager.Instance.ExecuteCommand($"setDefaultSettings {result[0]}", false);
        }
    }

    static class MainWindowMoreInfoColor
    {
        public static readonly IBrush DefaultMoreInfoBrush = new ImmutableSolidColorBrush(Color.Parse("#CCCCCC"));
        public static readonly IBrush OnEnterMoreInfoBrush = new ImmutableSolidColorBrush(Color.Parse("#999999"));
    }
}
