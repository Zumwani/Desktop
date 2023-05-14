using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Tracker : ViewModel
{

    public WindowConfig? Config { get; set; }

    public BluetoothBattery BluetoothBattery { get; } = new();

    public CPUTemp CPUTemp { get; } = new();
    public CPULoad CPULoad { get; } = new();
    public CPUPower CPUPower { get; } = new();

    public GPUTemp GPUTemp { get; } = new();
    public GPULoad GPULoad { get; } = new();
    public GPUMemoryLoad GPUMemoryLoad { get; } = new();
    public GPUMemoryUsed GPUMemoryUsed { get; } = new();
    public GPUMemoryTotal GPUMemoryTotal { get; } = new();

    public BatteryChargeLevel BatteryChargeLevel { get; } = new();
    public BatteryRemainingTime BatteryRemainingTime { get; } = new();
    public RamUsed RamUsed { get; } = new();
    public RamTotal RamTotal { get; } = new();

    public SystemMemory SystemMemory { get; } = new();
    public GPUMemory GPUMemory { get; } = new();

}
