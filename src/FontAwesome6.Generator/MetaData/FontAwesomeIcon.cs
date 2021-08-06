using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontAwesome6.Generator.MetaData
{
  public class FontAwesomeIcon
  {
    public string label { get; set; }
    public string unicode { get; set; }
    public Aliases aliases { get; set; }

    public string unicodeString => FixUnicode(unicode);
    public string unicodeComposite => FixUnicode(aliases?.unicodes?.composite?.FirstOrDefault());
    public string unicodePrimary => FixUnicode(aliases?.unicodes?.primary?.FirstOrDefault());
    public string unicodeSecondary => FixUnicode(aliases?.unicodes?.secondary?.FirstOrDefault());
    public List<string> styles { get; set; }
    public Dictionary<string, Svg> svg { get; set; }
    public List<string> free { get; set; }

    public string duotoneOpacity
    {
      get
      {
        if (svg.TryGetValue("duotone", out var info))
        {
          return info.opacity;
        }
        return "1.0";
      }
    }

    private string FixUnicode(string str)
    {
      if (string.IsNullOrEmpty(str))
      {
        return null;
      }

      if (str.Length < 4)
      {
        for (int i = str.Length; i < 4; i++)
        {
          str = "0" + str;
        }

        return "\\u" + str;
      }
      else if (str.Length == 4)
      {
        return "\\u" + str;
      }

      for (int i = str.Length; i < 8; i++)
      {
        str = "0" + str;
      }

      return "\\U" + str;
    }
  }
}
