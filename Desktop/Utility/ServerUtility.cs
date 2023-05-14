using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Desktop;

static class ServerUtility
{

    const string ServerFileName = "Desktop.Server.exe";
    static string ServerPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServerFileName);

    public static void Restart() => RestartInternal(false);
    public static void TryRestart() => RestartInternal(true);

    static int autoRetryCount;
    static void RestartInternal(bool isAuto)
    {

        if (DesignerProperties.GetIsInDesignMode(new()))
            return;

        if (isAuto)
        {
            autoRetryCount += 1;
            if (autoRetryCount > 2)
                return;
        }
        else
            autoRetryCount = 0;

        while (GetServerProcess(out var process))
            process.Kill();

        if (File.Exists(ServerPath))
            ChildProcessTrackerUtility.AddProcess(Process.Start(
                new ProcessStartInfo(ServerPath)
                {
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }));

    }

    static bool GetServerProcess([NotNullWhen(true)] out Process? server) =>
         (server = Process.GetProcessesByName(typeof(Server.API).Assembly.GetName().Name).FirstOrDefault(p => !p.HasExited)) is not null;

}