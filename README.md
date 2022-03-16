[![Build Status](https://dev.azure.com/codinion/FontAwesome/_apis/build/status/MartinTopfstedt.FontAwesome6?branchName=master)](https://dev.azure.com/codinion/FontAwesome/_build/latest?definitionId=20&branchName=master)

# FontAwesome 6

WPF (.Net and .Net Core), UWP and WinUI controls for the web's most popular icon set and toolkit.

Font-Awesome Version: 6.0.0

## Breaking Changes

The Package is now separated into two separate packages for SVG and Fonts. Therefore the namespace needed to be separate as well. Which means the old namespace `http://schemas.fontawesome.com/icons` has been split into `http://schemas.fontawesome.com/icons/svg` and `http://schemas.fontawesome.com/icons/fonts`.

## FontAwesome6 (Free)

### FontAwesome6.Svg

#### Installation

Install the FontAwesome6.Svg package: `Install-Package FontAwesome6.Svg`

#### Usage

##### Controls

Control | .Net Framework & .Net | WinUI | UWP
------- | ---- | ----- | ----
SvgAwesome | :white_check_mark: | :white_check_mark: | :white_check_mark: |
ImageAwesome | :white_check_mark: | :x: | :x: |

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
    <Button fa:FontAwesome.Icon="Soild_Flags"/>
    <Image fa:FontAwesome.Icon="Soild_Flags"/>
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

#### Usage

##### Controls

Control | .Net Framework & .Net | WinUI | UWP
------- | ---- | ----- | ----
FontAwesome | :white_check_mark: | :white_check_mark: | :white_check_mark: |
ImageAwesome | :white_check_mark: | :x: | :x: |

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

2. Download the Desktop Version of FontAwesome6 from the website: https://fontawesome.com/download

3. Extract the FontAwesome6 into "Font-Awesome-Pro" and execute the `FontAwesome6.Generator.exe`.

   1. FontAwesome Version: specify the version for the generation. e.x. 6.0.0-beta3

   2. Activate the Pro Version check box

   3. Source Dictionary: 

   4. FontAwesome Svg Directory: the directory of all FontAwesome Svg files. e.x. Font-Awesome-Pro/svgs

   5. Output Directory: the directory where the svg files get saved to.

4. The generated `FontAwesome6Svg.json` should be integrated into your project as content or embedded resource.

5. Proceed with the setup

#### Setup

### FontAwesome6.Pro.Fonts

>The FontAwesome6.Pro.Fonts NuGet package does not include any font files. You need to provide the FontAwesome6 Pro otf files by yourself. (see Installation)

#### Installation

1. Install the FontAwesome6.Pro.Fonts package: `Install-Package FontAwesome6.Pro.Fonts`

2. Download the Desktop Version of FontAwesome6 from the website: https://fontawesome.com/download

3. Include all needed FontAwesome6 Style otf files(ex: *Font Awesome 6 Pro-Solid-900.otf*) inside you project as "Content" and "Copy always".

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
    Fonts.FontAwesomeFonts.LoadFontsFromDirectory(Path.Combine(directory, "Fonts") + "/");      
  }
}
```

##### Load the font files from resource

>:warning: *(.Net Framework & .Net only)* Using the fonts from resources will create a memory leak. To prevent it load the font from the file system instead.

The example below shows how to load all font files(with their default names) as resource. The files are located inside the solution in the "Fonts" directory and have "Resource" as Content.

###### .Net Framework & .Net

```csharp
public partial class App : Application
{
  public App()
  {
    Fonts.FontAwesomeFonts.LoadFonts(new Uri("pack://application:,,,/Fonts/"));      
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
