using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace PearlCalculatorCP.Converters
{
    public class IsEquals : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((value is null || parameter is null) && value != parameter)
                return false;

            if (value is null && parameter is null)
                return true;

            return value!.GetType() == parameter!.GetType() && value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}