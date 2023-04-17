using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;

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
            _fonts.Add(EFontAwesomeStyle.Brands, Tuple.Create("fa-brands-400.ttf", "Font Awesome 6 Brands Regular"));
#if FontAwesomePro
            _fonts.Add(EFontAwesomeStyle.Solid, Tuple.Create("fa-solid-900.ttf", "Font Awesome 6 Pro Solid"));
            _fonts.Add(EFontAwesomeStyle.Regular, Tuple.Create("fa-regular-400.ttf", "Font Awesome 6 Pro Regular"));
            _fonts.Add(EFontAwesomeStyle.Light, Tuple.Create("fa-light-300.ttf", "Font Awesome 6 Pro Light"));
            _fonts.Add(EFontAwesomeStyle.Thin, Tuple.Create("fa-thin-100.ttf", "Font Awesome 6 Pro Thin"));
            _fonts.Add(EFontAwesomeStyle.Duotone, Tuple.Create("fa-duotone-900.ttf", "Font Awesome 6 Duotone Solid"));
#else
            _fonts.Add(EFontAwesomeStyle.Solid, Tuple.Create("fa-solid-900.ttf", "Font Awesome 6 Free Solid"));
            _fonts.Add(EFontAwesomeStyle.Regular, Tuple.Create("fa-regular-400.ttf", "Font Awesome 6 Free Regular"));

            try
            {
                var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
                SaveFontFilesToDirectory(Path.Combine(path, "Fonts"));
                LoadAllStyles("ms-appdata:///local/Fonts/");
            }
            catch
            {
                var path = Directory.GetCurrentDirectory();
                SaveFontFilesToDirectory(Path.Combine(path, "Fonts"));
                LoadAllStyles("ms-appx:///Fonts/");
            }
#endif
        }

        public static FontFamily GetFontFamily(EFontAwesomeStyle style)
        {
            if (_fontFamilies.TryGetValue(style, out var fontFamily))
            {
                return fontFamily;
            }

            throw new Exception($"Couldn't find FontFamily for {style}. Please load the font file for {style}.");
        }

        /// <summary>
        /// Loads the FontFamily and Typeface for the specific EFontAwesomeStyles.
        /// </summary>
        /// <param name="uri">Uri to the location of the font file.
        /// Load from resources: new Uri("ms-appx:///FontAwesome6.UWP/Fonts/")
        /// Load from a directory: new Uri("file:///C:/Temp/", UriKind.Absolute)
        /// </param>
        /// <param name="styles">The EFontAwesomeStyles which should be loaded.</param>
        public static void LoadStyles(Uri uri, params EFontAwesomeStyle[] styles)
        {
            foreach (var style in styles)
            {
                _fontFamilies[style] = new FontFamily($"{uri.AbsoluteUri}{_fonts[style].Item1}#{_fonts[style].Item2}");
            }
        }


        /// <summary>
        /// Loads the FontFamily and Typeface for the specific EFontAwesomeStyles.
        /// </summary>
        /// <param name="uri">Uri to the location of the font file.
        /// Load from resources: new Uri("ms-appx:///FontAwesome6.UWP/Fonts/")
        /// Load from a directory: new Uri("file:///C:/Temp/", UriKind.Absolute)
        /// </param>
        /// <param name="styles">The EFontAwesomeStyles which should be loaded.</param>
        public static void LoadStyles(string uri, params EFontAwesomeStyle[] styles)
        {
            LoadStyles(new Uri(uri), styles);
        }

        /// <summary>
        /// Loads all FontFamilies and Typefaces for all EFontAwesomeStyles.    
        /// </summary>
        /// <param name="uri">Uri to the location of all font files.
        /// Load from resources: new Uri("ms-appx:///FontAwesome6.UWP/Fonts/")
        /// Load from a directory: new Uri("file:///C:/Temp/", UriKind.Absolute)
        /// </param>
        public static void LoadAllStyles(Uri uri)
        {
            LoadStyles(uri, _fonts.Keys.ToArray());
        }

        /// <summary>
        /// Loads all FontFamilies and Typefaces.    
        /// </summary>
        /// <param name="absolutePath">Path to the location of all font files.</param>
        public static void LoadAllStyles(string uri)
        {
            LoadAllStyles(new Uri(uri));
        }

#if !FontAwesomePro
        private static void SaveFontFilesToDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var assembly = typeof(FontAwesomeFonts).Assembly;
            WriteResourceToFile(assembly, $"FontAwesome6.Fonts.WinUI.Fonts.fa-solid-900.ttf", Path.Combine(path, "fa-solid-900.ttf"));
            WriteResourceToFile(assembly, $"FontAwesome6.Fonts.WinUI.Fonts.fa-regular-400.ttf", Path.Combine(path, "fa-regular-400.ttf"));
            WriteResourceToFile(assembly, $"FontAwesome6.Fonts.WinUI.Fonts.fa-brands-400.ttf", Path.Combine(path, "fa-brands-400.ttf"));
        }

        private static void WriteResourceToFile(Assembly assembly, string resourceName, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using var res = assembly.GetManifestResourceStream(resourceName);
            using var file = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            res.CopyTo(file);
        }
#endif
    }
}
