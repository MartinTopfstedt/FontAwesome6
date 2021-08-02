using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace FontAwesome6.Example.WPF.Converters
{
  [ValueConversion(typeof(double?), typeof(string))]
  public class NullableDoubleToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is double d)
      {
        return d.ToString();
      }

      return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is string str && double.TryParse(str, out var d))
      {
        return d;
      }


      return null;
    }
  }
}
