using System.Windows.Media;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class General : ConfigModule, ISharedVisibilityConfig
{

    public bool Autostart { get; set; } = true;
    public bool DimScreenWhenNotActive { get; set; } = false;
    public bool ShowBorderAlongBottomOfPrimaryScreen { get; set; } = true;

    public string WallpaperUri { get; set; } = "";
    public Stretch WallpaperStretch { get; set; }

    public bool ShowFiles { get; set; } = true;
    public bool ShowNotes { get; set; } = true;
    public bool ShowNotifications { get; set; } = true;
    public bool ShowTime { get; set; } = true;
    public bool ShowDate { get; set; } = true;
    public bool ShowWeather { get; set; } = true;
    public bool ShowBluetoothDeviceBattery { get; set; } = true;
    public bool ShowCpuUsage { get; set; } = true;
    public bool ShowCpuTemperature { get; set; } = true;
    public bool ShowMemory { get; set; } = true;

}
