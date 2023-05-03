using System;
using System.Windows.Media;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class IdleMode : ConfigModule, ISharedVisibilityConfig
{

    public bool IsEnabled { get; set; } = false;
    public TimeSpan DelayBeforeOpening { get; set; } = TimeSpan.FromMinutes(5);
    public bool DisableDelayInDebugMode { get; set; } = true;
    public Stretch WallpaperStretch { get; set; }
    public bool ShowNotifications { get; set; } = true;
    public bool ShowTime { get; set; } = true;
    public bool ShowDate { get; set; } = true;
    public bool ShowWeather { get; set; } = true;
    public bool ShowBluetoothDeviceBattery { get; set; } = true;
    public bool ShowCpuUsage { get; set; } = true;
    public bool ShowCpuTemperature { get; set; } = true;
    public bool ShowMemory { get; set; } = true;
}
