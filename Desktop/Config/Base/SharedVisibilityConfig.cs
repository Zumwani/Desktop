namespace Desktop.Config;

public interface ISharedVisibilityConfig
{

    public bool ShowNotifications { get; set; }
    public bool ShowTime { get; set; }
    public bool ShowDate { get; set; }
    public bool ShowWeather { get; set; }
    public bool ShowBluetoothDeviceBattery { get; set; }
    public bool ShowCpuUsage { get; set; }
    public bool ShowCpuTemperature { get; set; }
    public bool ShowMemory { get; set; }

}
