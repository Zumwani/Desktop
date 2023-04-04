namespace Server.Models;

public struct ServerInfo
{
    public int ParentProcess { get; set; }
    public TimeSpan Uptime { get; set; }
}
