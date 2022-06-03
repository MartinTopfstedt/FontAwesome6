using System.Xml.Serialization;

namespace FontAwesome.Generator.Shared.Models.Svgs
{
    [XmlType("defs")]
    public class SvgDefs
    {
        [XmlElement]
        public string style { get; set; }
    }
}
