using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
#if FontAwesomePro
      var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      Fonts.FontAwesomeFonts.LoadFontsFromDirectory(Path.Combine(directory, "Fonts") + "/");

      // load from resources
      //Fonts.FontAwesomeFonts.LoadFonts(new Uri("pack://application:,,,/Fonts/"));

      Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.WPF.Svg.FontAwesomeSvgPro.json", Assembly.GetExecutingAssembly());
#endif
    }
  }
}
