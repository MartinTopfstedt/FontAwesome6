using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

using FontAwesome6.Extensions;
using FontAwesome6.Svg.Extensions;

namespace FontAwesome6.Svg.Converters
{
  /// <summary>
  /// Converts a FontAwesomIcon to an ImageSource. Use the ConverterParameter to pass the Brush.
  /// </summary>
  public class ImageSourceConverter : MarkupExtension, IValueConverter
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

      if (parameter is not Brush brush)
      {
        brush = Brushes.Black;
      }

      return ((EFontAwesomeIcon)value).CreateImageSource(brush);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
