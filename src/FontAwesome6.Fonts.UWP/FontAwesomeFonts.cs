using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;

namespace FontAwesome6.Fonts
{
  /// <summary>
  /// Provides FontFamilies and TypeFaces of FontAwesome6.
  /// </summary>
  public static class FontAwesomeFonts
  {
    private static readonly Dictionary<EFontAwesomeStyle, FontFamily> _fontFamilies = new Dictionary<EFontAwesomeStyle, FontFamily>();
    private static readonly Dictionary<EFontAwesomeStyle, Tuple<string, string>> _fonts = new Dictionary<EFontAwesomeStyle, Tuple<string, string>>();

    static FontAwesomeFonts()
    {
      _fonts.Add(EFontAwesomeStyle.Brands, Tuple.Create("Font Awesome 6 Brands-Regular-400.otf", "Font Awesome 6 Brands"));
#if FontAwesomePro
      _fonts.Add(EFontAwesomeStyle.Solid, Tuple.Create("Font Awesome 6 Pro-Solid-900.otf", "Font Awesome 6 Pro Solid"));
      _fonts.Add(EFontAwesomeStyle.Regular, Tuple.Create("Font Awesome 6 Pro-Regular-400.otf", "Font Awesome 6 Pro Regular"));
      _fonts.Add(EFontAwesomeStyle.Light, Tuple.Create("Font Awesome 6 Pro-Light-300.otf", "Font Awesome 6 Pro Light"));
      _fonts.Add(EFontAwesomeStyle.Duotone, Tuple.Create("Font Awesome 6 Duotone-Solid-900.otf", "Font Awesome 6 Duotone Solid"));
#else
      _fonts.Add(EFontAwesomeStyle.Solid, Tuple.Create("Font Awesome 6 Free-Solid-900.otf", "Font Awesome 6 Free"));
      _fonts.Add(EFontAwesomeStyle.Regular, Tuple.Create("Font Awesome 6 Free-Regular-400.otf", "Font Awesome 6 Free"));

      LoadFonts("ms-appx:///FontAwesome6.Fonts.UWP/Fonts/");
#endif
    }

    public static FontFamily GetFontFamily(EFontAwesomeStyle style)
    {
      if (_fontFamilies.TryGetValue(style, out var fontFamily))
      {
        return fontFamily;
      }

      return null;
    }

    /// <summary>
    /// Loads the FontFamilies and Typefaces from a specific path.
    /// Load from resources: "ms-appx:///FontAwesome6.Fonts.UWP/Fonts/"
    /// </summary>
    /// <param name="basePath">Path to the font files.</param>
    public static void LoadFonts(string basePath)
    {
      _fontFamilies.Clear();
      foreach (var kvp in _fonts)
      {
        var fontFamily = new FontFamily($"{basePath}/{kvp.Value.Item1}#{kvp.Value.Item2}");
        _fontFamilies.Add(kvp.Key, fontFamily);
      }
    }
  }
}
