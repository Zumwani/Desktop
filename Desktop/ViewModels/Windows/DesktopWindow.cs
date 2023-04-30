using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class DesktopWindow
{

    public Files Files { get; } = new();
    public Notifications Notifications { get; } = new();
    public Notes Notes { get; } = new();
    public Date Date { get; } = new();
    public Time Time { get; } = new();
    public Wallpaper Wallpaper { get; } = new();
    public SystemStatus SystemStatus { get; } = new();
    public Weather Weather { get; } = new();

    public bool IsOpen { get; set; }
    public bool IsLoaded { get; set; }

}
