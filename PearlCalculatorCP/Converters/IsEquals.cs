using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace PearlCalculatorCP.Converters
{
    public class IsEquals : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.GetType() == parameter?.GetType() && value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}