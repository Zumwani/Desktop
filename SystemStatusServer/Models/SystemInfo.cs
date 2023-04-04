namespace Server.Models;

public struct SystemInfo
{
    public double? CPU { get; set; }
    public double? Temperature { get; set; }
    public double? UsedMemory { get; set; }
    public double? AvailableMemory { get; set; }
}
