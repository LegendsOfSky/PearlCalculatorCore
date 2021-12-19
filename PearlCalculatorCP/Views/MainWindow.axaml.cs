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
using Avalonia.Controls.Primitives;
using PearlCalculatorCP.Views.Panels;
using PearlCalculatorIntermediateLib.Settings;

#nullable disable

namespace PearlCalculatorCP.Views
{
    public class MainWindow : Window
    {

        private static readonly List<FileDialogFilter> FileDialogFilter = new()
        {
            new(){Name = "json", Extensions = {"json"}},
        };

        private bool _isLoadDefaultSettings;
        
        private MainWindowViewModel _vm;
        
        private Popup _appSettingsPopup;
        private SplitView _settingsSplitView;


        public MainWindow()
        {
            InitializeComponent();

            DataContextChanged += (_, _) =>
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
            _appSettingsPopup = this.FindControl<Popup>("AppSettingsPopup");
            _settingsSplitView = this.FindControl<SplitView>("SettingsSplitView");
        }

        private void Window_OnTapped(object sender, RoutedEventArgs e)
        {
            if (e.Source is not TextPresenter)
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
                _vm.LoadDataFormSettings(JsonUtils.DeSerialize(json));
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
            var settings = new SettingsCollection
            {
                Version = SettingsCollection.CurrentVersion,
                RedTNT = Data.RedTNT,
                BlueTNT = Data.BlueTNT,
                TNTWeight = Data.TNTWeight,
                SelectedCannon = string.Empty,
                Direction = Data.Direction,
                Destination = Data.Destination.ToSurface2D(),
                CannonSettings = new[]
                {
                    new CannonSettings
                    {
                        CannonName = "Default",
                        MaxTNT = Data.MaxTNT,
                        DefaultRedDirection = Data.DefaultRedDuper,
                        DefaultBlueDirection = Data.DefaultBlueDuper,
                        NorthWestTNT = Data.NorthWestTNT,
                        NorthEastTNT = Data.NorthEastTNT,
                        SouthWestTNT = Data.NorthWestTNT,
                        SouthEastTNT = Data.SouthEastTNT,
                        Pearl = Data.Pearl,
                        Offset = Data.PearlOffset,
                        RedTNTConfiguration = Data.RedTNTConfiguration,
                        BlueTNTConfiguration = Data.BlueTNTConfiguration
                    }
                }
            };

            var json = JsonUtils.SerializeToUtf8Bytes(settings);

            using var sr = File.OpenWrite(path);
            sr.SetLength(0);
            await sr.WriteAsync(json.AsMemory());
        }

#endregion

        private void NumericUpDownToUInt_OnValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
        {
            NumericBoxUtils.ToUInt(sender as NumericUpDown, e);
        }
        
        private void OpenGithubLink(object sender, RoutedEventArgs e) => UrlUtils.OpenUrl("https://github.com/LegendsOfSky/PearlCalculatorCore");
        
        private void OpenDiscordLink(object sender, RoutedEventArgs e) => UrlUtils.OpenUrl("https://discord.gg/MMzsVuuSxT");

        private async void SetDefaultSettings(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filters = FileDialogFilter};
            var result = await dialog.ShowAsync(this);

            if (result == null || result.Length == 0 || !File.Exists(result[0])) return;

            CommandManager.Instance.ExecuteCommand($"setDefaultSettings {result[0]}", false);
        }

        private void AppSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_settingsSplitView.IsPaneOpen)
                _settingsSplitView.IsPaneOpen = false;
            
            _appSettingsPopup.Open();
        }

        private void SwitchSplitOpenState(object sender, RoutedEventArgs e)
        {
            _settingsSplitView.IsPaneOpen = !_settingsSplitView.IsPaneOpen;
        }
    }
}
