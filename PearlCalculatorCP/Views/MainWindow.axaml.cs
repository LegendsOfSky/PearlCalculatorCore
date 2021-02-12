using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PearlCalculatorCP.ViewModels;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;

namespace PearlCalculatorCP.Views
{
    public class MainWindow : Window
    {

        private MainWindowViewModel? _vm;

        private int MaxTicks { get; set; } = 100;

        public MainWindow()
        {
            InitializeComponent();

            DataContextChanged += (sender, args) =>
            {
                _vm = DataContext as MainWindowViewModel;

                _vm.TNTWeightMode = MainWindowViewModel.TNTWeightModeEnum.DistanceVSTNT;
            };
            
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.FindControl<RadioButton>("DistanceVSTNTRB").IsChecked = true;
        }

        private void CalculateTNTBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            Calculation.CalculateTNTAmount(this.MaxTicks);
        }

        private void PearlSimulateBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ImportSettingsBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void SaveSettingsBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnDirectionSelectChanged(object? sender, RoutedEventArgs e)
        {
            if(sender is RadioButton rb)
                Data.Direction = DirectionUtils.FormName(rb.Content.ToString());
        }

        private void OnTNTWeightModeChanged(object? sender, RoutedEventArgs e)
        {
            if (sender is RadioButton {GroupName: "TNTAmountResultSort"} rb)
                _vm.TNTWeightMode = (MainWindowViewModel.TNTWeightModeEnum)int.Parse(rb.CommandParameter.ToString());
        }
    }
}
