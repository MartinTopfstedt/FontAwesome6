using System.Xml.Serialization;

namespace FontAwesome6.Generator.Svgs
{
    [XmlType("defs")]
    public class SvgDefs
    {
        [XmlElement]
        public string style { get; set; }
    }
}
