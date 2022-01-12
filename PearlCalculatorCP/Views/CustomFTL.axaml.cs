using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PearlCalculatorCP.Utils;

namespace PearlCalculatorCP.Views
{
    public class CustomFTL : UserControl
    {

        public CustomFTL()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void TNTAmount_OnValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
        {
            if (sender is NumericUpDown n)
                NumericBoxUtils.ToInt(n, e);
        }
    }
}