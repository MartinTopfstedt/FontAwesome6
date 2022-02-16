using FontAwesome6.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace FontAwesome6.Example.WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            AllIcons = Enum.GetValues(typeof(EFontAwesomeIcon)).Cast<EFontAwesomeIcon>()
                        .OrderBy(i => i.GetStyle()).ThenBy(i => i.GetIconName()).ToList();
            AllIcons.Remove(EFontAwesomeIcon.None);

            SelectedIcon = AllIcons.First();

            FlipOrientations = Enum.GetValues(typeof(EFlipOrientation)).Cast<EFlipOrientation>().ToList();
            SpinDuration = 5;
            PulseDuration = 5;

            FontSize = 30;
            Rotation = 0;

            Visibilities = Enum.GetValues(typeof(Visibility)).Cast<Visibility>().ToList();
            Visibility = Visibility.Visible;

            // if you have a default icon color you can overwrite the defaults instead of setting the color for each icon
            //FontAwesomeDefaults.PrimaryColor = new SolidColorBrush(Colors.Red);
            //FontAwesomeDefaults.SecondaryColor = new SolidColorBrush(Colors.Green);

            PrimaryColor = Brushes.Black;
            SecondaryColor = Brushes.Black;
        }

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
        public double? PrimaryOpacity { get; set; }

        public double? SecondaryOpacity { get; set; }

        public bool SwapOpacity { get; set; } = false;


        public List<Visibility> Visibilities { get; set; } = new List<Visibility>();

        public List<EFlipOrientation> FlipOrientations { get; set; } = new List<EFlipOrientation>();
        public List<EFontAwesomeIcon> AllIcons { get; set; } = new List<EFontAwesomeIcon>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
