using System;
using System.Runtime.InteropServices;

namespace Desktop;

public static class IdleUtility
{

    static bool lastIsDisplayRequired;
    static DateTime lastDisplayRequiredChange;
    public static bool IsIdle(TimeSpan delay, bool checkIsDisplayRequired = true)
    {

        if (checkIsDisplayRequired)
        {

            var isDisplayRequired = IsDisplayRequired();
            if (lastIsDisplayRequired != isDisplayRequired)
                lastDisplayRequiredChange = DateTime.Now;

            lastIsDisplayRequired = isDisplayRequired;
            if (isDisplayRequired)
                return false;

            if (DateTime.Now - lastDisplayRequiredChange < delay)
                return false;

        }

        return GetIdleTimeInfo() >= delay.TotalMilliseconds;

    }

    #region Idle time

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

    #endregion
    #region Is display required

    struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    [DllImport("powrprof.dll", SetLastError = true)]
    static extern uint CallNtPowerInformation(int InformationLevel, IntPtr lpInputBuffer, uint nInputBufferSize, out uint lpOutputBuffer, uint nOutputBufferSize);

    [Flags]
    enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
    }

    static bool IsDisplayRequired()
    {

        _ = CallNtPowerInformation(
            16,
            (IntPtr)null,
            0,
            out var status,
            (uint)Marshal.SizeOf(typeof(ulong)));

        var state = (EXECUTION_STATE)status;

        return state.HasFlag(EXECUTION_STATE.ES_DISPLAY_REQUIRED);

    }

    #endregion

}