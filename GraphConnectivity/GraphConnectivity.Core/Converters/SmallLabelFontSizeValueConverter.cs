using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace GraphConnectivity.Core.Converters
{
    public class SmallLabelFontSizeValueConverter:MvxValueConverter<double,double>
    {
        protected override double Convert(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return value / 3.5;
        }

        protected override double ConvertBack(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return value * 3.5;
        }
    }
}
