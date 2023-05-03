using Desktop.Config;

namespace Desktop.ViewModels;

public class SystemStatus
{

    public ISharedVisibilityConfig? Config { get; set; }

    public BluetoothBattery BluetoothBattery { get; } = new();
    public CpuUsage CpuUsage { get; } = new();
    public CpuTemperature CpuTemperature { get; } = new();
    public Memory Memory { get; } = new();

}
