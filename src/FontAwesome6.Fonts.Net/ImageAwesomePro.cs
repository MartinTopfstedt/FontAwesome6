
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome6.Fonts
{
    /// <summary>
    /// Represents a control that draws an FontAwesome icon as an image.
    /// </summary>
    public partial class ImageAwesome
      : Image, ISpinable, IRotatable, IFlippable, IPulsable
    {

        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.SecondaryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register("SecondaryColor", typeof(Brush), typeof(ImageAwesome), new PropertyMetadata(FontAwesomeDefaults.SecondaryColor, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.PrimaryOpacity dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryOpacityProperty =
            DependencyProperty.Register("PrimaryOpacity", typeof(double?), typeof(ImageAwesome), new PropertyMetadata(null, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.SecondaryOpacity daryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty SecondaryOpacityProperty =
            DependencyProperty.Register("SecondaryOpacity", typeof(double?), typeof(ImageAwesome), new PropertyMetadata(null, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.SwapOpacity daryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty SwapOpacityProperty =
            DependencyProperty.Register("SwapOpacity", typeof(bool?), typeof(ImageAwesome), new PropertyMetadata(null, OnIconPropertyChanged));

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


    }
}
