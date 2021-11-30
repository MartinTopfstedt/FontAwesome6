using System;

using Windows.UI.Xaml.Data;

namespace FontAwesome6.Example.UWP.Converters
{
    public class NullableDoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double d)
            {
                return d.ToString();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string str && double.TryParse(str, out var d))
            {
                return (double?)d;
            }

            return null;
        }
    }
}
