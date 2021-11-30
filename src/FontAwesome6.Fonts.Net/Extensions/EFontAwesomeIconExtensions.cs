using FontAwesome6.Extensions;

using System.Globalization;
using System.Windows;
using System.Windows.Media;

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
            return FontAwesomeFonts.GetTypeface(icon.GetStyle());
        }

        /// <summary>
        /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be drawn.</param>
        /// <param name="primary">The System.Windows.Media.Brush to be used as the primary color.</param>    
        /// <param name="secondary">The System.Windows.Media.Brush to be used as the secondary color.</param>    
        /// <param name="swapOpacity">The boolean to be used for swapping the opacity between primary and secondary colors.</param>    
        /// <param name="primaryOpacity">The double? as the primary opacity.</param>    
        /// <param name="secondaryOpacity">The double? as the secondary opacity.</param>  
        /// <param name="emSize">The font size in em.</param>
        /// <returns>A new System.Windows.Media.ImageSource</returns>
        public static ImageSource CreateImageSource(this EFontAwesomeIcon icon, Brush primary, Brush secondary = null, bool? swapOpacity = null, double? primaryOpacity = null, double? secondaryOpacity = null, double emSize = 100)
        {
            return new DrawingImage(icon.CreateDrawing(primary, secondary, swapOpacity, primaryOpacity, secondaryOpacity, emSize));
        }

        /// <summary>
        /// Creates a new System.Windows.Media.Drawing of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
        /// This will use the Font for the Drawing creation.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be drawn.</param>
        /// <param name="primary">The System.Windows.Media.Brush to be used as the primary color.</param>    
        /// <param name="secondary">The System.Windows.Media.Brush to be used as the secondary color.</param>    
        /// <param name="swapOpacity">The boolean to be used for swapping the opacity between primary and secondary colors.</param>    
        /// <param name="primaryOpacity">The double? as the primary opacity.</param>    
        /// <param name="secondaryOpacity">The double? as the secondary opacity.</param>  
        /// <param name="emSize">The font size in em.</param>
        /// <returns>A new System.Windows.Media.Drawing</returns>
        public static Drawing CreateDrawing(this EFontAwesomeIcon icon, Brush primary, Brush secondary = null, bool? swapOpacity = null, double? primaryOpacity = null, double? secondaryOpacity = null, double emSize = 100)
        {
            if (!FontAwesomeUnicodes.Data.TryGetValue(icon.GetIconName(), out var info))
            {
                return null;
            }

            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                var typeface = icon.GetTypeFace();
#if FontAwesomePro
        if (icon.IsDuotone())
        {
          var primaryClone = primary.Clone();
          primaryClone.Opacity = primaryOpacity ?? 1;

          var secondaryClone = (secondary ?? primary).Clone();
          secondaryClone.Opacity = secondaryOpacity ?? info.Item2;

          if (swapOpacity.HasValue && swapOpacity.Value)
          {
            var temp = primaryClone.Opacity;
            primaryClone.Opacity = secondaryClone.Opacity;
            secondaryClone.Opacity = temp;
          }
          primaryClone.Freeze();
          secondaryClone.Freeze();

          var primaryGlyph = CreateFormattedText(info.Item1 + "\ufe01", typeface, primaryClone, emSize);
          var secondaryGlyph = CreateFormattedText(info.Item1 + "\ufe02", typeface, secondaryClone, emSize);

          drawingContext.DrawGeometry(primaryClone, null, primaryGlyph.BuildGeometry(new Point(0, 0)));
          drawingContext.DrawGeometry(secondaryClone, null, secondaryGlyph.BuildGeometry(new Point(0, 0)));
        }
        else
        {
          var primaryGlyph = CreateFormattedText(info.Item1, typeface, primary, emSize);
          drawingContext.DrawGeometry(primary, null, primaryGlyph.BuildGeometry(new Point(0, 0)));
        }
#else
                var primaryGlyph = CreateFormattedText(info.Item1, typeface, primary, emSize);
                drawingContext.DrawGeometry(primary, null, primaryGlyph.BuildGeometry(new Point(0, 0)));
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
        public static FormattedText CreateFormattedText(string text, Typeface typeface, Brush foregroundBrush, double emSize = 100)
        {
            if (string.IsNullOrEmpty(text) || typeface == null)
            {
                return null;
            }

            return new FormattedText(text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
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
