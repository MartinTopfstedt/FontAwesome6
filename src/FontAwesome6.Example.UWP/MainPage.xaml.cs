using System.Reflection;

using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FontAwesome6.Example.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
#if FontAwesomePro
            LoadFontAwesomeFonts();
            LoadFontAwesomeSvg();
#endif
            this.DataContext = new MainWindowViewModel();
            this.InitializeComponent();
        }

        public void LoadFontAwesomeFonts()
        {

          // loading FontAwesome 6 Font Files (otfs)
      
          // load all styles from resources
          Fonts.FontAwesomeFonts.LoadAllStyles("ms-appx:///Fonts/");

          // or load specific styles from resources
          // Fonts.FontAwesomeFonts.LoadStyles(new Uri("ms-appx:///FontAwesome6.Example.UWP/Fonts/"), EFontAwesomeStyle.Solid, EFontAwesomeStyle.Light);
        }

        public void LoadFontAwesomeSvg()
        {
            // load SVG Data

            // loading FontAwesome6 SVG Data from Resource for all EFontAwesomeStyles
            Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.UWP.Svg.FontAwesomeSvg.all.json", Assembly.GetExecutingAssembly());

            // loading FontAwesome6 SVG Data from Resource for the EFontAwesomeStyle.Thin
            // Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.UWP.Svg.FontAwesomeSvg.thin.json", Assembly.GetExecutingAssembly());

            // loading FontAwesome6 SVG Data from directory
            // var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Svg.FontAwesomeSvg.LoadFromDirectory(Path.Combine(directory, "Svg", "FontAwesomeSvg.all.json"));
        }
    }
}
