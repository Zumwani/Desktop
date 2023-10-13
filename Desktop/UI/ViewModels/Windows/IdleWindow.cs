using System.Diagnostics;
using System.Threading.Tasks;
using Desktop.Config;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;
using ShellUtility.Screens;
using WindowsHook;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class IdleWindow : IntervalViewModel
{

    public static IdleWindow Instance { get; } = new();

    public IdleMode Config { get; } = ConfigManager.IdleMode;
    public General GeneralConfig { get; } = ConfigManager.General;

    public Notifications Notifications { get; } = new();
    public Time Time { get; } = new();
    public Wallpaper Wallpaper { get; } = new();
    public Tracker Tracker { get; } = new();
    public Date Date { get; } = new();
    public Weather Weather { get; } = new();

    public bool IsIdle { get; set; }
    public bool IsOpen { get; set; }

    private IdleWindow()
    {
        Notifications.Config = Config;
        Tracker.Config = Config;
        Wallpaper.Config = Config;
        Hook.GlobalEvents().MouseMove += (s, e) => Task.Run(() => { if (IsIdle) Update(); });
        Config.PropertyChanged += (s, e) => { if (IsIdle) Update(); };
    }

    public override void Update()
    {

        IsIdle =
            (Debugger.IsAttached && Config.DisableDelayInDebugMode) ||
            IsTVActive() ||
            (Config.IsEnabled && IdleUtility.IsIdle(Config.DelayBeforeOpening));

    }

    bool IsTVActive()
    {

        var handle = ShellUtility.Windows.Utility.WindowUtility.GetActiveWindow();
        return Screen.FromWindowHandle(handle, false)?.Index == 2;

    }

}
