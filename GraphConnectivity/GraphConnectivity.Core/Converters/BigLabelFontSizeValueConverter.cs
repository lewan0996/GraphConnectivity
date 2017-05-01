using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace GraphConnectivity.Core.Converters
{
    public class BigLabelFontSizeValueConverter:MvxValueConverter<double,double>
    {
        protected override double Convert(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return value / 2;
        }

        protected override double ConvertBack(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return value * 2;
        }
    }
}
