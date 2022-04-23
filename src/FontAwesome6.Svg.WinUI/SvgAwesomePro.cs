
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace FontAwesome6.Svg
{
    public partial class SvgAwesome
    {
        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.SecondaryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register("SecondaryColor", typeof(Brush), typeof(SvgAwesome), new PropertyMetadata(FontAwesomeDefaults.SecondaryColor, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.PrimaryOpacity dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryOpacityProperty =
            DependencyProperty.Register("PrimaryOpacity", typeof(double), typeof(SvgAwesome), new PropertyMetadata(1.0, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.SecondaryOpacity daryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty SecondaryOpacityProperty =
            DependencyProperty.Register("SecondaryOpacity", typeof(double), typeof(SvgAwesome), new PropertyMetadata(0.4, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.SwapOpacity daryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty SwapOpacityProperty =
            DependencyProperty.Register("SwapOpacity", typeof(bool), typeof(SvgAwesome), new PropertyMetadata(false, OnIconPropertyChanged));

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
        public double PrimaryOpacity
        {
            get { return (double)GetValue(PrimaryOpacityProperty); }
            set { SetValue(PrimaryOpacityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the secondary opacity of the icon. Changing this property will cause the icon to be redrawn.
        /// Duotone icons only!
        /// </summary>
        public double SecondaryOpacity
        {
            get { return (double)GetValue(SecondaryOpacityProperty); }
            set { SetValue(SecondaryOpacityProperty, value); }
        }


        /// <summary>
        /// Gets or sets the swap opacity setting of the icon. Changing this property will cause the icon to be redrawn.
        /// Duotone icons only!
        /// </summary>
        public bool SwapOpacity
        {
            get { return (bool)GetValue(SwapOpacityProperty); }
            set { SetValue(SwapOpacityProperty, value); }
        }
    }
}