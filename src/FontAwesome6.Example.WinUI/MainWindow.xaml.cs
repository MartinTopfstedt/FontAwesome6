using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FontAwesome6.Example.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
#if FontAwesomePro
            LoadFontAwesomeFonts();
            LoadFontAwesomeSvg();
#endif
            this.InitializeComponent();
            if (Content is FrameworkElement element)
            {
                element.DataContext = new MainWindowViewModel();
            }
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
            Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.WinUI.Svg.FontAwesomeSvg.all.json", Assembly.GetExecutingAssembly());

            // loading FontAwesome6 SVG Data from Resource for the EFontAwesomeStyle.Thin
            // Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.WinUI.Svg.FontAwesomeSvg.thin.json", Assembly.GetExecutingAssembly());

            // loading FontAwesome6 SVG Data from directory
            // var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Svg.FontAwesomeSvg.LoadFromDirectory(Path.Combine(directory, "Svg", "FontAwesomeSvg.all.json"));
        }
    }
}
