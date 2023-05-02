using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;
using Common.Settings.Types;
using Desktop.Models;
using Desktop.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.Settings;

public class Notes : CollectionSetting<Note, Notes>
{ }

public class BluetoothDevice : Setting<string, BluetoothDevice>
{ }

public class Wallpaper : Setting<string, Wallpaper>
{ }

public class WallpaperStretch : Setting<Stretch, WallpaperStretch>
{ }

public class WallpaperOffset : Setting<Thickness, WallpaperOffset>
{ }

public class Left : Setting<double, Left>
{
    public override double DefaultValue => (SystemParameters.FullPrimaryScreenWidth / 2) - 400;
}

public class Top : Setting<double, Top>
{
    public override double DefaultValue => (SystemParameters.FullPrimaryScreenHeight / 2) - 300;
}

public class Width : Setting<double, Width>
{
    public override double DefaultValue => 800;
}

public class Height : Setting<double, Height>
{
    public override double DefaultValue => 600;
}

public class ShowHiddenNotes : Setting<bool, ShowHiddenNotes>
{ }

public class Weather : Setting<Weather.Config, Weather>
{

    public override Config? DefaultValue => new();

    [NotifyPropertyChanged]
    public class Config
    {
        public string? SearchLocation { get; set; }
        public string? ApiKey { get; set; }
        public WeatherUnit Unit { get; set; }
        [JsonIgnore, SafeForDependencyAnalysis] public string Units => Unit.ToString().ToLower();
    }

}

public class CalendarUri : Setting<string, CalendarUri>
{
    public override string? DefaultValue => "https://calendar.google.com";
}