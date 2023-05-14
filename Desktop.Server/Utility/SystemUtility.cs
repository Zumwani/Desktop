using System.Runtime.InteropServices;
using LibreHardwareMonitor.Hardware;
using Server.Models;

namespace Server.Utility;

static class SystemUtility
{

    static readonly Computer pc = new() { IsCpuEnabled = true, IsMemoryEnabled = true, IsGpuEnabled = true, IsBatteryEnabled = true };
    static readonly IHardware? cpu;
    static readonly IHardware? gpu;
    static readonly IHardware? battery;
    static readonly IHardware? ram;

    static readonly ISensor? cpuTemp;
    static readonly ISensor? cpuLoad;
    static readonly ISensor? cpuPower;

    static readonly ISensor? gpuTemp;
    static readonly ISensor? gpuLoad;
    static readonly ISensor? gpuMemoryLoad;
    static readonly ISensor? gpuPower;
    static readonly ISensor? gpuMemoryUsed;
    static readonly ISensor? gpuMemoryTotal;

    static readonly ISensor? batteryChargeLevel;
    static readonly ISensor? batteryRemainingTime;

    static readonly ISensor? ramUsed;

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

    static IHardware? Find(this Computer pc, HardwareType type) =>
        pc.Hardware.FirstOrDefault(h => h.HardwareType == type);

    static ISensor? Find(this IHardware? hardware, SensorType type, string? name = null) =>
        hardware?.Sensors.FirstOrDefault(s => s.SensorType == type && (name is null || s.Name == name));

    static SystemUtility()
    {

        pc.Open();

        try
        {

            cpu = pc.Find(HardwareType.Cpu);
            gpu = pc.Find(HardwareType.GpuNvidia) ?? pc.Find(HardwareType.GpuAmd);
            battery = pc.Find(HardwareType.Battery);
            ram = pc.Find(HardwareType.Memory);

            cpuTemp = cpu.Find(SensorType.Temperature, "CPU Package");
            cpuLoad = cpu.Find(SensorType.Load, "CPU Total");
            cpuPower = cpu.Find(SensorType.Power, "CPU Package");

            gpuTemp = cpu.Find(SensorType.Temperature, "GPU Core");
            gpuLoad = cpu.Find(SensorType.Load, "GPU Core");
            gpuMemoryLoad = cpu.Find(SensorType.Load, "GPU Core");
            gpuPower = cpu.Find(SensorType.Power, "GPU Power");
            gpuMemoryUsed = cpu.Find(SensorType.Data, "GPU Memory Used");
            gpuMemoryTotal = cpu.Find(SensorType.Data, "GPU Memory Total");

            batteryChargeLevel = battery.Find(SensorType.Level, "Charge Level");
            batteryRemainingTime = battery.Find(SensorType.TimeSpan, "Remaining Time (Estimated)");

            ramUsed = ram.Find(SensorType.Data, "Memory Used");

        }
        catch
        { throw new Exception("Could not initialize SystemUtility."); }

    }

    public static SystemInfo GetInfo()
    {

        cpu?.Update();
        gpu?.Update();
        ram?.Update();
        battery?.Update();

        return new SystemInfo()
        {

            CPUTemp = cpuTemp?.Value,
            CPULoad = cpuLoad?.Value,
            CPUPower = cpuPower?.Value,

            GPUTemp = gpuTemp?.Value,
            GPULoad = gpuLoad?.Value,
            GPUMemoryLoad = gpuMemoryLoad?.Value,
            GPUPower = gpuPower?.Value,
            GPUMemoryUsed = gpuMemoryUsed?.Value,
            GPUMemoryTotal = gpuMemoryTotal?.Value,

            BatteryChargeLevel = batteryChargeLevel?.Value,
            BatteryRemainingTime = batteryRemainingTime?.Value,

            RamUsed = ramUsed?.Value,
            RamTotal = TotalRam

        };

    }

    public static float? TotalRam =>
        GetPhysicallyInstalledSystemMemory(out var ram) ? ram / 1024 / 1024 : null;

}
