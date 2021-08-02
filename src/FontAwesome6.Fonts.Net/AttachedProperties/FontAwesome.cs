using FontAwesome6.Fonts.Extensions;
using FontAwesome6.Shared.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome6.Fonts.AttachedProperties
{
  public static class FontAwesome
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
        DependencyProperty.RegisterAttached("PrimaryColor", typeof(Brush), typeof(FontAwesome), new PropertyMetadata(Brushes.Black, OnRenderingTriggered));

    /// <summary>
    /// Identifies the FontAwesome.Fonts.SecondaryColor dependency property.
    /// </summary>
    public static readonly DependencyProperty SecondaryColorProperty =
        DependencyProperty.RegisterAttached("SecondaryColor", typeof(Brush), typeof(FontAwesome), new PropertyMetadata(Brushes.Black, OnRenderingTriggered));

    /// <summary>
    /// Identifies the FontAwesome.Fonts.PrimaryOpacity dependency property.
    /// </summary>
    public static readonly DependencyProperty PrimaryOpacityProperty =
        DependencyProperty.RegisterAttached("PrimaryOpacity", typeof(double?), typeof(FontAwesome), new PropertyMetadata(null, OnRenderingTriggered));

    /// <summary>
    /// Identifies the FontAwesome.Fonts.SecondaryOpacity daryColor dependency property.
    /// </summary>
    public static readonly DependencyProperty SecondaryOpacityProperty =
        DependencyProperty.RegisterAttached("SecondaryOpacity", typeof(double?), typeof(FontAwesome), new PropertyMetadata(null, OnRenderingTriggered));

    /// <summary>
    /// Identifies the FontAwesome.Fonts.SwapOpacity daryColor dependency property.
    /// </summary>
    public static readonly DependencyProperty SwapOpacityProperty =
        DependencyProperty.RegisterAttached("SwapOpacity", typeof(bool?), typeof(FontAwesome), new PropertyMetadata(null, OnRenderingTriggered));


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

    public static Brush GetSecondaryColor(DependencyObject target)
    {
      return (Brush)target.GetValue(SecondaryColorProperty);
    }
    public static void SetSecondaryColor(DependencyObject target, Brush value)
    {
      target.SetValue(SecondaryColorProperty, value);
    }

    public static bool? GetSwapOpacity(DependencyObject target)
    {
      return (bool?)target.GetValue(SwapOpacityProperty);
    }
    public static void SetSwapOpacity(DependencyObject target, bool? value)
    {
      target.SetValue(SwapOpacityProperty, value);
    }

    public static double? GetPrimaryOpacity(DependencyObject target)
    {
      return (double?)target.GetValue(PrimaryOpacityProperty);
    }
    public static void SetPrimaryOpacity(DependencyObject target, double? value)
    {
      target.SetValue(PrimaryOpacityProperty, value);
    }

    public static double? GetSecondaryOpacity(DependencyObject target)
    {
      return (double?)target.GetValue(SecondaryOpacityProperty);
    }
    public static void SetSecondaryOpacity(DependencyObject target, double? value)
    {
      target.SetValue(SecondaryOpacityProperty, value);
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
        //case Image target:
        //  {
        //    var icon = GetIcon(sender);
        //    var primaryColor = GetPrimaryColor(sender);
        //    var secondaryColor = GetSecondaryColor(sender);
        //    var swapOpacity = GetSwapOpacity(sender);
        //    var primaryOpacity = GetPrimaryOpacity(sender);
        //    var secondaryOpacity = GetSecondaryOpacity(sender);

        //    if (icon == EFontAwesomeIcon.None)
        //    {
        //      target.Source = null;
        //    }
        //    else
        //    {
        //      target.Source = icon.CreateImageSource(primaryColor, secondaryColor, swapOpacity, primaryOpacity, secondaryOpacity);
        //    }
        //  }
        //  break;        
        case ContentControl target:
          {
            var icon = GetIcon(sender);
            var primaryColor = GetPrimaryColor(sender);
            var secondaryColor = GetSecondaryColor(sender);
            var swapOpacity = GetSwapOpacity(sender);
            var primaryOpacity = GetPrimaryOpacity(sender);
            var secondaryOpacity = GetSecondaryOpacity(sender);

            if (icon == EFontAwesomeIcon.None)
            {
              sender.SetValue(ContentControl.ContentProperty, null);
            }
            else
            {
              sender.SetValue(ContentControl.FontFamilyProperty, icon.GetFontFamily());
              sender.SetValue(ContentControl.ContentProperty, icon.GetUnicode());
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