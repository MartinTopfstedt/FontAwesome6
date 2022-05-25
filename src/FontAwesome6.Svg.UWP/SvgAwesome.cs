using FontAwesome6.Shared.Extensions;
using FontAwesome6.Svg.Extensions;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FontAwesome6.Svg
{
    public partial class SvgAwesome : ContentControl, ISpinable, IRotatable, IFlippable, IPulsable
    {
        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(EFontAwesomeIcon), typeof(SvgAwesome), new PropertyMetadata(EFontAwesomeIcon.None, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.PrimaryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryColorProperty =
            DependencyProperty.Register("PrimaryColor", typeof(Brush), typeof(SvgAwesome), new PropertyMetadata(FontAwesomeDefaults.PrimaryColor, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty =
            DependencyProperty.Register("Spin", typeof(bool), typeof(SvgAwesome), new PropertyMetadata(false, OnSpinPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty =
            DependencyProperty.Register("SpinDuration", typeof(double), typeof(SvgAwesome), new PropertyMetadata(1d, SpinDurationChanged));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.FontAwesome.Pulse dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseProperty =
            DependencyProperty.Register("Pulse", typeof(bool), typeof(SvgAwesome), new PropertyMetadata(false, OnPulsePropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.FontAwesome.PulseDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseDurationProperty =
            DependencyProperty.Register("PulseDuration", typeof(double), typeof(SvgAwesome), new PropertyMetadata(1d, PulseDurationChanged));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(double), typeof(SvgAwesome), new PropertyMetadata(0d, RotationChanged));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.SvgAwesome.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty =
            DependencyProperty.Register("FlipOrientation", typeof(EFlipOrientation), typeof(SvgAwesome), new PropertyMetadata(EFlipOrientation.Normal, FlipOrientationChanged));

        /// <summary>
        /// Gets or sets the FontAwesome icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public EFontAwesomeIcon Icon
        {
            get { return (EFontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the primary brush of the icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public Brush PrimaryColor
        {
            get { return (Brush)GetValue(PrimaryColorProperty); }
            set { SetValue(PrimaryColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current spin (angle) animation of the icon.
        /// </summary>
        public bool Spin
        {
            get { return (bool)GetValue(SpinProperty); }
            set { SetValue(SpinProperty, value); }
        }

        private static void OnSpinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                svgAwesome.BeginSpin();
            }
            else
            {
                svgAwesome.StopSpin();
                svgAwesome.SetRotation();
            }
        }

        /// <summary>
        /// Gets or sets the duration of the spinning animation (in seconds). This will stop and start the spin animation.
        /// </summary>
        public double SpinDuration
        {
            get { return (double)GetValue(SpinDurationProperty); }
            set { SetValue(SpinDurationProperty, value); }
        }

        private static void SpinDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome) || !svgAwesome.Spin || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            svgAwesome.StopSpin();
            svgAwesome.BeginSpin();
        }

        /// <summary>
        /// Gets or sets the current pulse animation of the icon.
        /// </summary>
        public bool Pulse
        {
            get { return (bool)GetValue(PulseProperty); }
            set { SetValue(PulseProperty, value); }
        }

        private static void OnPulsePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                svgAwesome.BeginPulse();
            }
            else
            {
                svgAwesome.StopPulse();
                svgAwesome.SetRotation();
            }
        }

        /// <summary>
        /// Gets or sets the duration of the pulse animation (in seconds). This will stop and start the pulse animation.
        /// </summary>
        public double PulseDuration
        {
            get { return (double)GetValue(PulseDurationProperty); }
            set { SetValue(PulseDurationProperty, value); }
        }

        private static void PulseDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome) || !svgAwesome.Pulse || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            svgAwesome.StopPulse();
            svgAwesome.BeginPulse();
        }

        /// <summary>
        /// Gets or sets the current rotation (angle).
        /// </summary>
        public new double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }


        private static void RotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome) || svgAwesome.Spin || !(e.NewValue is double newValue) || newValue.Equals(e.OldValue))
            {
                return;
            }

            svgAwesome.SetRotation(newValue);
        }

        /// <summary>
        /// Gets or sets the current orientation (horizontal, vertical).
        /// </summary>
        public EFlipOrientation FlipOrientation
        {
            get { return (EFlipOrientation)GetValue(FlipOrientationProperty); }
            set { SetValue(FlipOrientationProperty, value); }
        }

        private static void FlipOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome) || !(e.NewValue is EFlipOrientation) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            svgAwesome.SetFlipOrientation();
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome))
            {
                return;
            }

            if (svgAwesome.Icon == EFontAwesomeIcon.None)
            {
                svgAwesome.Content = null;
            }
            else
            {
                var viewBox = new Viewbox();
                viewBox.Width = svgAwesome.Width;
                viewBox.Height = svgAwesome.Height;
                svgAwesome.Content = viewBox;
#if FontAwesomePro
                viewBox.Child = svgAwesome.Icon.CreateCanvas(svgAwesome.PrimaryColor, svgAwesome.SecondaryColor, svgAwesome.SwapOpacity, svgAwesome.PrimaryOpacity, svgAwesome.SecondaryOpacity);
#else
                viewBox.Child = svgAwesome.Icon.CreateCanvas(svgAwesome.PrimaryColor);
#endif
            }
        }
    }
}
