using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfExplorer
{
    #region HeaderToImageConverter

    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is string)&&(value as string).EndsWith /*Contains*/(@"\"))
            {
                Uri uri = new Uri("pack://application:,,,/diskdrive.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else if (value is FileInfo)
            {
                Uri uri = new Uri("pack://application:,,,/file.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else

            {
                Uri uri = new Uri("pack://application:,,,/folder.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("Cannot convert back line 34 ");
            return null;
            //throw new NotSupportedException("Cannot convert back");
        }
    }

    #endregion // DoubleToIntegerConverter


}
