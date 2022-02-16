using FontAwesome6.Extensions;

namespace FontAwesome6.Fonts.Extensions
{
    /// <summary>
    /// EFontAwesomeIcon extensions
    /// </summary>
    public static partial class EFontAwesomeIconExtensions
    {
        /// <summary>
        /// Get the Unicode of an icon
        /// </summary>
        public static string GetUnicode(this EFontAwesomeIcon icon)
        {
            if (icon == EFontAwesomeIcon.None)
            {
                return null;
            }

            return FontAwesomeUnicodes.Data.TryGetValue(icon.GetIconName(), out var info) ? info.Item1 : "";
        }
    }
}
