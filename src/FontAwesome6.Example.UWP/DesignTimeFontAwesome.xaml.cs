using System.IO;

using Windows.UI.Xaml.Controls;

namespace FontAwesome6.Example.UWP
{
    public partial class DesignTimeFontAwesome : UserControl
    {
        public DesignTimeFontAwesome()
        {
            InitializeComponent();
#if FontAwesomePro
            Fonts.FontAwesomeFonts.LoadAllStyles("ms-appx:///Fonts/");

            Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.UWP.Svg.FontAwesomeSvg.all.json", typeof(MainPage).Assembly);
#endif
        }
    }
}
