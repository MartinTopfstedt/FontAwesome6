using System.IO;
using System.Windows.Controls;

namespace FontAwesome6.Example.WPF
{
    public partial class DesignTimeFontAwesome : UserControl
    {
        public DesignTimeFontAwesome()
        {
            InitializeComponent();
#if FontAwesomePro
            var directory = Path.GetDirectoryName(typeof(App).Assembly.Location);
            // loading FontAwesome 6 Font Files (otfs)
            var fontDirectory = Path.Combine(directory, "Fonts") + "/";

            // load all styles from a directory
            Fonts.FontAwesomeFonts.LoadAllStyles(fontDirectory);

            Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.WPF.Svg.FontAwesomeSvg.all.json", typeof(App).Assembly);
#endif
        }
    }
}
