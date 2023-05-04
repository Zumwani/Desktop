using Desktop.Config;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class DesktopWindow
{

    public Config.General Config { get; } = ConfigManager.General;

    public Files Files { get; } = new();
    public Notifications Notifications { get; } = new();
    public Notes Notes { get; } = new();
    public Date Date { get; } = new();
    public Time Time { get; } = new();
    public Wallpaper Wallpaper { get; } = new();
    public SystemInfo SystemStatus { get; } = new();
    public Weather Weather { get; } = new();

    public bool IsOpen { get; set; }
    public bool IsLoaded { get; set; }

    public DesktopWindow() =>
        SystemStatus.Config = Config;

}
