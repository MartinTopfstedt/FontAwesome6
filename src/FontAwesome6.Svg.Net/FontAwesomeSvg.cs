using FontAwesome6.Svg.Shared;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace FontAwesome6.Svg
{
    public static class FontAwesomeSvg
    {
        private static Dictionary<string, FontAwesomeSvgInformation> _data = new Dictionary<string, FontAwesomeSvgInformation>();

        static FontAwesomeSvg()
        {
#if !FontAwesomePro
            LoadFromResource("FontAwesome6.Svg.Data.FontAwesomeSvg.all.json", typeof(FontAwesomeSvg).Assembly);
#endif
        }

        public static bool TryGetInformation(EFontAwesomeIcon icon, out FontAwesomeSvgInformation information)
        {
            return _data.TryGetValue(icon.ToString(), out information);
        }

        public static FontAwesomeSvgInformation GetInformation(EFontAwesomeIcon icon)
        {
            if (_data.TryGetValue(icon.ToString(), out var information))
            {
                return information;
            }

            if (!(bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                throw new Exception($"Couldn't load icon \"{icon}\". Please load the svg data for that icon first.");
            }

            return null;
        }

        public static void Clear()
        {
            _data.Clear();
        }

        public static void LoadFromResource(string resourceName, Assembly assembly)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    ReadSvgData(content);
                }
            }
        }

        public static void LoadFromDirectory(string fullFileName)
        {
            if (!File.Exists(fullFileName))
            {
                throw new System.Exception("File not found.");
            }

            ReadSvgData(File.ReadAllText(fullFileName));
        }

        private static void ReadSvgData(string content)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, FontAwesomeSvgInformation>>(content);
            foreach (var kvp in data)
            {
                _data[kvp.Key] = kvp.Value;
            }
        }
    }
}
