using FontAwesome.Generator.Shared.GraphQl;
using FontAwesome.Generator.Shared.Models;
using FontAwesome.Generator.Shared.Models.Svgs;

using Newtonsoft.Json;

using Scriban;
using Scriban.Runtime;

using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace FontAwesome.Generator.Shared
{
    public class FontAwesomeGenerator
    {
        private static Regex _reOpacity = new Regex(@"\.fa-secondary\{opacity:(.*?)\}");

        public async Task GenerateAllAsync(string version, bool isFree, string sourceDirectory, string svgDirectory, string outputDirectory, string outputFileNamePrefix)
        {
            var icons = await GetIconsAsync(version, isFree, svgDirectory);
            GenerateFiles(sourceDirectory, icons);
            GenerateSvg(outputDirectory, isFree, icons, outputFileNamePrefix);
        }

        public async Task<Dictionary<string, FontAwesomeIcon>> GetIconsAsync(string version, bool isFree,  string svgDirectory)
        {
            var api = new FontAwesomeApi();

            if (!await api.DoesVersionExistAsync(version))
            {
                throw new Exception($"Version {version} does not exist.");
            }

            var icons = await api.GetIconsAsync(version, isFree);

            var result = new Dictionary<string, FontAwesomeIcon>();
            foreach (var icon in icons)
            {
                var faIcon = new FontAwesomeIcon
                {
                    unicode = icon.Unicode,
                    label = icon.Label,
                    styles = icon.Styles,
                    free = icon.Membership.Free,
                    svg = new Dictionary<string, FontAwesomeSvgIcon>()
                };

                if (svgDirectory != null)
                {
                    var availableStyles = isFree ? icon.Membership.Free : icon.Membership.Pro;
                    foreach (var style in availableStyles)
                    {
                        var svgFile = Path.Combine(svgDirectory, style, icon.Id + ".svg");
                        if (!File.Exists(svgFile))
                        {
                            throw new FileNotFoundException(svgFile);
                        }

                        var serializer = new XmlSerializer(typeof(SvgFile));

                        using (var reader = XmlReader.Create(svgFile))
                        {
                            var svg = (SvgFile)serializer.Deserialize(reader);
                            faIcon.svg.Add(style, new FontAwesomeSvgIcon
                            {
                                height = svg.height,
                                width = svg.width,
                                opacity = ExtractOpacityFromStyle(svg.defs?.style),
                                paths = svg.paths.Select(p => p.d).ToArray()
                            });
                        }

                    }
                }
                result.Add(icon.Id, faIcon);
            }

            return result;
        }


        private static void GenerateFiles(string srcDirectory, Dictionary<string, FontAwesomeIcon> icons)
        {
            if (!Directory.Exists(srcDirectory))
            {
                throw new DirectoryNotFoundException(srcDirectory);
            }

            var dirFontAwesome6 = Path.Combine(srcDirectory, "FontAwesome6.Core");
            var dirFontAwesome6Fonts = Path.Combine(srcDirectory, "FontAwesome6.Fonts.Shared");

            var fileEFontAwesomeStyle = Path.Combine(dirFontAwesome6, "EFontAwesomeStyle.cs");
            var fileEFontAwesomeIcon = Path.Combine(dirFontAwesome6, "EFontAwesomeIcon.cs");
            var fileFontAwesomeUnicodes = Path.Combine(dirFontAwesome6Fonts, "FontAwesomeUnicodes.cs");

            if (!Directory.Exists(dirFontAwesome6))
            {
                throw new DirectoryNotFoundException(dirFontAwesome6);
            }

            if (!Directory.Exists(dirFontAwesome6Fonts))
            {
                throw new DirectoryNotFoundException(dirFontAwesome6Fonts);
            }

            if (!File.Exists(fileEFontAwesomeStyle))
            {
                throw new FileNotFoundException(fileEFontAwesomeStyle);
            }

            if (!File.Exists(fileEFontAwesomeIcon))
            {
                throw new FileNotFoundException(fileEFontAwesomeIcon);
            }

            if (!File.Exists(fileFontAwesomeUnicodes))
            {
                throw new FileNotFoundException(fileFontAwesomeUnicodes);
            }

            var freeStyles = icons.Values.SelectMany(i => i.free).Distinct();
            var allStyles = icons.Values.SelectMany(i => i.styles).Distinct();

            GenerateFile("EFontAwesomeStyle.scriban", Path.Combine(dirFontAwesome6, "EFontAwesomeStyle.cs"), new { FreeStyles = freeStyles, ProStyles = allStyles.Except(freeStyles) });

            var freeIcons = icons.Where(i => i.Value.free.Count > 0);
            var proIcons = icons.Where(i => i.Value.free.Count < i.Value.styles.Count);

            GenerateFile("EFontAwesomeIcon.scriban", Path.Combine(dirFontAwesome6, $"EFontAwesomeIcon.cs"), new { freeIcons, proIcons });

            GenerateFile("FontAwesomeUnicodes.scriban", Path.Combine(dirFontAwesome6Fonts, $"FontAwesomeUnicodes.cs"), new { freeIcons, proIcons });
        }

        private static void GenerateSvg(string outputDirectory, bool isFree, Dictionary<string, FontAwesomeIcon> icons, string outputFileNamePrefix)
        {
            if (!Directory.Exists(outputDirectory))
            {
                throw new DirectoryNotFoundException(outputDirectory);
            }

            var iconsPerStyle = new Dictionary<string, Dictionary<string, FontAwesomeSvgIcon>>();
            foreach (var kvp in icons)
            {
                var availableStyles = isFree ? kvp.Value.free : kvp.Value.styles;
                foreach (var style in availableStyles)
                {
                    if (!iconsPerStyle.TryGetValue(style, out var lstIcons))
                    {
                        lstIcons = new Dictionary<string, FontAwesomeSvgIcon>();
                        iconsPerStyle.Add(style, lstIcons);
                    }

                    lstIcons.Add(GenerateKey(style, kvp.Key), kvp.Value.svg[style]);
                }
            }

            foreach (var kvp in iconsPerStyle)
            {
                var file = Path.Combine(outputDirectory, $"{outputFileNamePrefix}.{kvp.Key}.json");
                Console.WriteLine($"Generating {kvp.Key} svg file.(\"{file}\")");
                File.WriteAllText(file, JsonConvert.SerializeObject(kvp.Value, Newtonsoft.Json.Formatting.None));
            }

            var allFile = Path.Combine(outputDirectory, $"{outputFileNamePrefix}.all.json");
            Console.WriteLine($"Generating all svg file.(\"{allFile}\")");

            var allIcons = iconsPerStyle.Values.SelectMany(v => v).ToDictionary(k => k.Key, k => k.Value);
            File.WriteAllText(allFile, JsonConvert.SerializeObject(allIcons, Newtonsoft.Json.Formatting.None));
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

        private static string ExtractOpacityFromStyle(string style)
        {
            if (string.IsNullOrEmpty(style))
            {
                return "1.0";
            }

            var match = _reOpacity.Match(style);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return "1.0";
        }

        private static string GenerateKey(string style, string name)
        {
            if (char.IsLower(style[0]))
            {
                style = char.ToUpperInvariant(style[0]) + style.Substring(1);
            }

            name = new Regex("[\\-&\\.']").Replace(name, " ");
            name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name);
            name = name.Replace(" ", "");

            return $"{style}_{name}";
        }
    }
}
