﻿using FontAwesome6.Shared.Extensions;
using FontAwesome6.Svg.Extensions;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome6.Svg
{
    /// <summary>
    /// Represents a control that draws an FontAwesome icon as an image.
    /// </summary>
    public partial class ImageAwesome
      : Image, ISpinable, IRotatable, IFlippable, IPulsable
    {
        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.Foreground dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ImageAwesome), new PropertyMetadata(FontAwesomeDefaults.PrimaryColor, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
                DependencyProperty.Register("Icon", typeof(EFontAwesomeIcon), typeof(ImageAwesome), new PropertyMetadata(EFontAwesomeIcon.None, OnIconPropertyChanged));


        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.PrimaryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryColorProperty =
            DependencyProperty.Register("PrimaryColor", typeof(Brush), typeof(ImageAwesome), new PropertyMetadata(FontAwesomeDefaults.PrimaryColor, OnIconPropertyChanged));

        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty =
            DependencyProperty.Register("Spin", typeof(bool), typeof(ImageAwesome), new PropertyMetadata(false, OnSpinPropertyChanged, SpinCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty =
            DependencyProperty.Register("SpinDuration", typeof(double), typeof(ImageAwesome), new PropertyMetadata(1d, SpinDurationChanged, SpinDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.FontAwesome.Pulse dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseProperty =
            DependencyProperty.Register("Pulse", typeof(bool), typeof(ImageAwesome), new PropertyMetadata(false, OnPulsePropertyChanged, PulseCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.FontAwesome.PulseDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseDurationProperty =
            DependencyProperty.Register("PulseDuration", typeof(double), typeof(ImageAwesome), new PropertyMetadata(1d, PulseDurationChanged, PulseDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(double), typeof(ImageAwesome), new PropertyMetadata(0d, RotationChanged, RotationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome6.Svg.ImageAwesome.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty =
            DependencyProperty.Register("FlipOrientation", typeof(EFlipOrientation), typeof(ImageAwesome), new PropertyMetadata(EFlipOrientation.Normal, FlipOrientationChanged));

        static ImageAwesome()
        {
            OpacityProperty.OverrideMetadata(typeof(ImageAwesome), new UIPropertyMetadata(1.0, OpacityChanged));
        }

        public ImageAwesome()
        {
            IsVisibleChanged += (s, a) => CoerceValue(SpinProperty);
        }

        private static void OpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(SpinProperty);
        }

        /// <summary>
        /// Gets or sets the foreground brush of the icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        [Obsolete("Use PrimaryColor instead.")]
        public Brush Foreground
        {
            get { return (Brush)GetValue(PrimaryColorProperty); }
            set { SetValue(PrimaryColorProperty, value); }
        }

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
            if (d is not ImageAwesome imageAwesome)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                imageAwesome.BeginSpin();
            }
            else
            {
                imageAwesome.StopSpin();
                imageAwesome.SetRotation();
            }
        }

        private static object SpinCoerceValue(DependencyObject d, object basevalue)
        {
            var imageAwesome = (ImageAwesome)d;

            return !imageAwesome.IsVisible || imageAwesome.Opacity == 0.0 || imageAwesome.SpinDuration == 0.0 ? false : basevalue;
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
            if (d is not ImageAwesome imageAwesome || !imageAwesome.Spin || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            imageAwesome.StopSpin();
            imageAwesome.BeginSpin();
        }

        private static object SpinDurationCoerceValue(DependencyObject d, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : value;
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
            if (d is not ImageAwesome fontAwesome)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                fontAwesome.BeginPulse();
            }
            else
            {
                fontAwesome.StopPulse();
                fontAwesome.SetRotation();
            }
        }

        private static object PulseCoerceValue(DependencyObject d, object basevalue)
        {
            var fontAwesome = (ImageAwesome)d;

            return !fontAwesome.IsVisible || fontAwesome.Opacity == 0.0 || fontAwesome.PulseDuration == 0.0 ? false : basevalue;
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
            if (d is not ImageAwesome fontAwesome || !fontAwesome.Pulse || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            fontAwesome.StopPulse();
            fontAwesome.BeginPulse();
        }

        private static object PulseDurationCoerceValue(DependencyObject d, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : value;
        }

        /// <summary>
        /// Gets or sets the current rotation (angle).
        /// </summary>
        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }


        private static void RotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ImageAwesome imageAwesome || imageAwesome.Spin || !(e.NewValue is double) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            imageAwesome.SetRotation();
        }

        private static object RotationCoerceValue(DependencyObject d, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : (val > 360 ? 360d : value);
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
            if (d is not ImageAwesome imageAwesome || !(e.NewValue is EFlipOrientation) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            imageAwesome.SetFlipOrientation();
        }


        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ImageAwesome control)
            {
                return;
            }

            if (control.Icon == EFontAwesomeIcon.None)
            {
                control.Source = null;
            }
            else
            {
#if FontAwesomePro
                control.Source = control.Icon.CreateImageSource(control.PrimaryColor, control.SecondaryColor, control.SwapOpacity, control.PrimaryOpacity, control.SecondaryOpacity);
#else
                control.Source = control.Icon.CreateImageSource(control.PrimaryColor);
#endif
            }
        }
    }
}
