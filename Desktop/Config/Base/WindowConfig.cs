﻿using System.Text.Json.Serialization;
using System.Windows.Media;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class WindowConfig : ConfigModule
{

    public bool ShowNotifications { get; set; }
    public bool ShowTime { get; set; }
    public bool ShowDate { get; set; }
    public bool ShowWeather { get; set; }
    public bool ShowBluetoothDeviceBattery { get; set; }
    public bool ShowCpuUsage { get; set; }
    public bool ShowCpuTemperature { get; set; }
    public bool ShowMemory { get; set; }
    public Stretch WallpaperStretch { get; set; }

    [JsonIgnore] public virtual bool IsIdle { get; }

}
