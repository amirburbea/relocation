using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Relocation
{
    public sealed class TooManyPointsConverter : IMultiValueConverter
    {
        private static readonly object _false = false;
        private static readonly object _true = true;

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length >= 2 && values[0] is int points && values[1] is ItemModel item
                ? points + item.Points - item.Category.Points > Constants.MaxPoints ? TooManyPointsConverter._true : TooManyPointsConverter._false
                : DependencyProperty.UnsetValue;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}