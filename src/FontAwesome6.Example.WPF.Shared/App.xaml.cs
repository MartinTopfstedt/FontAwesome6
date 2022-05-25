using System.CodeDom;
using System.IO;
using System.Reflection;
using System.Windows;

namespace FontAwesome6.Example.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            LoadFontAwesomeFonts();
            LoadFontAwesomeSvg();            
        }

        public static void LoadFontAwesomeFonts()
        {
            var directory = Path.GetDirectoryName(typeof(App).Assembly.Location);
#if FontAwesomePro
            // loading FontAwesome 6 Font Files (otfs)
            var fontDirectory = Path.Combine(directory, "Fonts") + "/";

            // load all styles from a directory
            Fonts.FontAwesomeFonts.LoadAllStyles(fontDirectory);

            // or load all styles from a directory
            // Fonts.FontAwesomeFonts.LoadAllStyles(new Uri("pack://application:,,,/Fonts/"));

            // or load specific styles from a directory      
            // Fonts.FontAwesomeFonts.LoadStyles(fontDirectory, EFontAwesomeStyle.Solid, EFontAwesomeStyle.Light);

            // or load specific styles from resources
            // Fonts.FontAwesomeFonts.LoadStyles(new Uri("pack://application:,,,/Fonts/"), EFontAwesomeStyle.Solid, EFontAwesomeStyle.Light);
#endif
        }

        public static void LoadFontAwesomeSvg()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
#if FontAwesomePro
            // load SVG Data

            // loading FontAwesome6 SVG Data from Resource for all EFontAwesomeStylesapp.xaml            
            Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.WPF.Svg.FontAwesomeSvg.all.json", typeof(App).Assembly);

            // loading FontAwesome6 SVG Data from Resource for the EFontAwesomeStyle.Thin
            // Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.WPF.Svg.FontAwesomeSvg.thin.json", Assembly.GetExecutingAssembly());

            // loading FontAwesome6 SVG Data from directory
            //Svg.FontAwesomeSvg.LoadFromDirectory(Path.Combine(directory, "Svg", "FontAwesomeSvg.all.json"));
#endif
        }

    }
}
