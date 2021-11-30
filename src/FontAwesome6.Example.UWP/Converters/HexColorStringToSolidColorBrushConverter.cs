using System;

using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FontAwesome6.Example.UWP.Converters
{
    public class HexColorStringToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is SolidColorBrush brush)
            {
                return brush.Color.ToString();
            }


            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string colorStr)
            {
                try
                {
                    //Target hex string
                    colorStr = colorStr.Replace("#", string.Empty);
                    // from #RRGGBB string
                    var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
                    var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
                    var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);
                    //get the color
                    Color color = Color.FromArgb(255, r, g, b);
                    // create the solidColorbrush
                    return new SolidColorBrush(color);
                }
                catch { }

            }

            return null;
        }
    }
}
