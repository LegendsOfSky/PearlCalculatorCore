using Avalonia.Controls;

namespace PearlCalculatorCP.Utils
{
    public static class NumericBoxUtils
    {
        public static void ToInt(NumericUpDown? sender, NumericUpDownValueChangedEventArgs e)
        {
            var newValue = (int)e.NewValue;
            if (newValue != e.NewValue)
                sender.Value = newValue;
        }

        public static void ToUInt(NumericUpDown? sender, NumericUpDownValueChangedEventArgs e)
        {
            var newValue = (uint)e.NewValue;
            if (newValue != e.NewValue)
                sender.Value = newValue;

        }
    }
}