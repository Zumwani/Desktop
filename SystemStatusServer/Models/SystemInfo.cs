namespace Server.Models;

public class SystemInfo
{
    public double? CpuUsage { get; set; }
    public double? CpuTemperature { get; set; }
    public double? UsedMemory { get; set; }
    public double? AvailableMemory { get; set; }
}
