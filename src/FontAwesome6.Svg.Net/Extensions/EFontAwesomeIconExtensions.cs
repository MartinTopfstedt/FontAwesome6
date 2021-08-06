using FontAwesome6.Extensions;
using FontAwesome6.Svg;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FontAwesome6.Svg.Extensions
{
  /// <summary>
  /// EFontAwesomeIcon extensions
  /// </summary>
  public static partial class EFontAwesomeIconExtensions
  {
    /// <summary>
    /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
    /// </summary>
    /// <param name="icon">The FontAwesome icon to be drawn.</param>
    /// <param name="primary">The System.Windows.Media.Brush to be used as the primary color.</param>    
    /// <param name="secondary">The System.Windows.Media.Brush to be used as the secondary color.</param>    
    /// <param name="swapOpacity">The boolean to be used for swapping the opacity between primary and secondary colors.</param>    
    /// <param name="primaryOpacity">The double? as the primary opacity.</param>    
    /// <param name="secondaryOpacity">The double? as the secondary opacity.</param>    
    /// <returns>A new System.Windows.Media.ImageSource</returns>
    public static ImageSource CreateImageSource(this EFontAwesomeIcon icon, Brush primary, Brush secondary = null, bool? swapOpacity = null, double? primaryOpacity = null, double? secondaryOpacity = null)
    {
      return new DrawingImage(icon.CreateDrawing(primary, secondary, swapOpacity, primaryOpacity, secondaryOpacity));
    }

    /// <summary>
    /// Creates a new System.Windows.Media.Drawing of a specified FontAwesomeIcon.
    /// This will use the SVG for the Drawing creation.
    /// </summary>
    /// <param name="icon">The FontAwesome icon to be drawn.</param>
    /// <param name="primary">The System.Windows.Media.Brush to be used as the primary color.</param>    
    /// <param name="secondary">The System.Windows.Media.Brush to be used as the secondary color.</param>    
    /// <param name="swapOpacity">The boolean to be used for swapping the opacity between primary and secondary colors.</param>    
    /// <param name="primaryOpacity">The double? as the primary opacity.</param>    
    /// <param name="secondaryOpacity">The double? as the secondary opacity.</param>  
    /// <returns>A new System.Windows.Media.Drawing</returns>
    public static Drawing CreateDrawing(this EFontAwesomeIcon icon, Brush primary, Brush secondary = null, bool? swapOpacity = null, double? primaryOpacity = null, double? secondaryOpacity = null)
    {
      if (!FontAwesomeSvg.Data.TryGetValue(icon.ToString(), out var information))
      {
        return null;
      }

      var visual = new DrawingVisual();
      using (var drawingContext = visual.RenderOpen())
      {
        if (information.Paths.Length > 1)
        {
          var primaryClone = primary.Clone();
          primaryClone.Opacity = primaryOpacity ?? 1;

          var secondaryClone = (secondary ?? primary).Clone();
          secondaryClone.Opacity = secondaryOpacity ?? information.Opacity;

          if (swapOpacity.HasValue && swapOpacity.Value)
          {
            var temp = primaryClone.Opacity;
            primaryClone.Opacity = secondaryClone.Opacity;
            secondaryClone.Opacity = temp;
          }
          primaryClone.Freeze();
          secondaryClone.Freeze();

          drawingContext.DrawGeometry(primaryClone, null, Geometry.Parse(information.Paths[1]));
          drawingContext.DrawGeometry(secondaryClone, null, Geometry.Parse(information.Paths[0]));
        }
        else if (information.Paths.Length == 1)
        {
          drawingContext.DrawGeometry(primary, null, Geometry.Parse(information.Paths[0]));
        }
      }
      return visual.Drawing;
    }


    /// <summary>
    /// Creates a new System.Windows.UIElement of a specified FontAwesomeIcon
    /// </summary>
    /// <param name="icon">The FontAwesome icon to be drawn.</param>
    /// <param name="primary">The System.Windows.Media.Brush to be used as the primary color.</param>    
    /// <param name="secondary">The System.Windows.Media.Brush to be used as the secondary color.</param>    
    /// <param name="swapOpacity">The boolean to be used for swapping the opacity between primary and secondary colors.</param>    
    /// <param name="primaryOpacity">The double? as the primary opacity.</param>    
    /// <param name="secondaryOpacity">The double? as the secondary opacity.</param>    
    /// <returns>A new System.Windows.UIElement</returns>
    public static UIElement CreateCanvas(this EFontAwesomeIcon icon, Brush primary, Brush secondary = null, bool? swapOpacity = null, double? primaryOpacity = null, double? secondaryOpacity = null)
    {
      if (!FontAwesomeSvg.Data.TryGetValue(icon.ToString(), out var information))
      {
        return null;
      }
      else if (information.Paths.Length > 1)
      {
        var primaryClone = primary.Clone();
        primaryClone.Opacity = primaryOpacity ?? 1;

        var secondaryClone = (secondary ?? primary).Clone();
        secondaryClone.Opacity = secondaryOpacity ?? information.Opacity;

        if (swapOpacity.HasValue && swapOpacity.Value)
        {
          var temp = primaryClone.Opacity;
          primaryClone.Opacity = secondaryClone.Opacity;
          secondaryClone.Opacity = temp;
        }
        primaryClone.Freeze();
        secondaryClone.Freeze();

        var path1 = new Path();
        path1.Data = Geometry.Parse(information.Paths[1]);
        path1.Width = information.Width;
        path1.Height = information.Height;
        path1.Fill = primaryClone;
        

        var path2 = new Path();
        path2.Data = Geometry.Parse(information.Paths[0]);
        path2.Width = information.Width;
        path2.Height = information.Height;
        path2.Fill = secondaryClone;

        var canvas = new Canvas();
        canvas.Width = information.Width;
        canvas.Height = information.Height;
        canvas.Children.Add(path1);
        canvas.Children.Add(path2);

        return canvas;
      }
      else if (information.Paths.Length == 1)
      {
        return new Path
        {
          Data = Geometry.Parse(information.Paths[0]),
          Width = information.Width,
          Height = information.Height,
          Fill = primary
        };
      }

      return null;
    }    
  }
}
