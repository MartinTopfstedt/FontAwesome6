using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontAwesome6.Generator.MetaData
{
  public class SvgJsonConverter : JsonConverter<Svg>
  {
    public override Svg ReadJson(JsonReader reader, Type objectType, Svg existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var jo = JObject.Load(reader);

      Svg svg = null;

      if (jo["path"].Type == JTokenType.String)
      {
        var path = new string[] { jo["path"].Value<string>() };
        jo.Remove("path");
        svg = jo.ToObject<Svg>();
        svg.path = path;
      }
      else if (jo["path"].Type == JTokenType.Array)
      {
        svg = jo.ToObject<Svg>();
      }

      return svg;
    }

    public override void WriteJson(JsonWriter writer, Svg value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}
