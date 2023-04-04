using System.Runtime.InteropServices;
using LibreHardwareMonitor.Hardware;
using Server.Models;

namespace Server.Utility;

static class SystemUtility
{

    static readonly Computer pc = new() { IsCpuEnabled = true, IsMemoryEnabled = true };
    static readonly IHardware cpu;
    static readonly IHardware ram;
    static readonly ISensor cpuTemp;
    static readonly ISensor cpuUsage;
    static readonly ISensor ramUsage;

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

    static SystemUtility()
    {

        pc.Open();

        try
        {

            cpu = pc.Hardware.First(h => h.HardwareType == HardwareType.Cpu);
            cpuTemp = cpu.Sensors.First(s => s.SensorType == SensorType.Temperature && s.Name == "CPU Package");
            cpuUsage = cpu.Sensors.First(s => s.SensorType == SensorType.Load && s.Name == "CPU Total");

            ram = pc.Hardware.First(h => h.HardwareType == HardwareType.Memory);
            ramUsage = ram.Sensors.First(s => s.SensorType == SensorType.Data && s.Name == "Memory Used");

        }
        catch
        { throw new Exception("Could not initialize SystemUtility."); }

    }

    public static SystemInfo GetInfo()
    {

        cpu.Update();
        ram.Update();

        return new()
        {
            UsedMemory = ramUsage.Value,
            AvailableMemory = TotalRam,
            CPU = cpuUsage.Value,
            Temperature = cpuTemp.Value,
        };

    }

    public static float? TotalRam =>
        GetPhysicallyInstalledSystemMemory(out var ram) ? ram / 1024 / 1024 : null;

}
