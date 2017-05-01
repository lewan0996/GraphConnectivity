using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace GraphConnectivity.Core.Converters
{
    public class NumberOfComponentsValueConverter:MvxValueConverter<int,string>
    {
        protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Składowe: " + value;
        }
    }
}
