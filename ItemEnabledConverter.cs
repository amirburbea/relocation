using System;
using System.Globalization;
using System.Windows.Data;

namespace Relocation
{
    public sealed class ItemEnabledConverter : IMultiValueConverter
    {
        private static readonly object _false = false;
        private static readonly object _true = true;

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length >= 2 && values[0] is ItemModel item && values[1] is int points && points + item.Points - item.Category.Points <= 78
                ? ItemEnabledConverter._true
                : ItemEnabledConverter._false;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}