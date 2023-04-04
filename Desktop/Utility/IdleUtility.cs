using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Desktop;

public static class IdleUtility
{

    static readonly TimeSpan Delay = Debugger.IsAttached ? TimeSpan.FromSeconds(5) : TimeSpan.FromMinutes(5);

    public static bool IsIdle() =>
        IdleTimeDetector.GetIdleTimeInfo() >= Delay.TotalMilliseconds;

    static class IdleTimeDetector
    {

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public static int GetIdleTimeInfo()
        {

            var systemUptime = Environment.TickCount;
            var idleMilliseconds = 0;

            var lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                var lastInputTicks = (int)lastInputInfo.dwTime;
                idleMilliseconds = systemUptime - lastInputTicks;
            }

            return idleMilliseconds;

        }

    }

    struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

}