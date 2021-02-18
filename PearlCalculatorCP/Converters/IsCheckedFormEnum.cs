using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace PearlCalculatorCP.Converters
{
    public class IsCheckedFormEnum : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(true) == true ? parameter : BindingOperations.DoNothing;
        }
    }
}