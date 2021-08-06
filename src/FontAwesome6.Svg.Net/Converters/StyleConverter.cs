using FontAwesome6.Extensions;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace FontAwesome6.Svg.Converters
{
  public class StyleConverter : MarkupExtension, IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is not EFontAwesomeIcon)
      {
        return null;
      }

      var icon = (EFontAwesomeIcon)value;
      var style = icon.GetStyle();
      if (style == EFontAwesomeStyle.None)
      {
        return null;
      }

      return parameter is string format && !string.IsNullOrEmpty(format) ? string.Format(format, style) : style.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }
  }
}
