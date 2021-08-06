using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;

using System.Windows.Media;


namespace FontAwesome6.Fonts
{
  /// <summary>
  /// Provides FontFamilies and TypeFaces of FontAwesome6.
  /// </summary>
  public static class FontAwesomeFonts
  {
    private static readonly Dictionary<EFontAwesomeStyle, FontFamily> _fontFamilies = new Dictionary<EFontAwesomeStyle, FontFamily>();
    private static readonly Dictionary<EFontAwesomeStyle, Tuple<string, string>> _fonts = new Dictionary<EFontAwesomeStyle, Tuple<string, string>>();

    private static readonly Dictionary<EFontAwesomeStyle, Typeface> _typeFaces = new Dictionary<EFontAwesomeStyle, Typeface>();

    static FontAwesomeFonts()
    {
      _fonts.Add(EFontAwesomeStyle.Brands, Tuple.Create("Font Awesome 6 Brands-Regular-400.otf", "Font Awesome 6 Brands Regular"));
#if FontAwesomePro
      _fonts.Add(EFontAwesomeStyle.Solid, Tuple.Create("Font Awesome 6 Pro-Solid-900.otf", "Font Awesome 6 Pro Solid"));
      _fonts.Add(EFontAwesomeStyle.Regular, Tuple.Create("Font Awesome 6 Pro-Regular-400.otf", "Font Awesome 6 Pro Regular"));
      _fonts.Add(EFontAwesomeStyle.Light, Tuple.Create("Font Awesome 6 Pro-Light-300.otf", "Font Awesome 6 Pro Light"));
      _fonts.Add(EFontAwesomeStyle.Duotone, Tuple.Create("Font Awesome 6 Duotone-Solid-900.otf", "Font Awesome 6 Duotone Solid"));
      _fonts.Add(EFontAwesomeStyle.Thin, Tuple.Create("Font Awesome 6 Pro-Thin-100.otf", "Font Awesome 6 Pro Thin"));
#else
      _fonts.Add(EFontAwesomeStyle.Solid, Tuple.Create("Font Awesome 6 Free-Solid-900.otf", "Font Awesome 6 Free Solid"));
      _fonts.Add(EFontAwesomeStyle.Regular, Tuple.Create("Font Awesome 6 Free-Regular-400.otf", "Font Awesome 6 Free Regular"));
      
      var path = Path.GetTempPath();
      SaveFontFilesToDirectory(path);
      LoadFontsFromDirectory(path);
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

    public static Typeface GetTypeFace(EFontAwesomeStyle style)
    {
      if (_typeFaces.TryGetValue(style, out var typeFace))
      {
        return typeFace;
      }

      return null;
    }

    /// <summary>
    /// Loads the FontFamilies and Typefaces from a specific uri.
    /// Load from resources: new Uri("pack://application:,,,/FontAwesome6.Net;component/Fonts/")
    /// Load from a directory: new Uri("file:///C:/Temp/", UriKind.Absolute)
    /// </summary>
    /// <param name="uri">Uri to the font files.</param>
    public static void LoadFonts(Uri uri)
    {
      _fontFamilies.Clear();
      _typeFaces.Clear();
      foreach (var kvp in _fonts)
      {
        var fontFamily = new FontFamily(uri, $"./#{kvp.Value.Item2}");
        _fontFamilies.Add(kvp.Key, fontFamily);
        _typeFaces.Add(kvp.Key, new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal));
      }
    }

    public static void LoadFontsFromDirectory(string absolutePath)
    {
      LoadFonts(new Uri($"file:///{absolutePath}", UriKind.Absolute));
    }

#if !FontAwesomePro
    private static void SaveFontFilesToDirectory(string path)
    {      
      var resManager = new ResourceManager("FontAwesome6.Fonts.Net.g", typeof(FontAwesomeFonts).Assembly);
      WriteResourceToFile(resManager, $"Fonts/Font Awesome 6 Free-Solid-900.otf", Path.Combine(path, "Font Awesome 6 Free-Solid-900.otf"));
      WriteResourceToFile(resManager, $"Fonts/Font Awesome 6 Free-Regular-400.otf", Path.Combine(path, "Font Awesome 6 Free-Regular-400.otf"));
      WriteResourceToFile(resManager, $"Fonts/Font Awesome 6 Brands-Regular-400.otf", Path.Combine(path, "Font Awesome 6 Brands-Regular-400.otf"));
    }

    private static void WriteResourceToFile(ResourceManager resManager, string resourceName, string fileName)
    {
      if (File.Exists(fileName))
      {
        return;
      }

      using (var res = resManager.GetStream(Uri.EscapeUriString(resourceName).ToLowerInvariant()))
      {
        using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
          res.CopyTo(file);
        }
      }
    }
#endif

  }
}
