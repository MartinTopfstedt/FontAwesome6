using FontAwesome6.Generator.MetaData;

using Newtonsoft.Json;

using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace FontAwesome6.Generator
{
  class Program
  {
    static void Main(string[] args)
    {
      var icons = ReadMetadata(args[0]);

      var freeStyles = icons.Values.SelectMany(i => i.free).Distinct();
      var allStyles = icons.Values.SelectMany(i => i.styles).Distinct();

      GenerateFile("EFontAwesomeStyle.scriban", Path.Combine(args[1], "EFontAwesomeStyle.cs"), new { FreeStyles = freeStyles, ProStyles = allStyles.Except(freeStyles) });

      var freeIcons = icons.Where(i => i.Value.free.Count > 0);
      var proIcons = icons.Where(i => i.Value.free.Count < i.Value.styles.Count);

      GenerateFile("EFontAwesomeIcon.scriban", Path.Combine(args[1], $"EFontAwesomeIcon.cs"), new { freeIcons, proIcons });      
      GenerateFile("FontAwesomeLabels.scriban", Path.Combine(args[1], $"FontAwesomeLabels.cs"), new { freeIcons, proIcons });
      //GenerateFile("FontAwesomeStyles.scriban", Path.Combine(args[1], $"FontAwesomeStyles.cs"), new { freeIcons, proIcons });

      // Fonts
      GenerateFile("FontAwesomeUnicodes.scriban", Path.Combine(args[2], $"FontAwesomeUnicodes.cs"), new { freeIcons, proIcons });

      // SVG
      GenerateJson("FontAwesomeSvgJson.scriban", Path.Combine(args[3], $"FontAwesomeSvg.json"), new { icons = freeIcons, isFree = true });
      GenerateJson("FontAwesomeSvgJson.scriban", Path.Combine(args[4], $"FontAwesomeSvgPro.json"), new { icons = proIcons, isFree = false });      
    }

    static Dictionary<string, FontAwesomeIcon> ReadMetadata(string metaDataFile)
    {
      return JsonConvert.DeserializeObject<Dictionary<string, FontAwesomeIcon>>(File.ReadAllText(metaDataFile), new JsonSerializerSettings()
      {
        Converters = { new SvgJsonConverter(), new AliasesJsonConverter() }
      });
    }

    private static void GenerateFile(string templateName, string targetFile, object model)
    {
      File.WriteAllText(targetFile, GenerateFromTemplate(templateName, model));
    }

    private static void GenerateJson(string templateName, string targetFile, object model)
    {
      var json = GenerateFromTemplate(templateName, model);

      var loaded = JsonConvert.DeserializeObject(json);

      File.WriteAllText(targetFile, JsonConvert.SerializeObject(loaded, Formatting.None));
    }

    private static string GenerateFromTemplate(string templateName, object model)
    {
      var templateFile = Path.Combine(Directory.GetCurrentDirectory(), "Templates", templateName);
      var templateContent = File.ReadAllText(templateFile);

      var context = new TemplateContext
      {
        MemberRenamer = m => m.Name,
        LoopLimit = 0
      };
      var scriptObject = new ScriptObject();
      scriptObject.Import(model, renamer: m => m.Name);
      context.PushGlobal(scriptObject);

      var template = Template.Parse(templateContent, templateFile);
      return template.Render(context);
    }
  }
}
