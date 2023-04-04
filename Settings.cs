using System.Windows;
using System.Windows.Media;
using Common.Settings.Types;
using Desktop.Models;

namespace Desktop.Settings;

public class Notes : CollectionSetting<Note, Notes>
{ }

public class Notifications : CollectionSetting<Notification, Notifications>
{ }

public class BluetoothDevice : Setting<string, BluetoothDevice>
{ }

public class Wallpaper : Setting<string, Wallpaper>
{ }

public class WallpaperStretch : Setting<Stretch, WallpaperStretch>
{ }

public class WallpaperOffset : Setting<Thickness, WallpaperOffset>
{ }

//public class Bounds : Setting<Rect, Bounds>
//{

//    public override Rect DefaultValue =>
//        new(
//            x: (SystemParameters.FullPrimaryScreenWidth / 2) - 400,
//            y: (SystemParameters.FullPrimaryScreenHeight / 2) - 300,
//            width: 800,
//            height: 600);

//}

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