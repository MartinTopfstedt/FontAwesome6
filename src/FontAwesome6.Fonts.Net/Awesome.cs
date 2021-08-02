using FontAwesome6.Extensions;
using FontAwesome6.Fonts.Extensions;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesome6.Fonts
{
  /// <summary>
  /// Provides attached properties to set FontAwesome icons on controls.
  /// </summary>
  [Obsolete("Use FontAwesome type instead.")]
  public static class Awesome
  {
    /// <summary>
    /// Identifies the FontAwesome6.Fonts.Awesome.Icon attached dependency property.
    /// </summary>
    [Obsolete("Use FontAwesome.Icon instead.")]
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.RegisterAttached(
            "Icon",
            typeof(EFontAwesomeIcon),
            typeof(Awesome),
            new PropertyMetadata(EFontAwesomeIcon.None, OnIconChanged));

    /// <summary>
    /// Gets the icon of a ContentControl, expressed as a FontAwesome icon.
    /// </summary>
    /// <param name="target">The ContentControl subject of the query</param>
    /// <returns>FontAwesome icon found as icon</returns>
    public static EFontAwesomeIcon GetIcon(DependencyObject target)
    {
      return (EFontAwesomeIcon)target.GetValue(IconProperty);
    }
    /// <summary>
    /// Sets the icon of a ContentControl expressed as a FontAwesome icon. This will cause the content to be redrawn.
    /// </summary>
    /// <param name="target">The ContentControl where to set the icon</param>
    /// <param name="value">FontAwesome icon to set as icon</param>
    public static void SetIcon(DependencyObject target, EFontAwesomeIcon value)
    {
      target.SetValue(IconProperty, value);
    }

    private static void OnIconChanged(DependencyObject sender, DependencyPropertyChangedEventArgs evt)
    {
      // If target is not a ContenControl just ignore: Awesome.Icon property can only be set on a ContentControl element
      if (sender is ContentControl target)
      {
        var icon = (EFontAwesomeIcon)evt.NewValue;

        RenderFontAwesome(target, icon);
      }
    }

    private static void RenderFontAwesome(ContentControl sender, EFontAwesomeIcon icon)
    {
      sender.SetValue(ContentControl.FontFamilyProperty, icon.GetFontFamily());
      sender.SetValue(ContentControl.ContentProperty, icon.GetUnicode());
    }
  }
}
