using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PearlCalculatorCP.Views
{
    public class Manually : UserControl
    {


        private Button _tntAmountBtn;
        private Button _pearlTraceBtn;
        private Button _pearlMomentumBtn;
        

        public event EventHandler<RoutedEventArgs> CalculateTNTAmount;

        public event EventHandler<RoutedEventArgs> CalculatePearlTrace;

        public event EventHandler<RoutedEventArgs> CalculatePearlMomentum;
        
        public Manually()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _tntAmountBtn = this.FindControl<Button>("TNTAmountBtn");
            _pearlTraceBtn = this.FindControl<Button>("PearlTraceBtn");
            _pearlMomentumBtn = this.FindControl<Button>("PearlMomentumBtn");
        }

        private void TNTAmountBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            CalculateTNTAmount?.Invoke(this, e);
        }

        private void PearlTraceBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            CalculatePearlTrace?.Invoke(this, e);
        }

        private void PearlMomentumBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            CalculatePearlMomentum?.Invoke(this, e);
        }
    }
}