﻿using System.Text.RegularExpressions;

namespace FontAwesome6.Generator.MetaData
{
    public class Svg
    {
        public string raw { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string[] path { get; set; }

        public string opacity
        {
            get
            {
                var re = new Regex(@"\.fa-secondary\{opacity:(.*?)\}");
                var match = re.Match(raw);
                var opacity = "1.0";
                if (match.Success)
                {
                    opacity = match.Groups[1].Value;
                }

                return opacity;
            }
        }
    }
}
