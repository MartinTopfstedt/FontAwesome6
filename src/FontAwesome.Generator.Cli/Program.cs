using FontAwesome.Generator.Shared;

using McMaster.Extensions.CommandLineUtils;

using System.ComponentModel.DataAnnotations;

namespace FontAwesome.Generator.Cli
{
    class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Required]
        [Argument(0, Description = "FontAwesome version. (ex. 6.0.0-beta1)")]
        public string Version { get; }
           
        [Required]
        [DirectoryExists]
        [Argument(1, Description = "The source code directory.(ex. \".\\src\")")]
        public string SourceDirectory { get; }

        [Required]
        [DirectoryExists]
        [Argument(2, Description = "The directory of fontawesome svgs.(ex. \".\\Font-Awesome\\svgs\")")]
        public string FontAwesomeSvgDirectory { get; }

        [DirectoryExists]
        [Argument(3, Description = "The output directory for the generated fontawesome svg information.(ex. \".\\generated\")")]
        public string SvgOutputDirectory { get; }

        [Argument(4, Description = "The prefix of the generated svg files.(ex. \"FontAwesomeSvg\")")]
        public string SvgOutputFileNamePrefix { get; }

        [Argument(6, Description = "Is FontAwesome Pro version.")]
        public bool IsProVersion { get; } = false;

        private async Task OnExecute()
        {
            var generator = new FontAwesomeGenerator();
            await generator.GenerateAllAsync(Version, !IsProVersion, SourceDirectory, FontAwesomeSvgDirectory, SvgOutputDirectory, SvgOutputFileNamePrefix ?? "FontAwesomeSvg");
        }
    }
}