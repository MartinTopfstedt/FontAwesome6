using FontAwesome6.Extensions;

using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FontAwesome6.Fonts.Extensions
{
  /// <summary>
  /// EFontAwesomeIcon extensions
  /// </summary>
  public static partial class EFontAwesomeIconExtensions
  {
    /// <summary>
    /// Get the FontFamily of an icon
    /// </summary>
    public static FontFamily GetFontFamily(this EFontAwesomeIcon icon)
    {
      return FontAwesomeFonts.GetFontFamily(icon.GetStyle());
    }

    /// <summary>
    /// Get the Typeface of EFontAwesomeStyle
    /// </summary>
    public static Typeface GetTypeFace(this EFontAwesomeIcon icon)
    {
      return FontAwesomeFonts.GetTypeFace(icon.GetStyle());
    }

    /// <summary>
    /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
    /// </summary>
    /// <param name="icon">The FontAwesome icon to be drawn.</param>
    /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
    /// <param name="emSize">The font size in em.</param>
    /// <returns>A new System.Windows.Media.ImageSource</returns>
    public static ImageSource CreateImageSource(this EFontAwesomeIcon icon, Brush foregroundBrush, double emSize = 100)
    {
      return new DrawingImage(icon.CreateDrawing(foregroundBrush, emSize));
    }

    /// <summary>
    /// Creates a new System.Windows.Media.Drawing of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
    /// This will use the Font for the Drawing creation.
    /// </summary>
    /// <param name="icon">The FontAwesome icon to be drawn.</param>
    /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
    /// <param name="emSize">The font size in em.</param>
    /// <returns>A new System.Windows.Media.Drawing</returns>
    public static Drawing CreateDrawing(this EFontAwesomeIcon icon, Brush foregroundBrush, double emSize = 100)
    {
      var visual = new DrawingVisual();

      using (var drawingContext = visual.RenderOpen())
      {        
        var typeface = icon.GetTypeFace();
#if FontAwesomePro
        if (icon.IsDuotone() && icon.TryGetDuotoneUnicode(out var primary, out var secondary))
        {
          var primaryBrush = foregroundBrush.Clone();
          primaryBrush.Opacity = 1;

          drawingContext.DrawText(CreateFormattedText(primary, typeface, primaryBrush, emSize), new Point(0, 0));
         
          var secondaryBrush = foregroundBrush.Clone();
          secondaryBrush.Opacity = 0.4;

          drawingContext.DrawText(CreateFormattedText(secondary, typeface, secondaryBrush, emSize), new Point(0, 0));
        }
        else
        {
          var unicode = icon.GetUnicode();
          drawingContext.DrawText(CreateFormattedText(unicode, typeface, foregroundBrush, emSize), new Point(0, 0));
        }
#else
        drawingContext.DrawText(CreateFormattedText(unicode, typeface, foregroundBrush, emSize), new Point(0, 0));
#endif
      }
      return visual.Drawing;
    }

    /// <summary>
    /// Creates a new System.Windows.Media.FormattedText of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
    /// </summary>
    /// <param name="icon">The FontAwesome icon to be drawn.</param>
    /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
    /// <param name="emSize">The font size in em.</param>
    /// <returns>A new System.Windows.Media.FormattedText</returns>
    public static FormattedText CreateFormattedText(string unicode, Typeface typeface, Brush foregroundBrush, double emSize = 100)
    {
      if (string.IsNullOrEmpty(unicode) || typeface == null)
      {
        return null;
      }

      return new FormattedText(unicode, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                  typeface, emSize, foregroundBrush)
      {
        TextAlignment = TextAlignment.Center,
      };
    }

    /// <summary>
    /// Creates a new System.Windows.Media.FormattedText of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
    /// </summary>
    /// <param name="icon">The FontAwesome icon to be drawn.</param>
    /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
    /// <param name="emSize">The font size in em.</param>
    /// <returns>A new System.Windows.Media.FormattedText</returns>
    public static FormattedText CreateFormattedText(this EFontAwesomeIcon icon, Brush foregroundBrush, double emSize = 100)
    {
      return CreateFormattedText(icon.GetUnicode(), icon.GetTypeFace(), foregroundBrush, emSize);
    }
  }
}
