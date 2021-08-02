using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontAwesome6.Generator.MetaData
{
  public class AliasesJsonConverter : JsonConverter<Aliases>
  {
    public override Aliases ReadJson(JsonReader reader, Type objectType, Aliases existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      var jo = JObject.Load(reader);

      Aliases aliases = null;

      if (!jo.ContainsKey("unicodes"))
      {
        return null;
      }

      if (jo["unicodes"].Type == JTokenType.Array)
      {
        var values = jo["unicodes"].Values<string>();
        var codes = new List<string>(values);
        aliases = new Aliases();
        aliases.unicodes = new Unicodes() { composite = codes, primary  = codes };
      }
      else if (jo["unicodes"].Type == JTokenType.Object)
      {
        aliases = jo.ToObject<Aliases>();
      }

      return aliases;
    }

    public override void WriteJson(JsonWriter writer, Aliases value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}
