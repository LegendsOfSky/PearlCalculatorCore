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

        public int MaxTicks { get; private set; } = 100;

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

        private void CalculateTNTBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Calculation.CalculateTNTAmount(this.MaxTicks, 10);
        }

        private void PearlSimulateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region Settings Import/Export

        private async void ImportSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filters = FileDialogFilter, AllowMultiple = false};
            var result = await dialog.ShowAsync(this.GetVisualRoot() as Window);

            if (result != null && File.Exists(result[0]))
            {
                var path = result[0];
                using var fs = File.OpenRead(path);
                if (new BinaryFormatter().Deserialize(fs) is Settings settings)
                {
                    ImportSettings(settings);
                }
            }
        }

        private async void SaveSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog {Filters = FileDialogFilter};
            var path = await dialog.ShowAsync(this.GetVisualRoot() as Window);

            if (!string.IsNullOrWhiteSpace(path) && !string.IsNullOrEmpty(path))
            {
                var bf = new BinaryFormatter();
                using var fs = File.Open(path, FileMode.OpenOrCreate);
                bf.Serialize(fs, Settings.GetSettingsFormData());
            }
        }
        
        private void ImportSettings(Settings settings)
        {
            Data.NorthWestTNT = settings.NorthWestTNT;
            Data.NorthEastTNT = settings.NorthEastTNT;
            Data.SouthWestTNT = settings.SouthWestTNT;
            Data.SouthEastTNT = settings.SouthEastTNT;

            Data.Pearl = settings.Pearl;
            
            Data.Destination = settings.Destination;
            
            _vm.PearlPosX = settings.Pearl.Position.X;
            _vm.PearlPosZ = settings.Pearl.Position.Z;
            _vm.DestinationX = settings.Destination.X;
            _vm.DestinationZ = settings.Destination.Z;
            _vm.PearlOffsetX = settings.Offset.X.ToString();
            _vm.PearlOffsetZ = settings.Offset.Z.ToString();
            _vm.BlueTNT = (uint)settings.BlueTNT;
            _vm.RedTNT = (uint) settings.RedTNT;
            _vm.MaxTNT = (uint)settings.MaxTNT;

            if (this.FindControl<RadioButton>($"{settings.Direction.ToString()}RB") is { } rb)
                rb.IsChecked = true;
        }

        #endregion
        
        private void OnDirectionSelectChanged(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton {GroupName:"DirectionSelectGroup"} rb)
                _vm.Direction = DirectionUtils.FormName(rb.Content.ToString());
        }

        private void OnTNTWeightModeChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton {GroupName: "TNTAmountResultSortMode"} rb)
                _vm.TNTWeightMode = (MainWindowViewModel.TNTWeightModeEnum)int.Parse(rb.CommandParameter.ToString());
        }

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
