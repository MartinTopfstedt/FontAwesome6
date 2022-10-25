#if FontAwesomePro
using FontAwesome6.Extensions;
#endif
using FontAwesome6.Fonts.Extensions;
using FontAwesome6.Shared.Extensions;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome6.Fonts.AttachedProperties
{
    public static partial class FontAwesome
    {
        /// <summary>
        /// Identifies the FontAwesome.Fonts.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached("Icon", typeof(EFontAwesomeIcon), typeof(FontAwesome), new PropertyMetadata(EFontAwesomeIcon.None, OnRenderingTriggered));

        /// <summary>
        /// Identifies the FontAwesome.Fonts.PrimaryColor dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryColorProperty =
            DependencyProperty.RegisterAttached("PrimaryColor", typeof(Brush), typeof(FontAwesome), new PropertyMetadata(FontAwesomeDefaults.PrimaryColor, OnRenderingTriggered));

        /// <summary>
        /// Identifies the FontAwesome.Fonts.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty =
            DependencyProperty.RegisterAttached("Spin", typeof(bool), typeof(FontAwesome), new PropertyMetadata(false, OnSpinPropertyChanged, SpinCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.Fonts.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty =
            DependencyProperty.RegisterAttached("SpinDuration", typeof(double), typeof(FontAwesome), new PropertyMetadata(1d, OnSpinDurationChanged, OnSpinDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.Fonts.Pulse dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseProperty =
            DependencyProperty.RegisterAttached("Pulse", typeof(bool), typeof(FontAwesome), new PropertyMetadata(false, OnPulsePropertyChanged, PulseCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.Fonts.PulseDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty PulseDurationProperty =
            DependencyProperty.RegisterAttached("PulseDuration", typeof(double), typeof(FontAwesome), new PropertyMetadata(1d, OnPulseDurationChanged, OnPulseDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.Fonts.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.RegisterAttached("Rotation", typeof(double), typeof(FontAwesome), new PropertyMetadata(0d, OnRotationChanged, OnRotationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.Fonts.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty =
            DependencyProperty.RegisterAttached("FlipOrientation", typeof(EFlipOrientation), typeof(FontAwesome), new PropertyMetadata(EFlipOrientation.Normal, OnFlipOrientationChanged));


        public static EFontAwesomeIcon GetIcon(DependencyObject target)
        {
            return (EFontAwesomeIcon)target.GetValue(IconProperty);
        }
        public static void SetIcon(DependencyObject target, EFontAwesomeIcon value)
        {
            target.SetValue(IconProperty, value);
        }

        public static Brush GetPrimaryColor(DependencyObject target)
        {
            return (Brush)target.GetValue(PrimaryColorProperty);
        }
        public static void SetPrimaryColor(DependencyObject target, Brush value)
        {
            target.SetValue(PrimaryColorProperty, value);
        }

        public static bool GetSpin(DependencyObject target)
        {
            return (bool)target.GetValue(SpinProperty);
        }
        public static void SetSpin(DependencyObject target, bool value)
        {
            target.SetValue(SpinProperty, value);
        }

        public static double GetSpinDuration(DependencyObject target)
        {
            return (double)target.GetValue(SpinDurationProperty);
        }
        public static void SetSpinDuration(DependencyObject target, double value)
        {
            target.SetValue(SpinDurationProperty, value);
        }

        public static bool GetPulse(DependencyObject target)
        {
            return (bool)target.GetValue(PulseProperty);
        }
        public static void SetPulse(DependencyObject target, bool value)
        {
            target.SetValue(PulseProperty, value);
        }

        public static double GetPulseDuration(DependencyObject target)
        {
            return (double)target.GetValue(PulseDurationProperty);
        }
        public static void SetPulseDuration(DependencyObject target, double value)
        {
            target.SetValue(PulseDurationProperty, value);
        }

        public static double GetRotation(DependencyObject target)
        {
            return (double)target.GetValue(RotationProperty);
        }
        public static void SetRotation(DependencyObject target, double value)
        {
            target.SetValue(RotationProperty, value);
        }

        public static EFlipOrientation GetFlipOrientation(DependencyObject target)
        {
            return (EFlipOrientation)target.GetValue(FlipOrientationProperty);
        }
        public static void SetFlipOrientation(DependencyObject target, EFlipOrientation value)
        {
            target.SetValue(FlipOrientationProperty, value);
        }

        private static void OnRenderingTriggered(DependencyObject sender, DependencyPropertyChangedEventArgs evt)
        {
            switch (sender)
            {
                case Window target:
                    {
                        var icon = GetIcon(sender);
                        if (icon == EFontAwesomeIcon.None)
                        {
                            target.SetValue(Window.IconProperty, null);
                        }
                        else
                        {
                            var primaryColor = GetPrimaryColor(sender);
#if FontAwesomePro
                            var secondaryColor = GetSecondaryColor(sender);
                            var swapOpacity = GetSwapOpacity(sender);
                            var primaryOpacity = GetPrimaryOpacity(sender);
                            var secondaryOpacity = GetSecondaryOpacity(sender);
                            target.SetValue(Window.IconProperty, icon.CreateImageSource(primaryColor, secondaryColor, swapOpacity, primaryOpacity, secondaryOpacity));
#endif                            
                            target.SetValue(Window.IconProperty, icon.CreateImageSource(primaryColor));
                        }
                    }
                    break;
                case Image target:
                    {
                        var icon = GetIcon(sender);
                        if (icon == EFontAwesomeIcon.None)
                        {
                            target.Source = null;
                        }
                        else
                        {
                            var primaryColor = GetPrimaryColor(sender);
#if FontAwesomePro
                            var secondaryColor = GetSecondaryColor(sender);
                            var swapOpacity = GetSwapOpacity(sender);
                            var primaryOpacity = GetPrimaryOpacity(sender);
                            var secondaryOpacity = GetSecondaryOpacity(sender);
                            target.Source = icon.CreateImageSource(primaryColor, secondaryColor, swapOpacity, primaryOpacity, secondaryOpacity);
#endif
                            target.Source = icon.CreateImageSource(primaryColor);
                        }
                    }
                    break;
                case ContentControl target:
                    {
                        var icon = GetIcon(sender);
                        if (icon == EFontAwesomeIcon.None)
                        {
                            target.SetValue(ContentControl.ContentProperty, null);
                        }
#if FontAwesomePro
                        else if (icon.IsDuotone())
                        {
                          var primaryColor = GetPrimaryColor(sender);
                          var secondaryColor = GetSecondaryColor(sender);
                          var swapOpacity = GetSwapOpacity(sender);
                          var primaryOpacity = GetPrimaryOpacity(sender);
                          var secondaryOpacity = GetSecondaryOpacity(sender);

                          var img = new Image();
                          img.Source = icon.CreateImageSource(primaryColor, secondaryColor, swapOpacity, primaryOpacity, secondaryOpacity);

                          target.SetValue(ContentControl.ContentProperty, img);
                        }
#endif
                        else
                        {
                            target.SetValue(ContentControl.FontFamilyProperty, icon.GetFontFamily());
                            target.SetValue(ContentControl.ContentProperty, icon.GetUnicode());
                        }
                    }
                    break;
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        private static void OnSpinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                var spinDuration = GetSpinDuration(d);
                element.BeginSpin(spinDuration);
            }
            else
            {
                element.StopSpin();

                var isRotationSet = d.ReadLocalValue(RotationProperty);
                if (isRotationSet != DependencyProperty.UnsetValue)
                {
                    element.SetRotation(GetRotation(d));
                }
            }
        }

        private static object SpinCoerceValue(DependencyObject d, object basevalue)
        {
            if (d is not FrameworkElement)
            {
                return false;
            }

            var isVisible = (bool)d.GetValue(UIElement.IsVisibleProperty);
            var opacity = (double)d.GetValue(UIElement.OpacityProperty);
            var spinDuration = GetSpinDuration(d);
            return !isVisible || opacity == 0.0 || spinDuration == 0.0 ? false : basevalue;
        }

        private static void OnSpinDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!GetSpin(d) && d is FrameworkElement element && e.NewValue is double duration && duration.Equals(e.OldValue))
            {
                element.StopSpin();
                element.BeginSpin(duration);
            }
        }

        private static object OnSpinDurationCoerceValue(DependencyObject d, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : value;
        }

        private static void OnPulsePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                element.BeginPulse(GetPulseDuration(d));
            }
            else
            {
                element.StopPulse();

                var isRotationSet = d.ReadLocalValue(RotationProperty);
                if (isRotationSet != DependencyProperty.UnsetValue)
                {
                    element.SetRotation(GetRotation(d));
                }
            }
        }

        private static object PulseCoerceValue(DependencyObject d, object basevalue)
        {
            if (d is not FrameworkElement || d is not IPulsable pulsable)
            {
                return false;
            }

            var isVisible = (bool)d.GetValue(UIElement.IsVisibleProperty);
            var opacity = (double)d.GetValue(UIElement.OpacityProperty);
            return !isVisible || opacity == 0.0 || pulsable.PulseDuration == 0.0 ? false : basevalue;
        }

        private static void OnPulseDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetPulse(d) && d is FrameworkElement element && e.NewValue is double duration && !duration.Equals(e.OldValue))
            {
                element.StopPulse();
                element.BeginPulse(duration);
            }

        }

        private static object OnPulseDurationCoerceValue(DependencyObject d, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : value;
        }

        private static void OnRotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!GetSpin(d) && d is FrameworkElement element && e.NewValue is double rotation && !rotation.Equals(e.OldValue))
            {
                element.SetRotation(rotation);
            }
        }

        private static object OnRotationCoerceValue(DependencyObject d, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : (val > 360 ? 360d : value);
        }

        private static void OnFlipOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && e.NewValue is EFlipOrientation orientation && !orientation.Equals(e.OldValue))
            {
                element.SetFlipOrientation(orientation);
            }
        }
    }
}