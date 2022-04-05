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

        public ICommand GenerateSourceFilesCommand { get; set; }

        public ICommand GenerateSvgFilesCommand { get; set; }

        public ICommand BrowseFontAwesomeSvgDirectoryCommand { get; set; }

        public ICommand BrowseSvgOutputDirectoryCommand { get; set; }

        public ICommand BrowseSourceDirectoryCommand { get; set; }

        public bool IsWindowsEnabled { get; set; } = true;

        public MainWindowViewModel()
        {
            Version = "6.1.1";

            var root = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", ".."));

            SourceDirectory = Path.Combine(root, "src");
            FontAwesomeSvgDirectory = Path.Combine(root, "Font-Awesome", "svgs");
            SvgOutputDirectory = Path.Combine(root, "generated", "Font-Awesome");
            SvgOutputFileNamePrefix = "FontAwesomeSvg";

            GenerateSourceFilesCommand = new RelayCommand(obj => GenerateSourceFilesExecuted(), obj => CanExecuteGenerateSourceFiles());
            GenerateSvgFilesCommand = new RelayCommand(obj => GenerateSvgFilesExecuted(), obj => CanExecuteGenerateSvgFiles());
            BrowseFontAwesomeSvgDirectoryCommand = new RelayCommand(obj => BrowseFontAwesomeSvgDirectoryExecuted());
            BrowseSvgOutputDirectoryCommand = new RelayCommand(obj => BrowseSvgOutputDirectoryExecuted());
            BrowseSourceDirectoryCommand = new RelayCommand(obj => BrowseSourceDirectoryExecuted());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool CanExecuteGenerateSourceFiles()
        {
            if (!Directory.Exists(SourceDirectory))
            {
                return false;
            }
            return true;
        }

        private bool CanExecuteGenerateSvgFiles()
        {
            if (!Directory.Exists(FontAwesomeSvgDirectory) || !Directory.Exists(SvgOutputDirectory))
            {
                return false;
            }
            return true;
        }

        private async void GenerateSourceFilesExecuted()
        {
            try
            {
                IsWindowsEnabled = false;
                var generator = new FontAwesomeGenerator();
                await generator.GenerateSourceFiles(Version, SourceDirectory).ConfigureAwait(false);
                MessageBox.Show("Source Files Generation finished.", "Generation");                
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

        private async void GenerateSvgFilesExecuted()
        {
            try
            {
                IsWindowsEnabled = false;
                var generator = new FontAwesomeGenerator();
                await generator.GenerateSvgFiles(Version, !IsPro, FontAwesomeSvgDirectory, SvgOutputDirectory, SvgOutputFileNamePrefix ?? "FontAwesomeSvg").ConfigureAwait(false);
                MessageBox.Show("SVG Files Generation finished.", "Generation");
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
