using System;
using System.Collections.Generic;
using System.Text;

namespace FontAwesome6.Svg.Shared
{
  public class FontAwesomeSvgInformation
  {
    public FontAwesomeSvgInformation(string[] paths, int width, int height, double opacity)
    {
      Paths = paths;
      Width = width;
      Height = height;
      Opacity = opacity;
    }

    public string[] Paths { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public double Opacity { get; set; }
  }
}
