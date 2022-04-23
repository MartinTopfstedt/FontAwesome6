
using FontAwesome6.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace FontAwesome6.Example.UWP
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Visibility Visibility { get; set; }
        public EFontAwesomeIcon SelectedIcon { get; set; }

        public bool SpinIsEnabled { get; set; }
        public double SpinDuration { get; set; }
        public bool PulseIsEnabled { get; set; }
        public double PulseDuration { get; set; }
        public EFlipOrientation FlipOrientation { get; set; }
        public double FontSize { get; set; }
        public double Rotation { get; set; }

        public Brush PrimaryColor { get; set; }

        public Brush SecondaryColor { get; set; }
        public double PrimaryOpacity { get; set; }

        public double SecondaryOpacity { get; set; }

        public bool SwapOpacity { get; set; }

        public string FilterText { get; set; }

        public List<Visibility> Visibilities { get; set; } = new List<Visibility>();

        public List<EFlipOrientation> FlipOrientations { get; set; } = new List<EFlipOrientation>();
        public List<EFontAwesomeIcon> AllIcons { get; set; } = new List<EFontAwesomeIcon>();

        public IEnumerable<EFontAwesomeIcon> VisibleIcons { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            AllIcons = Enum.GetValues(typeof(EFontAwesomeIcon)).Cast<EFontAwesomeIcon>()
                        .OrderBy(i => i.GetStyle()).ThenBy(i => i.GetIconName()).ToList();
            AllIcons.Remove(EFontAwesomeIcon.None);

            VisibleIcons = AllIcons.ToList();
            SelectedIcon = VisibleIcons.First();

            FlipOrientations = Enum.GetValues(typeof(EFlipOrientation)).Cast<EFlipOrientation>().ToList();
            SpinDuration = 5;
            PulseDuration = 5;

            FontSize = 30;
            Rotation = 0;

            Visibilities = Enum.GetValues(typeof(Visibility)).Cast<Visibility>().ToList();
            Visibility = Visibility.Visible;

            // if you have a default icon color you can overwrite the defaults instead of setting the color for each icon
            //FontAwesomeDefaults.PrimaryColor = new SolidColorBrush(Windows.UI.Colors.Red);
            //FontAwesomeDefaults.SecondaryColor = new SolidColorBrush(Windows.UI.Colors.Green);

            PrimaryColor = Application.Current.Resources.ThemeDictionaries["ApplicationForegroundThemeBrush"] as SolidColorBrush;
            SecondaryColor = Application.Current.Resources.ThemeDictionaries["ApplicationForegroundThemeBrush"] as SolidColorBrush;

            PrimaryOpacity = 1;
            SecondaryOpacity = 0.4;

            SwapOpacity = false;
        }

        private void OnFilterTextChanged()
        {
            UpdateVisibleIcons();
        }

        private void UpdateVisibleIcons()
        {
            try
            {
                VisibleIcons = AllIcons.Where(icon => Regex.IsMatch(icon.GetIconName(), FilterText, RegexOptions.IgnoreCase));
            }
            catch
            {
                VisibleIcons = AllIcons;
            }
            SelectedIcon = VisibleIcons.FirstOrDefault();
        }
    }
}
