using System.Diagnostics;
using Desktop.Config;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class IdleWindow : IntervalViewModel
{

    public Config.IdleMode Config { get; } = ConfigManager.IdleMode;
    public Config.General GeneralConfig { get; } = ConfigManager.General;

    public Notifications Notifications { get; } = new();
    public Time Time { get; } = new();
    public Wallpaper Wallpaper { get; } = new();
    public Tracker Tracker { get; } = new();
    public Date Date { get; } = new();
    public Weather Weather { get; } = new();

    public bool IsIdle { get; set; }
    public bool IsOpen { get; set; }

    public IdleWindow()
    {
        Notifications.Config = Config;
        Tracker.Config = Config;
        Wallpaper.Config = Config;
    }

    public override void Update()
    {
        if (Debugger.IsAttached && Config.DisableDelayInDebugMode)
            IsIdle = true;
        else
            IsIdle = Config.IsEnabled && IdleUtility.IsIdle(Config.DelayBeforeOpening);
    }

}
