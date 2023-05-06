using System.Diagnostics;
using System.Threading.Tasks;
using Desktop.Config;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public partial class DesktopWindow : ViewModel
{

    public General Config { get; } = ConfigManager.General;

    public Files Files { get; } = new();
    public Notifications Notifications { get; } = new();
    public Notes Notes { get; } = new();
    public Wallpaper Wallpaper { get; } = new();
    public Tracker Tracker { get; } = new();
    public DateTimeWeather DateTimeWeather { get; } = new();

    public bool IsOpen { get; set; } = true;
    public bool IsLoaded { get; set; }

    public RelayCommand QuitCommand { get; }

    public DesktopWindow()
    {

        Tracker.Config = Config;
        DateTimeWeather.Config = Config;
        Wallpaper.Config = Config;
        Notifications.ShowTestButton = Debugger.IsAttached;

        QuitCommand = new(async () =>
        {
            IsOpen = false;
            await Task.Delay(1000);
            App.Current.Shutdown();
        });

    }

}
