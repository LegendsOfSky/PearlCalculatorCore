using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using PearlCalculatorCP.ViewModels;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;

#nullable disable

namespace PearlCalculatorCP.Views
{
    public class MainWindow : Window
    {
        private static readonly List<FileDialogFilter> FileDialogFilter = new List<FileDialogFilter>
        {
            new FileDialogFilter
            {
                Name = "pcld",
                Extensions = {"pcld"}
            }
        };
        
        private MainWindowViewModel _vm;

        private TextBox _offsetXInputBox;
        private TextBox _offsetZInputBox;
        
        //In TextBox, when set Text property, it set a field "_ignoreTextChanged"'s value to true
        //can't change the property when the field's value is true
        //so, I use reflection set the field to false
        //and then i can reset the property value
        private static readonly FieldInfo IgnoreTextChangesFieldInfo = typeof(TextBox).GetField("_ignoreTextChanges",
            BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField);
        
        public MainWindow()
        {
            InitializeComponent();

            DataContextChanged += (sender, args) =>
            {
                _vm = DataContext as MainWindowViewModel;

                _vm.OnPearlOffsetXTextChanged += (lastText, nextText, supressCallback, backingField) =>
                    OnPearlOffsetTextChanged(lastText, nextText, supressCallback, _offsetXInputBox, backingField);
                
                _vm.OnPearlOffsetZTextChanged += (lastText, nextText, supressCallback, backingField) =>
                        OnPearlOffsetTextChanged(lastText, nextText, supressCallback, _offsetZInputBox, backingField);
            };
            
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.FindControl<RadioButton>("DistanceVSTNTRB").IsChecked = true;

            _offsetXInputBox = this.Find<TextBox>("OffsetXInputBox");
            _offsetZInputBox = this.Find<TextBox>("OffsetZInputBox");
        }

        #region Settings Import/Export

        private async void ImportSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filters = FileDialogFilter, AllowMultiple = false};
            var result = await dialog.ShowAsync(this.GetVisualRoot() as Window);

            if (result == null || result.Length == 0 || !File.Exists(result[0])) return;
            
            var path = result[0];
            await using var fs = File.OpenRead(path);
            if (new BinaryFormatter().Deserialize(fs) is Settings settings)
            {
                _vm.LoadDataFormSettings(settings);
            }
        }

        private async void SaveSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog {Filters = FileDialogFilter};
            var path = await dialog.ShowAsync(this.GetVisualRoot() as Window);

            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrEmpty(path)) return;
            
            var bf = new BinaryFormatter();
            await using var fs = File.Open(path, FileMode.OpenOrCreate);
            bf.Serialize(fs, Settings.CreateSettingsFormData());
        }

        #endregion

        private (bool, double) OnPearlOffsetTextChanged(string lastText, string nextText, Action supressCallback, TextBox textBox, double backingField)
        {
            if (nextText.Length < 2 || nextText[..2] != "0." ||
                !(double.TryParse(nextText, out var result) && result != backingField))
            {
                supressCallback?.Invoke();
                IgnoreTextChangesFieldInfo.SetValue(textBox, false);
                textBox.Text = lastText;
                textBox.CaretIndex = lastText.Length;
                return (false, 0);
            }
            return (true, result);

        }

    }
}
