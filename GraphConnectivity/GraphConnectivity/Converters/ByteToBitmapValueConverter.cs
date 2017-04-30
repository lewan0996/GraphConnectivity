using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using MvvmCross.Platform.Converters;

namespace GraphConnectivity.Converters
{
    public class ByteToBitmapValueConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(value as byte[]);
            bitmapImage.EndInit();

           /* Bitmap bmp;
            using (var ms = new MemoryStream(value as byte[]))
            {
                bmp = new Bitmap(ms);
            }*/

            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
