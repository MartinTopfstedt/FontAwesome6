using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace FontAwesome6.Example.WPF.Converters
{
  [ValueConversion(typeof(SolidColorBrush), typeof(string))]
  public class HexColorStringToSolidColorBrushConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is SolidColorBrush brush)
      {
        return brush.Color.ToString();
      }


      return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is string str)
      {
        try
        {
          var converter = new System.Windows.Media.BrushConverter();
          return (SolidColorBrush)converter.ConvertFromString(str);
        }
        catch { }

      }

      return null;
    }
  }
}
