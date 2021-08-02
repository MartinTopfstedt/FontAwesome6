using FontAwesome6.Extensions;

using System.Collections.Generic;
using System.Linq;

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

      return FontAwesomeUnicodes.Data.TryGetValue(icon.GetIconName(), out var unicode) ? unicode : "";
    }

    /// <summary>
    /// Get the Unicode of an icon
    /// </summary>
    public static bool TryGetDuotoneUnicode(this EFontAwesomeIcon icon, out string primary, out string secondary)
    {
      primary = null;
      secondary = null;
      if (FontAwesomeUnicodes.Data.TryGetValue(icon.GetIconName(), out var unicode))
      {
        primary = unicode + "\ufe01";
        secondary = unicode + "\ufe02";

        return true;
      }

      return false;
    }
  }
}
