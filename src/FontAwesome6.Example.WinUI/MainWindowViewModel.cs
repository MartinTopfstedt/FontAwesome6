
using FontAwesome6.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System.Text.RegularExpressions;

namespace FontAwesome6.Example.WinUI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Visibility Visibility { get; set; }

        private EFontAwesomeIcon _selectedIcon;
        public EFontAwesomeIcon SelectedIcon
        {
            get => _selectedIcon;
            set
            {
                _selectedIcon = value;
                RaisePropertyChanged(nameof(SelectedIcon));
            }
        }

        private bool _spinIsEnabled;
        public bool SpinIsEnabled
        {
            get => _spinIsEnabled;
            set
            {
                _spinIsEnabled = value;
                RaisePropertyChanged(nameof(SpinIsEnabled));
            }
        }
        private double _spinDuration;
        public double SpinDuration
        {
            get => _spinDuration;
            set
            {
                _spinDuration = value;
                RaisePropertyChanged(nameof(SpinDuration));
            }
        }

        private bool _pulseIsEnabled;
        public bool PulseIsEnabled
        {
            get => _pulseIsEnabled;
            set
            {
                _pulseIsEnabled = value;
                RaisePropertyChanged(nameof(PulseIsEnabled));
            }
        }

        private double _pulseDuration;
        public double PulseDuration
        {
            get => _pulseDuration;
            set
            {
                _pulseDuration = value;
                RaisePropertyChanged(nameof(PulseDuration));
            }
        }

        private EFlipOrientation _flipOrientation;
        public EFlipOrientation FlipOrientation
        {
            get => _flipOrientation;
            set
            {
                _flipOrientation = value;
                RaisePropertyChanged(nameof(FlipOrientation));
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                RaisePropertyChanged(nameof(FontSize));
            }
        }

        private double _rotation;
        public double Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                RaisePropertyChanged(nameof(Rotation));
            }
        }

        private Brush _secondaryColor;
        public Brush SecondaryColor
        {
            get => _secondaryColor;
            set
            {
                _secondaryColor = value;
                RaisePropertyChanged(nameof(SecondaryColor));
            }
        }

        private Brush _primaryColor;
        public Brush PrimaryColor
        {
            get => _primaryColor;
            set
            {
                _primaryColor = value;
                RaisePropertyChanged(nameof(PrimaryColor));
            }
        }

        private double _primaryOpacity = 1;
        public double PrimaryOpacity
        {
            get => _primaryOpacity;
            set
            {
                _primaryOpacity = value;
                RaisePropertyChanged(nameof(PrimaryOpacity));
            }
        }

        private double _secondaryOpacity = .4;
        public double SecondaryOpacity
        {
            get => _secondaryOpacity;
            set
            {
                _secondaryOpacity = value;
                RaisePropertyChanged(nameof(SecondaryOpacity));
            }
        }

        private bool _swapOpacity;
        public bool SwapOpacity
        {
            get => _swapOpacity;
            set
            {
                _swapOpacity = value;
                RaisePropertyChanged(nameof(SwapOpacity));
            }
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                RaisePropertyChanged(nameof(FilterText));
                UpdateVisibleIcons();
            }
        }

        private IEnumerable<EFontAwesomeIcon> _visibleIcons;
        public IEnumerable<EFontAwesomeIcon> VisibleIcons
        {
            get => _visibleIcons;
            set
            {
                _visibleIcons = value;
                RaisePropertyChanged(nameof(VisibleIcons));
            }
        }

        public List<Visibility> Visibilities { get; set; } = new List<Visibility>();
        public List<EFlipOrientation> FlipOrientations { get; set; } = new List<EFlipOrientation>();
        public List<EFontAwesomeIcon> AllIcons { get; set; } = new List<EFontAwesomeIcon>();

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

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
