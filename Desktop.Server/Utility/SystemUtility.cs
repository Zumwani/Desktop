using System.Runtime.InteropServices;
using LibreHardwareMonitor.Hardware;
using Server.Models;

namespace Server.Utility;

static class SystemUtility
{

    #region Hardware / sensors

    static readonly Computer pc = new() { IsBatteryEnabled = true, IsCpuEnabled = true, IsGpuEnabled = true, IsMemoryEnabled = true, IsNetworkEnabled = true };
    static readonly IHardware? cpu;
    static readonly IHardware? gpu;
    static readonly IHardware? battery;
    static readonly IHardware? ram;
    static readonly IHardware? network;

    static readonly ISensor? batteryChargeLevel;
    static readonly ISensor? batteryRemainingTime;

    static readonly ISensor? cpuTemp;
    static readonly ISensor? cpuLoad;
    static readonly ISensor? cpuPower;
    static readonly ISensor? ramUsed;

    static readonly ISensor? gpuTemp;
    static readonly ISensor? gpuLoad;
    static readonly ISensor? gpuPower;
    static readonly ISensor? gpuMemoryUsed;
    static readonly ISensor? gpuMemoryTotal;

    static readonly ISensor? networkLoad;
    static readonly ISensor? networkDataUploaded;
    static readonly ISensor? networkDataDownloaded;
    static readonly ISensor? networkThroughputUpload;
    static readonly ISensor? networkThroughputDownload;

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

    public static float? TotalRam =>
        GetPhysicallyInstalledSystemMemory(out var ram) ? ram / 1024 / 1024 : null;

    static IHardware? Find(this Computer pc, HardwareType type) =>
        pc.Hardware.FirstOrDefault(h => h.HardwareType == type);

    static ISensor? Find(this IHardware? hardware, SensorType type, string? name = null) =>
        hardware?.Sensors.FirstOrDefault(s => s.SensorType == type && (name is null || s.Name == name));

    #endregion
    #region Initialize

    static SystemUtility()
    {

        pc.Open();

        try
        {

            cpu = pc.Find(HardwareType.Cpu);
            gpu = pc.Find(HardwareType.GpuNvidia) ?? pc.Find(HardwareType.GpuAmd);
            battery = pc.Find(HardwareType.Battery);
            ram = pc.Find(HardwareType.Memory);
            network = pc.Find(HardwareType.Network);
            network = pc.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Network && h.Name == "Ethernet");

            batteryChargeLevel = battery.Find(SensorType.Level, "Charge Level");
            batteryRemainingTime = battery.Find(SensorType.TimeSpan, "Remaining Time (Estimated)");

            cpuTemp = cpu.Find(SensorType.Temperature, "CPU Package");
            cpuLoad = cpu.Find(SensorType.Load, "CPU Total");
            cpuPower = cpu.Find(SensorType.Power, "CPU Package");
            ramUsed = ram.Find(SensorType.Data, "Memory Used");

            gpuTemp = gpu.Find(SensorType.Temperature, "GPU Core");
            gpuLoad = gpu.Find(SensorType.Load, "GPU Core");
            gpuPower = gpu.Find(SensorType.Power, "GPU Package");
            gpuMemoryUsed = gpu.Find(SensorType.Data, "GPU Memory Used");
            gpuMemoryTotal = gpu.Find(SensorType.Data, "GPU Memory Total");

            networkLoad = network.Find(SensorType.Load, "Network Utilization");
            networkDataUploaded = network.Find(SensorType.Data, "Data Uploaded");
            networkDataDownloaded = network.Find(SensorType.Data, "Data Downloaded");
            networkThroughputUpload = network.Find(SensorType.Throughput, "Upload Speed");
            networkThroughputDownload = network.Find(SensorType.Throughput, "Download Speed");

        }
        catch
        { throw new Exception("Could not initialize SystemUtility."); }

    }

    #endregion
    #region Get info

    public static SystemInfo GetInfo()
    {

        cpu?.Update();
        gpu?.Update();
        ram?.Update();
        battery?.Update();
        network?.Update();

        return new SystemInfo()
        {

            BatteryChargeLevel = batteryChargeLevel?.ValueToString(),
            BatteryRemainingTime = batteryRemainingTime?.ValueToString(),

            CPUTemp = cpuTemp?.ValueToString(),
            CPULoad = cpuLoad?.ValueToString(),
            CPUPower = cpuPower?.ValueToString(),
            RamUsed = ramUsed?.ValueToString(),
            RamTotal = string.Format("{0:F0} GB", TotalRam),

            GPUTemp = gpuTemp?.ValueToString(),
            GPULoad = gpuLoad?.ValueToString(),
            GPUPower = gpuPower?.ValueToString(),
            GPUMemoryUsed = gpuMemoryUsed?.ValueToString(),
            GPUMemoryTotal = gpuMemoryTotal?.ValueToString(),

            NetworkLoad = networkLoad?.ValueToString(),
            NetworkDataUploaded = networkDataUploaded?.ValueToString(),
            NetworkDataDownloaded = networkDataDownloaded?.ValueToString(),
            NetworkThroughputUpload = networkThroughputUpload?.ValueToString(),
            NetworkThroughputDownload = networkThroughputDownload?.ValueToString(),

        };

    }

    static string? ValueToString(this ISensor sensor)
    {

        var value = sensor.Value;

        if (!value.HasValue)
            return null;

        const int _1MB = 1048576;

        return sensor.SensorType switch
        {
            SensorType.Throughput => value < _1MB ? $"{value / 1024:F1} KB/s" : $"{value / _1MB:F1} MB/s",
            SensorType.TimeSpan => string.Format(sensor.GetFormat(), TimeSpan.FromSeconds(value.Value)),
            _ => string.Format(sensor.GetFormat(), value)
        };

    }

    static string GetFormat(this ISensor sensor) =>
        sensor.SensorType switch
        {
            SensorType.Voltage => "{0:F3} V",
            SensorType.Current => "{0:F3} A",
            SensorType.Clock => "{0:F1} MHz",
            SensorType.Load => "{0:F0} %",
            SensorType.Temperature => "{0:F0} °C",
            SensorType.Fan => "{0:F0} RPM",
            SensorType.Flow => "{0:F1} L/h",
            SensorType.Control => "{0:F1} %",
            SensorType.Level => "{0:F0} %",
            SensorType.Power => "{0:F0} W",
            SensorType.Data => "{0:F1} GB",
            SensorType.SmallData => "{0:F1} MB",
            SensorType.Factor => "{0:F3}",
            SensorType.Frequency => "{0:F1} Hz",
            SensorType.Throughput => "{0:F1} B/s",
            SensorType.TimeSpan => "{0:g}",
            SensorType.Energy => "{0:F0} mWh",
            SensorType.Noise => "{0:F0} dBA",
            _ => "",
        };

    #endregion

}
