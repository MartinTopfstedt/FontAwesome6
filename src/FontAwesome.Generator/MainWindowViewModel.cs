using FontAwesome.Generator.Shared;
using FontAwesome.Generator.Shared.GraphQl;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FontAwesome.Generator
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Version { get; set; }

        public bool IsPro { get; set; }

        public string SourceDirectory { get; set; }

        public string FontAwesomeSvgDirectory { get; set; }

        public string SvgOutputDirectory { get; set; }

        public string SvgOutputFileNamePrefix { get; set; }

        public ICommand GenerateCommand { get; set; }

        public ICommand BrowseFontAwesomeSvgDirectoryCommand { get; set; }

        public ICommand BrowseSvgOutputDirectoryCommand { get; set; }

        public ICommand BrowseSourceDirectoryCommand { get; set; }

        public bool IsWindowsEnabled { get; set; } = true;

        public MainWindowViewModel()
        {
            Version = "6.0.0-beta1";

            var root = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", ".."));

            SourceDirectory = Path.Combine(root, "src");
            FontAwesomeSvgDirectory = Path.Combine(root, "Font-Awesome", "svgs");
            SvgOutputDirectory = Path.Combine(root, "Font-Awesome", "generated");
            SvgOutputFileNamePrefix = "FontAwesomeSvg";

            GenerateCommand = new RelayCommand(obj => GenerateExecuted(), obj => CanExecutGenerate());
            BrowseFontAwesomeSvgDirectoryCommand = new RelayCommand(obj => BrowseFontAwesomeSvgDirectoryExecuted());
            BrowseSvgOutputDirectoryCommand = new RelayCommand(obj => BrowseSvgOutputDirectoryExecuted());
            BrowseSourceDirectoryCommand = new RelayCommand(obj => BrowseSourceDirectoryExecuted());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool CanExecutGenerate()
        {
            if (!Directory.Exists(SourceDirectory) || !Directory.Exists(FontAwesomeSvgDirectory) || !Directory.Exists(SvgOutputDirectory))
            {
                return false;
            }
            return true;
        }

        private async void GenerateExecuted()
        {
            try
            {
                IsWindowsEnabled = false;
                var generator = new FontAwesomeGenerator();
                await generator.GenerateAllAsync(Version, !IsPro, SourceDirectory, FontAwesomeSvgDirectory, SvgOutputDirectory, SvgOutputFileNamePrefix ?? "FontAwesomeSvg").ConfigureAwait(false);
                MessageBox.Show("Generation finished.", "Generation");                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Generation: Error");
            }
            finally
            {
                IsWindowsEnabled = true;
            }
        }

        private void BrowseFontAwesomeSvgDirectoryExecuted()
        {

        }

        private void BrowseSvgOutputDirectoryExecuted()
        {

        }
        private void BrowseSourceDirectoryExecuted()
        {

        }
    }
}
