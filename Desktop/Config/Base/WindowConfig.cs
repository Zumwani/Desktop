using System.Text.Json.Serialization;
using System.Windows.Media;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class WindowConfig : ConfigModule
{

    public Stretch WallpaperStretch { get; set; }

    public bool ShowNotifications { get; set; }
    public bool ShowTime { get; set; }
    public bool ShowDate { get; set; }
    public bool ShowWeather { get; set; }

    public bool ShowBluetoothDeviceBattery { get; set; }

    public bool ShowBatteryChargeLevel { get; set; }
    public bool ShowBatteryRemainingTime { get; set; }

    public bool ShowCPULoad { get; set; }
    public bool ShowCPUPower { get; set; }
    public bool ShowCPUTemp { get; set; }
    public bool ShowSystemMemory { get; set; }

    public bool ShowGPULoad { get; set; }
    public bool ShowGPUPower { get; set; }
    public bool ShowGPUTemp { get; set; }
    public bool ShowGPUMemory { get; set; }

    public bool ShowNetworkLoad { get; set; }
    public bool ShowNetworkUploaded { get; set; }
    public bool ShowNetworkDownloaded { get; set; }
    public bool ShowNetworkThroughputUpload { get; set; }
    public bool ShowNetworkThroughputDownload { get; set; }

    [JsonIgnore] public virtual bool IsIdle { get; }

}
