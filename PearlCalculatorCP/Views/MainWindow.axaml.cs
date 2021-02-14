using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PearlCalculatorCP.ViewModels;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;

#nullable disable

namespace PearlCalculatorCP.Views
{
    public class MainWindow : Window
    {
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

                _vm.TNTWeightMode = MainWindowViewModel.TNTWeightModeEnum.DistanceVSTNT;

                _vm.OnPearlOffsetXTextChanged += OnPearlOffsetXTextChanged;
                _vm.OnPearlOffsetZTextChanged += OnPearlOffsetZTextChanged;
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

        private void ImportSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDirectionSelectChanged(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton rb)
                Data.Direction = DirectionUtils.FormName(rb.Content.ToString());
        }

        private void OnTNTWeightModeChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton {GroupName: "TNTAmountResultSort"} rb)
                _vm.TNTWeightMode = (MainWindowViewModel.TNTWeightModeEnum)int.Parse(rb.CommandParameter.ToString());
        }

        private bool OnPearlOffsetXTextChanged(string lastText, string nextText) =>
            OnPearlOffsetTextChanged(lastText, nextText, _vm.SupressNextOffsetXUpdate, _offsetXInputBox, ref Data.PearlOffset.X);

        private bool OnPearlOffsetZTextChanged(string lastText, string nextText) => 
            OnPearlOffsetTextChanged(lastText, nextText, _vm.SupressNextOffsetZUpdate, _offsetZInputBox, ref Data.PearlOffset.Z);

        private bool OnPearlOffsetTextChanged(string lastText, string nextText, Action supressCallback, TextBox textBox, ref double backingField)
        {
            if (nextText.Length < 2 || nextText.Substring(0, 2) != "0." ||
                !(double.TryParse(nextText, out var result) && result != backingField))
            {
                supressCallback.Invoke();
                IgnoreTextChangesFieldInfo.SetValue(textBox, false);
                textBox.Text = lastText;
                textBox.CaretIndex = lastText.Length;
                return false;
            }

            backingField = result;
            return true;

        }

    }
}
