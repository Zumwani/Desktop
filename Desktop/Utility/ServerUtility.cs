using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Desktop;

static class ServerUtility
{

    static bool isInitialized;
    public static void Initialize()
    {

        if (isInitialized)
            return;
        isInitialized = true;

        Restart();

    }

    public static void Restart()
    {

        if (DesignerProperties.GetIsInDesignMode(new()))
            return;

        while (GetServerProcess(out var process))
            process.Kill();

        _ = NotificationUtility.Notify("Server not running, attempting to start restart...", TimeSpan.FromSeconds(2.5));
        ChildProcessTrackerUtility.AddProcess(Process.Start(new ProcessStartInfo(Server.App.Path)
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