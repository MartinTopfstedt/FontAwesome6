using FontAwesome6.Generator.MetaData;

using Newtonsoft.Json;

using Scriban;
using Scriban.Runtime;

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FontAwesome6.Generator
{
  class Program
  {
    static void Main(string[] args)
    {
      var icons = ReadMetadata(args[0]);

      var root = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "..", "..");

      var dirGenerated = Path.Combine(root, "generated");

      var src = Path.Combine(root, "src");

      var dirFontAwesome6 = Path.Combine(src, "FontAwesome6", "FontAwesome6");
      var dirFontAwesome6Fonts = Path.Combine(src, "FontAwesome6.Fonts.Shared");
      var dirFontAwesome6Svg = Path.Combine(src, "FontAwesome6.Svg.Shared");

      var freeStyles = icons.Values.SelectMany(i => i.free).Distinct();
      var allStyles = icons.Values.SelectMany(i => i.styles).Distinct();

      GenerateFile("EFontAwesomeStyle.scriban", Path.Combine(dirFontAwesome6, "EFontAwesomeStyle.cs"), new { FreeStyles = freeStyles, ProStyles = allStyles.Except(freeStyles) });

      var freeIcons = icons.Where(i => i.Value.free.Count > 0);
      var proIcons = icons.Where(i => i.Value.free.Count < i.Value.styles.Count);

      GenerateFile("EFontAwesomeIcon.scriban", Path.Combine(dirFontAwesome6, $"EFontAwesomeIcon.cs"), new { freeIcons, proIcons });      
      
      // Fonts
      GenerateFile("FontAwesomeUnicodes.scriban", Path.Combine(dirFontAwesome6Fonts, $"FontAwesomeUnicodes.cs"), new { freeIcons, proIcons });

      // SVG
      GenerateJson("FontAwesomeSvgJson.scriban", Path.Combine(dirGenerated, $"FontAwesomeSvg.json"), new { icons = freeIcons, isFree = true });
      GenerateJson("FontAwesomeSvgJson.scriban", Path.Combine(dirGenerated, $"FontAwesomeSvgPro.json"), new { icons = proIcons, isFree = false });      
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
