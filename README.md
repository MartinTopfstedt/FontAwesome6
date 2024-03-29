[![Build Status](https://dev.azure.com/codinion/FontAwesome/_apis/build/status/MartinTopfstedt.FontAwesome6?branchName=master)](https://dev.azure.com/codinion/FontAwesome/_build/latest?definitionId=20&branchName=master)

# FontAwesome 6

WPF (.Net and .Net Core), UWP and WinUI controls for the web's most popular icon set and toolkit.

Font-Awesome Version: 6.2.0

## Breaking Changes

The Package is now separated into two packages for SVG and Fonts. Therefore the namespace needed to be separate as well. Which means the old namespace `http://schemas.fontawesome.com/icons` has been split into `http://schemas.fontawesome.com/icons/svg` and `http://schemas.fontawesome.com/icons/fonts`.

## FontAwesome6 (Free)

### FontAwesome6.Svg

#### Installation

Install the FontAwesome6.Svg package: `Install-Package FontAwesome6.Svg`
Or
Install the FontAwesome6.Svg.WinUI package: `Install-Package FontAwesome6.Svg.WinUI`

#### Usage

##### Controls

Control | .Net Framework & .Net | WinUI | UWP
------- | ---- | ----- | ----
SvgAwesome | :white_check_mark: | :white_check_mark: | :white_check_mark: |
ImageAwesome | :white_check_mark: | :x: | :x: |
Attached Properties | :white_check_mark: | :x: | :x: |

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/svg">        
    <fa:ImageAwesome Icon="Solid_Flag" />
    <fa:SvgAwesome Icon="Solid_Flag" />
</Window>        
```

See example projects for advanced usage.

##### Attached Properties (.Net Framework & .Net)

Available for following controls:

- Image
- Viewbox
- all controls which inherit ContentControl, e.x. Button.

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/svg/properties">        
    <Button fa:FontAwesome.Icon="Solid_Flags"/>
    <Image fa:FontAwesome.Icon="Solid_Flags"/>
</Window>        
```

See example projects for advanced usage.

##### Converters  (.Net Framework & .Net)

Available converters:

- DrawingConverter
- IconNameConverter
- ImageSourceConverter
- StyleConverter
- VisibilityConverter

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/svg">   
    <Window.Resources>
      <fa:ImageSourceConverter x:Key="ImageSourceConverter"/>      
    </Window.Resources>         
    <Image Source="{Binding SelectedIcon, Converter={StaticResource ImageSourceConverter}}"/>
</Window>        
```

See example projects for advanced usage.

### FontAwesome6.Fonts

#### Installation

Install the FontAwesome6.Fonts package: `Install-Package FontAwesome6.Fonts`
Or
Install the FontAwesome6.Fonts.WinUI package: `Install-Package FontAwesome6.Fonts.WinUI`

#### Usage

##### Controls

Control | .Net Framework & .Net | WinUI | UWP
------- | ---- | ----- | ----
FontAwesome | :white_check_mark: | :white_check_mark: | :white_check_mark: |
ImageAwesome | :white_check_mark: | :x: | :x: |
Attached Properties | :white_check_mark: | :x: | :x: |

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/fonts">        
    <fa:ImageAwesome Icon="Solid_Flag" />
    <fa:FontAwesome Icon="Solid_Flag" />
</Window>        
```

See example projects for advanced usage.

##### Attached Properties (.Net Framework & .Net)

Available for following controls:

- Image
- all controls which inherit ContentControl, e.x. Button.

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/fonts/properties">        
    <Button fa:FontAwesome.Icon="Soild_Flags"/>
    <Image fa:FontAwesome.Icon="Soild_Flags"/>
</Window>        
```

See example projects for advanced usage.

##### Converters (.Net Framework & .Net)

Available converters:

- DrawingConverter
- IconNameConverter
- ImageSourceConverter
- StyleConverter
- VisibilityConverter

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/fonts">   
    <Window.Resources>
      <fa:ImageSourceConverter x:Key="ImageSourceConverter"/>      
    </Window.Resources>         
    <Image Source="{Binding SelectedIcon, Converter={StaticResource ImageSourceConverter}}"/>
</Window>        
```

See example projects for advanced usage.

## FontAwesome6.Pro

### FontAwesome6.Pro.Svg

>The FontAwesome6.Pro.Svg NuGet package does not include any icon svg data. You need to provide the FontAwesome6 Pro svg data by yourself. (see Installation)

#### Installation

1. Install the FontAwesome6.Pro.Svg package: `Install-Package FontAwesome6.Pro.Svg`
   Or
   Install the FontAwesome6.Pro.Svg.WinUI package: `Install-Package FontAwesome6.Pro.Svg.WinUI`

2. Download the Desktop Version of FontAwesome6 from the website: https://fontawesome.com/download

3. Extract the FontAwesome6 into "Font-Awesome-Pro" and execute the `FontAwesome6.Generator.exe`.

   1. FontAwesome Version: specify the version for the generation. e.x. 6.0.0-beta3

   2. Activate the Pro Version check box

   3. Source Dictionary: not needed

   4. FontAwesome Svg Directory: the directory of all FontAwesome Svg files. e.x. Font-Awesome-Pro/svgs

   5. Output Directory: the directory where the svg files get saved to.

4. The generated `FontAwesome6Svg.json` should be integrated into your project as content or embedded resource.

5. Proceed with the setup

#### Setup

### FontAwesome6.Pro.Fonts

>The FontAwesome6.Pro.Fonts NuGet package does not include any font files. You need to provide the FontAwesome6 Pro ttf files by yourself. (see Installation)

#### Installation

1. Install the FontAwesome6.Pro.Fonts package: `Install-Package FontAwesome6.Pro.Fonts`
   Or
   Install the FontAwesome6.Pro.Fonts.WInUI package: `Install-Package FontAwesome6.Pro.Fonts.WinUI`

2. Download the Web Version of FontAwesome6 from the website: https://fontawesome.com/download

3. Include all needed FontAwesome6 Style ttf files(ex: *fa-regular-400*) inside you project as "Content" and "Copy always".

4. Proceed with the setup

#### Setup

##### Load the font files from the file system

The example below shows how to load all font files(with their default names) from the "Fonts" directory, which is located besides the executing assembly location.

```csharp
public partial class App : Application
{
  public App()
  {
    var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    FontAwesome6.Fonts.FontAwesomeFonts.LoadAllStyles(Path.Combine(directory, "Fonts") + "/");      
  }
}
```

##### Load the font files from resource

>:warning: *(.Net Framework & .Net only)* Using the fonts from resources will create a memory leak. To prevent it load the font from the file system instead.

The example below shows how to load all font files (with their default names) as resource. The files are located inside the solution in the "Fonts" directory and have "Resource" as Content.

###### .Net Framework & .Net

```csharp
public partial class App : Application
{
  public App()
  {
    FontAwesome6.Fonts.FontAwesomeFonts.LoadAllStyles(new Uri("pack://application:,,,/Fonts/"));      
  }
}
```

###### WInUI & UWP

```csharp
public sealed partial class MainPage : Page
{
  public MainPage()
  {
    Fonts.FontAwesomeFonts.LoadFonts(new Uri("ms-appx:///Fonts/"));      
  }
}
```

### Duotone

#### SVG

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/svg">        
    <fa:ImageAwesome Icon="Duotone_Flag" 
                     PrimaryColor="Black" 
                     SecondaryColor="Green" 
                     PrimaryOpacity="1" 
                     SecondaryOpacity="0.4"
                     SwapOpacity="false"/>
</Window>        
```

#### Fonts

```xaml
<Window x:Class="FontAwesome6.Example.WPF.MainWindow" 
        xmlns:fa="http://schemas.fontawesome.com/icons/fonts">        
    <fa:ImageAwesome Icon="Duotone_Flag" 
                     PrimaryColor="Black" 
                     SecondaryColor="Green" 
                     PrimaryOpacity="1" 
                     SecondaryOpacity="0.4"
                     SwapOpacity="false"/>
</Window>        
```
### Load Icons inside Designer

Add a `DesignTimeResource.xaml` inside the `Properties` directory and a new UserControl anywhere(e.x. `DesignTimeFontAwesome.xaml`).
Then add the UserControl to the ResourceDirectory.
```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"     
    xmlns:local="clr-namespace:FontAwesome6.Example.WPF">
    <local:DesignTimeFontAwesome x:Key="NotNeeded"></local:DesignTimeFontAwesome>
</ResourceDictionary>
```
After that, you need to modify the `.csproj` file and change the entry of `DesignTimeResource.xaml` to the following:
```
<Page Include="Properties\DesignTimeResources.xaml" Condition="'$(DesignTime)'=='true' OR ('$(SolutionPath)'!='' AND Exists('$(SolutionPath)') AND '$(BuildingInsideVisualStudio)'!='true' AND '$(BuildingInsideExpressionBlend)'!='true')">
  <Generator>MSBuild:Compile</Generator>
  <SubType>Designer</SubType>
  <ContainsDesignTimeResources>true</ContainsDesignTimeResources>
</Page>
```
Create a new UserControl called `DesignTimeFontAwesome.xaml` and add the icon / font loading inside the constructor.
```csharp
using System.Windows.Controls;
namespace FontAwesome6.Example.WPF
{
    public partial class DesignTimeFontAwesome : UserControl
    {
        public DesignTimeFontAwesome()
        {
            InitializeComponent();
            Svg.FontAwesomeSvg.LoadFromResource("FontAwesome6.Example.WPF.Svg.FontAwesomeSvg.all.json", typeof(App).Assembly);
        }
    }
}
```