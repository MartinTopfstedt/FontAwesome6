using FontAwesome6.Example.WinUI2.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace FontAwesome6.Example.WinUI2.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
