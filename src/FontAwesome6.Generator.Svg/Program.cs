using FontAwesome6.GraphQl;

using McMaster.Extensions.CommandLineUtils;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FontAwesome6.Generator.Svg
{
    class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Required]
        [DirectoryExists]
        [Argument(0, Description = "The directory of fontawesome.(ex. \".\\Font-Awesome\")")]
        public string FontAwesomeDirectory { get; } = Directory.GetCurrentDirectory();

        [DirectoryExists]
        [Argument(1, Description = "The output directory of the generated fontawesome svg information.(ex. \".\\generated\")")]
        public string OuputDirectory { get; } = Directory.GetCurrentDirectory();

        [Argument(2, Description = "The prefix of the generated files.(ex. \"FontAwesomeSvg\")")]
        public string OutputFileNamePrefix { get; } = "FontAwesomeSvg";

        private async Task OnExecute()
        {
            try
            {
                Console.WriteLine($"Generating SVG information from \"{FontAwesomeDirectory}\" into \"{OuputDirectory}\"");

                var api = new FontAwesomeApi();
                var icons = await api.GetIconsAsync(FontAwesomeDirectory, "6.0.0-beta1");

                var iconsPerStyle = new Dictionary<string, Dictionary<string, SvgIcon>>();
                foreach (var kvp in icons)
                {
                    foreach (var style in kvp.Value.styles)
                    {
                        if (!iconsPerStyle.TryGetValue(style, out var lstIcons))
                        {
                            lstIcons = new Dictionary<string, SvgIcon>();
                            iconsPerStyle.Add(style, lstIcons);
                        }

                        lstIcons.Add(GenerateKey(style, kvp.Key), new SvgIcon
                        {
                            width = kvp.Value.svg[style].width,
                            height = kvp.Value.svg[style].height,
                            opacity = kvp.Value.svg[style].opacity,
                            paths = kvp.Value.svg[style].path
                        });
                    }
                }

                foreach (var kvp in iconsPerStyle)
                {
                    var file = Path.Combine(OuputDirectory, $"{OutputFileNamePrefix}.{kvp.Key}.json");
                    Console.WriteLine($"Generating {kvp.Key} svg file.(\"{file}\")");
                    File.WriteAllText(file, JsonConvert.SerializeObject(kvp.Value, Formatting.None));
                }

                var allFile = Path.Combine(OuputDirectory, $"{OutputFileNamePrefix}.all.json");
                Console.WriteLine($"Generating all svg file.(\"{allFile}\")");

                var allIcons = iconsPerStyle.Values.SelectMany(v => v).ToDictionary(k => k.Key, k => k.Value);
                File.WriteAllText(allFile, JsonConvert.SerializeObject(allIcons, Formatting.None));

                Console.WriteLine($"Generation finished.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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
