using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace FontAwesome.Generator.Shared.Models.Svgs
{
    [XmlRoot("svg", Namespace = "http://www.w3.org/2000/svg")]
    public class SvgFile
    {
        [XmlAttribute]
        public string viewBox { get; set; }

        public SvgDefs defs { get; set; }

        [XmlElement("path")]
        public SvgPath[] paths { get; set; }

        public int height
        {
            get
            {
                var re = new Regex(@"([0-9]+) ([0-9]+) ([0-9]+) ([0-9]+)");
                var match = re.Match(viewBox);
                if (match.Success)
                {
                    return int.Parse(match.Groups[4].Value);
                }

                return 0;
            }
        }

        public int width
        {
            get
            {
                var re = new Regex(@"([0-9]+) ([0-9]+) ([0-9]+) ([0-9]+)");
                var match = re.Match(viewBox);
                if (match.Success)
                {
                    return int.Parse(match.Groups[3].Value);
                }

                return 0;
            }
        }
    }
}
