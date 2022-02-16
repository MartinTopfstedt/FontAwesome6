using System.Windows;
using System.Windows.Media;

namespace FontAwesome6.Fonts.AttachedProperties
{
    public static partial class FontAwesome
    {
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
        /// Identifies the FontAwesome.Fonts.SecondaryOpacity dependency property.
        /// </summary>
        public static readonly DependencyProperty SecondaryOpacityProperty =
            DependencyProperty.RegisterAttached("SecondaryOpacity", typeof(double?), typeof(FontAwesome), new PropertyMetadata(null, OnRenderingTriggered));

        /// <summary>
        /// Identifies the FontAwesome.Fonts.SwapOpacity dependency property.
        /// </summary>
        public static readonly DependencyProperty SwapOpacityProperty =
            DependencyProperty.RegisterAttached("SwapOpacity", typeof(bool?), typeof(FontAwesome), new PropertyMetadata(null, OnRenderingTriggered));


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
    }
}