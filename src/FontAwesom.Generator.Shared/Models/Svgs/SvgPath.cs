using System.Xml.Serialization;

namespace FontAwesome.Generator.Shared.Models.Svgs
{
    [XmlType("path")]
    public class SvgPath
    {
        [XmlAttribute]
        public string @class { get; set; }

        [XmlAttribute]
        public string d { get; set; }
    }
}
