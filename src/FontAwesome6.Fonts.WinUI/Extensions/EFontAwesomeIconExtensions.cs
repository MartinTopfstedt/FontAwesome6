using FontAwesome6.Extensions;

using Microsoft.UI.Xaml.Media;

namespace FontAwesome6.Fonts.Extensions
{
    /// <summary>
    /// EFontAwesomeIcon extensions
    /// </summary>
    public static partial class EFontAwesomeIconExtensions
    {
        /// <summary>
        /// Get the FontFamily of an icon
        /// </summary>
        public static FontFamily GetFontFamily(this EFontAwesomeIcon icon)
        {
            return FontAwesomeFonts.GetFontFamily(icon.GetStyle());
        }
    }
}
