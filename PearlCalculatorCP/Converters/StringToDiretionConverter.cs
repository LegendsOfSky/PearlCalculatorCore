using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorCP.Converters
{
    public class StringToDiretionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null 
                ? throw new ArgumentException("Why the control is null?????!!!!!") 
                : Enum.Parse<Direction>(((ComboBoxItem)value).Content.ToString());
        }
    }
}