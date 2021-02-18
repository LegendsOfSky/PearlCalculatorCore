using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Utilities;

namespace PearlCalculatorCP.Converters
{
    public class StringToNumericConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return !targetType.IsValueType ? (object) null : AvaloniaProperty.UnsetValue;

            object result;
            return TypeUtilities.TryConvert(targetType, value, culture, out result) ? result : (object) new BindingNotification((Exception) 
                new InvalidCastException(string.Empty), BindingErrorType.Error);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}