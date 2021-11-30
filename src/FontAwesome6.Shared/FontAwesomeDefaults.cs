
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#elif WINUI
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
#else
using System.Windows.Media;
#endif

namespace FontAwesome6
{
    public static class FontAwesomeDefaults
    {
        static FontAwesomeDefaults()
        {
#if WINDOWS_UWP || WINUI
            PrimaryColor = Application.Current.Resources.ThemeDictionaries["ApplicationForegroundThemeBrush"] as SolidColorBrush;
#else
            PrimaryColor = Brushes.Black;
#endif
            SecondaryColor = PrimaryColor;
        }
        public static Brush PrimaryColor { get; set; }
        public static Brush SecondaryColor { get; set; }
    }
}
