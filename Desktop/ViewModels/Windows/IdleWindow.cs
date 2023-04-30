using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class IdleWindow : IntervalViewModel
{

    public Notifications Notifications { get; } = new();
    public Date Date { get; } = new();
    public Time Time { get; } = new();
    public Wallpaper Wallpaper { get; } = new();
    public SystemStatus SystemStatus { get; } = new();
    public Weather Weather { get; } = new();

    public bool IsIdle { get; set; }
    public bool IsOpen { get; set; }

    public override void Update() =>
        IsIdle = IdleUtility.IsIdle();

}