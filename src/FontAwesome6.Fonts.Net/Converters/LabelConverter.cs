using FontAwesome6.Extensions;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace FontAwesome6.Fonts.Converters
{
  public class LabelConverter : MarkupExtension, IValueConverter
  {
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is not EFontAwesomeIcon)
      {
        return null;
      }

      var icon = (EFontAwesomeIcon)value;
      var label = icon.GetLabel();
      if (label == null)
      {
        return null;
      }

      return parameter is string format && !string.IsNullOrEmpty(format) ? string.Format(format, label) : label;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
