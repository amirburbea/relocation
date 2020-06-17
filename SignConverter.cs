using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Relocation
{
    public sealed class SignConverter : IValueConverter
    {
        private static readonly object _negativeOne = -1;
        private static readonly object _one = 1;
        private static readonly object _zero = 0;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int number ? SignConverter.GetSign(number) : DependencyProperty.UnsetValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static object GetSign(int number) => Math.Sign(number) switch
        {
            1 => _one,
            -1 => _negativeOne,
            _ => _zero
        };
    }
}