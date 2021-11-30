using FontAwesome6.GraphQl;

using Scriban;
using Scriban.Runtime;

using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FontAwesome6.Generator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // parse parameter

            var root = args.Length > 0 ? args[0] : Path.Combine(Directory.GetCurrentDirectory(), "..", "..");

            //// generate

            var api = new FontAwesomeApi();
            var icons = await api.GetIconsAsync(Path.Combine(root, "Font-Awesome-Pro"), "6.0.0-beta1");

            var dirGenerated = Path.Combine(root, "generated");

            var src = Path.Combine(root, "src");

            var dirFontAwesome6 = Path.Combine(src, "FontAwesome6.Core");
            var dirFontAwesome6Fonts = Path.Combine(src, "FontAwesome6.Fonts.Shared");
            var dirFontAwesome6Svg = Path.Combine(src, "FontAwesome6.Svg.Shared");

            var freeStyles = icons.Values.SelectMany(i => i.free).Distinct();
            var allStyles = icons.Values.SelectMany(i => i.styles).Distinct();

            GenerateFile("EFontAwesomeStyle.scriban", Path.Combine(dirFontAwesome6, "EFontAwesomeStyle.cs"), new { FreeStyles = freeStyles, ProStyles = allStyles.Except(freeStyles) });

            var freeIcons = icons.Where(i => i.Value.free.Count > 0);
            var proIcons = icons.Where(i => i.Value.free.Count < i.Value.styles.Count);

            GenerateFile("EFontAwesomeIcon.scriban", Path.Combine(dirFontAwesome6, $"EFontAwesomeIcon.cs"), new { freeIcons, proIcons });

            GenerateFile("FontAwesomeUnicodes.scriban", Path.Combine(dirFontAwesome6Fonts, $"FontAwesomeUnicodes.cs"), new { freeIcons, proIcons });
        }

        private static void GenerateFile(string templateName, string targetFile, object model)
        {
            File.WriteAllText(targetFile, GenerateFromTemplate(templateName, model));
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
