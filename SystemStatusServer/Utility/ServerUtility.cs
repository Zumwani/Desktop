using Server.Models;

namespace Server.Utility;

static class ServerUtility
{

    public static DateTime StartTime { get; private set; }
    public static int ParentProcess { get; set; }

    static ServerUtility() =>
        StartTime = DateTime.Now;

    public static ServerInfo GetInfo() =>
        new() { Uptime = DateTime.Now - StartTime, ParentProcess = ParentProcess };

}
