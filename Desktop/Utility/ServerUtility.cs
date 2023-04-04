using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Desktop.Utility;

namespace Desktop;

static class ServerUtility
{

    static bool isInitialized;
    public static async void Initialize()
    {

        if (isInitialized)
            return;
        isInitialized = true;

        await Task.Delay(1000);

        Start();
        App.Current.Exit += (s, e) => Stop();
        ActionUtility.Invoke(async () =>
        {
            if ((await Server.API.GetServerInfo())?.ParentProcess != Environment.ProcessId)
            {
                _ = NotificationUtility.Notify("Lost connection to server, restarting it...", TimeSpan.FromSeconds(2.5));
                Restart();
            }
        }, TimeSpan.FromSeconds(1));

    }

    public static void Start()
    {

        if (DesignerProperties.GetIsInDesignMode(new()))
            return;

        if (!GetServerProcess(out _))
        {
            _ = NotificationUtility.Notify("Attempting to start server...", TimeSpan.FromSeconds(2.5));
            _ = Process.Start(new ProcessStartInfo(Server.App.Path, Environment.ProcessId.ToString())
            {
                Verb = "runas",
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            });
        }

    }

    public static void Stop()
    {

        if (DesignerProperties.GetIsInDesignMode(new()))
            return;

        if (GetServerProcess(out var process))
        {
            process.Kill();
            _ = NotificationUtility.Notify("Stopping server...", TimeSpan.FromSeconds(2.5));
        }

    }

    public static void Restart()
    {
        Stop();
        Start();
    }

    static bool GetServerProcess([NotNullWhen(true)] out Process? server) =>
         (server = Process.GetProcessesByName("SystemStatusServer").FirstOrDefault(p => !p.HasExited)) is not null;

}