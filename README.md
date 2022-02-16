# FontAwesome6

WPF (.Net and .Net Core), UWP and WinUI controls for the web's most popular icon set and toolkit.

Font-Awesome Version: 6.0.0

## FontAwesome6 (Free)

### FontAwesome6.Svg

#### Installation

1. Install the FontAwesome6.Svg package: `Install-Package FontAwesome6.Svg`

2. Proceed with the setup

### FontAwesome6.Fonts

#### Installation

1. Install the FontAwesome6.Fonts package: `Install-Package FontAwesome6.Fonts`

2. Proceed with the setup

### Setup

## FontAwesome6.Pro

### FontAwesome6.Pro.Svg

>The FontAwesome6.Pro.Svg NuGet package does not include any icon svg data. You need to provide the FontAwesome6 Pro svg data by yourself. (see Installation)

#### Installation

1. Install the FontAwesome6.Pro.Svg package: `Install-Package FontAwesome6.Pro.Svg`

2. Download the Desktop Version of FontAwesome6 from the website: https://fontawesome.com/download

3. Extract the FontAwesome6 and execute the FontAwesome6.SvgGenerator.exe with the path to `metadata/icons.json` from the FontAwesome6 download

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

#### Setup (.Net Core or .Net Framework)

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

>:warning: Using the fonts from resources will create a memory leak. To prevent it load the font from the file system instead.

The example below shows how to load all font files(with their default names) as resource. The files are located inside the solution in the "Fonts" directory and have "Resource" as Content.

```csharp
public partial class App : Application
{
  public App()
  {
    Fonts.FontAwesomeFonts.LoadFonts(new Uri("pack://application:,,,/Fonts/"));      
  }
}
```

#### Setup (UWP)

