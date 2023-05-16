using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Tracker : ViewModel
{

    public WindowConfig? Config { get; set; }

    public BluetoothBattery BluetoothBattery { get; } = new();

    public BatteryChargeLevel BatteryChargeLevel { get; } = new();
    public BatteryRemainingTime BatteryRemainingTime { get; } = new();

    public CPUTemp CPUTemp { get; } = new();
    public CPULoad CPULoad { get; } = new();
    public CPUPower CPUPower { get; } = new();
    public SystemMemory SystemMemory { get; } = new();

    public GPUTemp GPUTemp { get; } = new();
    public GPUPower GPUPower { get; } = new();
    public GPULoad GPULoad { get; } = new();
    public GPUMemory GPUMemory { get; } = new();

    public NetworkLoad NetworkLoad { get; } = new();
    public NetworkDataUploaded NetworkDataUploaded { get; } = new();
    public NetworkDataDownloaded NetworkDataDownloaded { get; } = new();
    public NetworkThroughputUpload NetworkThroughputUpload { get; } = new();
    public NetworkThroughputDownload NetworkThroughputDownload { get; } = new();

}
