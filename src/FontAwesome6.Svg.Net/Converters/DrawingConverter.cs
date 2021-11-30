
/* Unmerged change from project 'FontAwesome6.Svg.Net (net462)'
Before:
using System;
After:
using FontAwesome6.Extensions;
using FontAwesome6.Svg.Extensions;

using System;
*/

/* Unmerged change from project 'FontAwesome6.Svg.Net (netcoreapp3.1)'
Before:
using System;
After:
using FontAwesome6.Extensions;
using FontAwesome6.Svg.Extensions;

using System;
*/

/* Unmerged change from project 'FontAwesome6.Svg.Net (net472)'
Before:
using System;
After:
using FontAwesome6.Extensions;
using FontAwesome6.Svg.Extensions;

using System;
*/

/* Unmerged change from project 'FontAwesome6.Svg.Net (net5.0-windows)'
Before:
using System;
After:
using FontAwesome6.Extensions;
using FontAwesome6.Svg.Extensions;

using System;
*/

/* Unmerged change from project 'FontAwesome6.Svg.Net (net6.0-windows)'
Before:
using System;
After:
using FontAwesome6.Extensions;
using FontAwesome6.Svg.Extensions;

using System;
*/
using FontAwesome6.Svg.Extensions;

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace FontAwesome6.Svg.Converters
{
    public class DrawingConverter : MarkupExtension, IValueConverter
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

            return ((EFontAwesomeIcon)value).CreateDrawing(brush);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
