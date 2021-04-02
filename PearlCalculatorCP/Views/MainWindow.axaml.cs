#define ENABLE_ALL_SETTINGS

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

#if ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
using System.Text;
using System.Text.Json;
#endif

#if !ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
using System.Runtime.Serialization.Formatters.Binary;
#endif

#nullable disable

namespace PearlCalculatorCP.Views
{
    public class MainWindow : Window
    {

        private static readonly List<FileDialogFilter> FileDialogFilter = new List<FileDialogFilter>
        {
#if ENABLE_ALL_SETTINGS || !ENABLE_JSON_SETTINGS
            new FileDialogFilter
            {
                Name = "pcld",
                Extensions = {"pcld"}
            },
#endif
            
#if ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
            new FileDialogFilter
            {
                Name = "json",
                Extensions = {"json"}
            },
#endif
            
#if ENABLE_ALL_SETTINGS
            new FileDialogFilter
            {
                Name = "pcld;json",
                Extensions = {"pcld", "json"}
            },
#endif
        };

#if ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
        private static readonly SettingsJsonConverter JsonConverter = new SettingsJsonConverter();
        
        private static JsonSerializerOptions WriteSerializerOptions = new JsonSerializerOptions
        {
            Converters = { JsonConverter },
            WriteIndented = true
        };

        private static JsonSerializerOptions ReadSerializerOptions = new JsonSerializerOptions
        {
            Converters = {JsonConverter}
        };
#endif
        
        private MainWindowViewModel _vm;

        private TextBox _offsetXInputBox;
        private TextBox _offsetZInputBox;

        private TextBox _consoleInputBox;
        private ListBox _consoleOutputListBox;

        private Button _moreInfoBtn;
        
        //In TextBox, when set Text property, it set a field "_ignoreTextChanged"'s value to true
        //can't change the property when the field's value is true
        //so, I use reflection set the field to false
        //and then i can reset the property value
        private static readonly FieldInfo IgnoreTextChangesFieldInfo = typeof(TextBox).GetField("_ignoreTextChanges",
            BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField);

        private List<string> _commandHistory = new List<string>(100);
        private int _historyIndex = -1;
        
        public MainWindow()
        {
            InitializeComponent();

            DataContextChanged += (sender, args) =>
            {
                _vm = DataContext as MainWindowViewModel;

                _vm.OnPearlOffsetXTextChanged += (lastText, nextText, supressCallback) =>
                    OnPearlOffsetTextChanged(lastText, nextText, supressCallback, _offsetXInputBox);
                
                _vm.OnPearlOffsetZTextChanged += (lastText, nextText, supressCallback) =>
                        OnPearlOffsetTextChanged(lastText, nextText, supressCallback, _offsetZInputBox);
            };

            Title = ProgramInfo.Title;
            
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.FindControl<RadioButton>("DistanceVSTNTRB").IsChecked = true;

            _offsetXInputBox = this.FindControl<TextBox>("OffsetXInputBox");
            _offsetZInputBox = this.FindControl<TextBox>("OffsetZInputBox");

            _consoleInputBox = this.FindControl<TextBox>("ConsoleInputBox");
            _consoleOutputListBox = this.FindControl<ListBox>("ConsoleOutputListBox");

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
            
            var path = result[0];

#if ENABLE_ALL_SETTINGS
            if (Path.GetExtension(path) == ".json")
                ImportSettingsFormJson(path);
            else
                ImportSettingsFormPcld(path);
#elif ENABLE_JSON_SETTINGS
            ImportSettingsFormJson(path);
#else
            ImportSettingsFormPcld(path);
#endif
        }

#if ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
        
        private async void ImportSettingsFormJson(string path)
        {
            var json = await File.ReadAllTextAsync(path, Encoding.UTF8);
            try
            {
                _vm.LoadDataFormSettings(JsonSerializer.Deserialize<Settings>(json, ReadSerializerOptions));
            }
            catch (Exception)
            {
                _vm.ConsoleOutputs.Add(DefineCmdOutput.ErrorTemplate("settings json file format error"));
            }
        }
#endif
        
#if !ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
        private async void ImportSettingsFormPcld(string path)
        {
            await using var fs = File.OpenRead(path);
            if (new BinaryFormatter().Deserialize(fs) is Settings settings)
            {
                _vm.LoadDataFormSettings(settings);
            }
        }
#endif

        
        
        private async void SaveSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog {Filters = FileDialogFilter};
            var path = await dialog.ShowAsync(this);

            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrEmpty(path)) return;
            
#if ENABLE_ALL_SETTINGS
            if (Path.GetExtension(path) == ".json")
                SaveSettingsToJson(path);
            else
                SaveSettingsToPcld(path);
#elif ENABLE_JSON_SETTINGS
            SaveSettingsToJson(path);
#else
            SaveSettingsToPcld(path);
#endif
        }

#if ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
        private async void SaveSettingsToJson(string path)
        {
            var jsonStr = JsonSerializer.Serialize(Settings.CreateSettingsFormData(), WriteSerializerOptions);

            if (File.Exists(path)) File.Delete(path);

            using var sr = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
            var json = Encoding.UTF8.GetBytes(jsonStr);

            await sr.WriteAsync(json.AsMemory());
        }
#endif

#if !ENABLE_JSON_SETTINGS || ENABLE_ALL_SETTINGS
        private async void SaveSettingsToPcld(string path)
        {
            var bf = new BinaryFormatter();
            await using var fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bf.Serialize(fs, Settings.CreateSettingsFormData());
        }
#endif

        #endregion

        private (bool, double) OnPearlOffsetTextChanged(string lastText, string nextText, Action supressCallback, TextBox textBox)
        {
            if (nextText.Length < 2 || nextText[..2] != "0." || !double.TryParse(nextText, out var result))
            {
                supressCallback?.Invoke();
                IgnoreTextChangesFieldInfo.SetValue(textBox, false);
                textBox.Text = lastText;
                textBox.CaretIndex = lastText.Length;
                return (false, 0);
            }
            return (true, result);
        }

        private void OnCmdInput_KeyUp(object sender, KeyEventArgs e)
        {
            bool isSetCaretIndex = false;
            
            if (e.Key == Key.Enter)
            {
                _commandHistory.Add(_vm.CommandText);
                _historyIndex = -1;
                _vm.SendCmd();
                
                (_consoleOutputListBox.Scroll as ScrollViewer).ScrollToEnd();
            }
            else if (e.Key == Key.Up)
            {
                if (_historyIndex == -1)
                    _historyIndex = _commandHistory.Count - 1;
                else if (_historyIndex >= 0)
                {
                    if (--_historyIndex == -1)
                        _historyIndex = -2;
                }
                
                isSetCaretIndex = true;
            }
            else if (e.Key == Key.Down)
            {
                if (_historyIndex == -2)
                    _historyIndex = 0;
                else if (_historyIndex < _commandHistory.Count)
                    _historyIndex++;
                
                isSetCaretIndex = true;
            }

            if (isSetCaretIndex)
            {
                _vm.CommandText = _historyIndex >= 0 && _historyIndex < _commandHistory.Count
                    ? _commandHistory[_historyIndex]
                    : string.Empty;

                _consoleInputBox.CaretIndex = _vm.CommandText.Length;
            }
        }

        private void ManuallyCalculateTNTAmount(object sender, RoutedEventArgs e) => _vm.ManuallyCalculateTNTAmount();

        private void ManuallyCalculatePearlTrace(object sender, RoutedEventArgs e) => _vm.ManuallyCalculatePearlTrace();

        private void ManuallyCalculatePearlMomentum(object sender, RoutedEventArgs e) => _vm.ManuallyCalculatePearlMomentum();

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
    }

    static class MainWindowMoreInfoColor
    {
        public static readonly IBrush DefaultMoreInfoBrush = new ImmutableSolidColorBrush(Color.Parse("#CCCCCC"));
        public static readonly IBrush OnEnterMoreInfoBrush = new ImmutableSolidColorBrush(Color.Parse("#999999"));
    }
}
