#if FontAwesomePro
using FontAwesome6.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FontAwesome6.Svg.Extensions;
using FontAwesome6.Shared.Extensions;

namespace FontAwesome6.Svg
{
  public partial class SvgAwesome : Viewbox, ISpinable, IRotatable, IFlippable, IPulsable
  {
    /// <summary>
    /// Identifies the FontAwesome6.Svg.SvgAwesome.SecondaryColor dependency property.
    /// </summary>
    public static readonly DependencyProperty SecondaryColorProperty =
        DependencyProperty.Register("SecondaryColor", typeof(Brush), typeof(SvgAwesome), new PropertyMetadata(Brushes.Black, OnIconPropertyChanged));

    /// <summary>
    /// Identifies the FontAwesome6.Svg.SvgAwesome.PrimaryOpacity dependency property.
    /// </summary>
    public static readonly DependencyProperty PrimaryOpacityProperty =
        DependencyProperty.Register("PrimaryOpacity", typeof(double?), typeof(SvgAwesome), new PropertyMetadata(null, OnIconPropertyChanged));

    /// <summary>
    /// Identifies the FontAwesome6.Svg.SvgAwesome.SecondaryOpacity daryColor dependency property.
    /// </summary>
    public static readonly DependencyProperty SecondaryOpacityProperty =
        DependencyProperty.Register("SecondaryOpacity", typeof(double?), typeof(SvgAwesome), new PropertyMetadata(null, OnIconPropertyChanged));

    /// <summary>
    /// Identifies the FontAwesome6.Svg.SvgAwesome.SwapOpacity daryColor dependency property.
    /// </summary>
    public static readonly DependencyProperty SwapOpacityProperty =
        DependencyProperty.Register("SwapOpacity", typeof(bool?), typeof(SvgAwesome), new PropertyMetadata(null, OnIconPropertyChanged));

    /// <summary>
    /// Gets or sets the secondary brush of the icon. Changing this property will cause the icon to be redrawn.
    /// Duotone icons only!
    /// </summary>
    public Brush SecondaryColor
    {
      get { return (Brush)GetValue(SecondaryColorProperty); }
      set { SetValue(SecondaryColorProperty, value); }
    }


    /// <summary>
    /// Gets or sets the primary opacity of the icon. Changing this property will cause the icon to be redrawn.
    /// Duotone icons only!
    /// </summary>
    public double? PrimaryOpacity
    {
      get { return (double?)GetValue(PrimaryOpacityProperty); }
      set { SetValue(PrimaryOpacityProperty, value); }
    }

    /// <summary>
    /// Gets or sets the secondary opacity of the icon. Changing this property will cause the icon to be redrawn.
    /// Duotone icons only!
    /// </summary>
    public double? SecondaryOpacity
    {
      get { return (double?)GetValue(SecondaryOpacityProperty); }
      set { SetValue(SecondaryOpacityProperty, value); }
    }


    /// <summary>
    /// Gets or sets the swap opacity setting of the icon. Changing this property will cause the icon to be redrawn.
    /// Duotone icons only!
    /// </summary>
    public bool? SwapOpacity
    {
      get { return (bool?)GetValue(SwapOpacityProperty); }
      set { SetValue(SwapOpacityProperty, value); }
    }

    private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is not SvgAwesome svgAwesome)
      {
        return;
      }

      if (svgAwesome.Icon == EFontAwesomeIcon.None)
      {
        svgAwesome.Child = null;
      }
      else
      {
        svgAwesome.Child = svgAwesome.Icon.CreateCanvas(svgAwesome.PrimaryColor, svgAwesome.SecondaryColor, svgAwesome.SwapOpacity, svgAwesome.PrimaryOpacity, svgAwesome.SecondaryOpacity);
      }
    }
  }
}
#endif