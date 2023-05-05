using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Tracker : ViewModel
{

    public WindowConfig? Config { get; set; }

    public BluetoothBattery BluetoothBattery { get; } = new();
    public CpuUsage CpuUsage { get; } = new();
    public CpuTemperature CpuTemperature { get; } = new();
    public Memory Memory { get; } = new();

}
