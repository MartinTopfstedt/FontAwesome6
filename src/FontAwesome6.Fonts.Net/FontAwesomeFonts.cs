using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

using System.Windows.Media;


namespace FontAwesome6.Fonts
{
    /// <summary>
    /// Provides FontFamilies and TypeFaces of FontAwesome6.
    /// </summary>
    public static class FontAwesomeFonts
    {
        private static readonly Dictionary<EFontAwesomeStyle, FontFamily> _fontFamilies = new Dictionary<EFontAwesomeStyle, FontFamily>();
        private static readonly Dictionary<EFontAwesomeStyle, string> _fontFamilyNames = new Dictionary<EFontAwesomeStyle, string>();

        static FontAwesomeFonts()
        {
            _fontFamilyNames.Add(EFontAwesomeStyle.Brands, "Font Awesome 6 Brands Regular");
#if FontAwesomePro
            _fontFamilyNames.Add(EFontAwesomeStyle.Solid, "Font Awesome 6 Pro Solid");
            _fontFamilyNames.Add(EFontAwesomeStyle.Regular, "Font Awesome 6 Pro Regular");
            _fontFamilyNames.Add(EFontAwesomeStyle.Light, "Font Awesome 6 Pro Light");
            _fontFamilyNames.Add(EFontAwesomeStyle.Duotone, "Font Awesome 6 Duotone Solid");
            _fontFamilyNames.Add(EFontAwesomeStyle.Thin, "Font Awesome 6 Pro Thin");
#else
            _fontFamilyNames.Add(EFontAwesomeStyle.Solid, "Font Awesome 6 Free Solid");
            _fontFamilyNames.Add(EFontAwesomeStyle.Regular, "Font Awesome 6 Free Regular");

            var path = Path.Combine(Path.GetTempPath(), FontAwesomeInfo.Version);
            SaveFontFilesToDirectory(path);
            LoadAllStyles(path);
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

        public static Typeface GetTypeface(EFontAwesomeStyle style)
        {
            if (_fontFamilies.TryGetValue(style, out var fontFamily))
            {
                return fontFamily.GetTypefaces().FirstOrDefault();
            }

            throw new Exception($"Couldn't find Typeface for {style}. Please load the font file for {style}.");
        }

        /// <summary>
        /// Loads the FontFamily and Typeface for the specific EFontAwesomeStyles.
        /// </summary>
        /// <param name="uri">Uri to the location of the font file.
        /// Load from resources: new Uri("pack://application:,,,/FontAwesome6.Net;component/Fonts/")
        /// Load from a directory: new Uri("file:///C:/Temp/", UriKind.Absolute)
        /// </param>
        /// <param name="styles">The EFontAwesomeStyles which should be loaded.</param>
        public static void LoadStyles(Uri uri, params EFontAwesomeStyle[] styles)
        {
            foreach (var style in styles)
            {
                _fontFamilies[style] = new FontFamily(uri, $"./#{_fontFamilyNames[style]}");
            }
        }

        /// <summary>
        /// Loads the FontFamily and Typeface for the specific EFontAwesomeStyles.
        /// </summary>
        /// <param name="absolutePath">Path to the location of the font file.</param>
        /// <param name="styles">The EFontAwesomeStyle which should be loaded.</param>
        public static void LoadStyles(string absolutePath, params EFontAwesomeStyle[] styles)
        {
            LoadStyles(new Uri($"file:///{absolutePath}", UriKind.Absolute), styles);
        }

        /// <summary>
        /// Loads all FontFamilies and Typefaces for all EFontAwesomeStyles.    
        /// </summary>
        /// <param name="uri">Uri to the location of all font files.
        /// Load from resources: new Uri("pack://application:,,,/FontAwesome6.Net;component/Fonts/")
        /// Load from a directory: new Uri("file:///C:/Temp/", UriKind.Absolute)
        /// </param>
        public static void LoadAllStyles(Uri uri)
        {
            LoadStyles(uri, _fontFamilyNames.Keys.ToArray());
        }

        /// <summary>
        /// Loads all FontFamilies and Typefaces.    
        /// </summary>
        /// <param name="absolutePath">Path to the location of all font files.</param>
        public static void LoadAllStyles(string absolutePath)
        {
            LoadAllStyles(new Uri($"file:///{absolutePath}", UriKind.Absolute));
        }

#if !FontAwesomePro
        private static void SaveFontFilesToDirectory(string path)
        {
            var resManager = new ResourceManager("FontAwesome6.Fonts.Net.g", typeof(FontAwesomeFonts).Assembly);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            WriteResourceToFile(resManager, $"Fonts/fa-solid-900.ttf", Path.Combine(path, "fa-solid-900.ttf"));
            WriteResourceToFile(resManager, $"Fonts/fa-regular-400.ttf", Path.Combine(path, "fa-regular-400.ttf"));
            WriteResourceToFile(resManager, $"Fonts/fa-brands-400.ttf", Path.Combine(path, "fa-brands-400.ttf"));
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
