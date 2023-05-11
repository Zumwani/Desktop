using System.ComponentModel;
using System.Linq;
using System.Windows;
using Desktop.ViewModels.SettingPages;
using Files = Desktop.ViewModels.SettingPages.Files;
using Notifications = Desktop.ViewModels.SettingPages.Notifications;
using Tracker = Desktop.ViewModels.SettingPages.Tracker;
using Weather = Desktop.ViewModels.SettingPages.Weather;

namespace Desktop;

public partial class SettingsWindow : Window
{

    public SettingsWindow()
    {
        InitializeComponent();
        Config.ConfigManager.General.PropertyChanged += General_PropertyChanged;
    }

    void General_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Config.ConfigManager.General.IsEditMode))
        {
            //Topmost = Config.ConfigManager.General.IsEditMode;
            Owner = App.Current.MainWindow;
        }
    }

    public ViewModels.SettingPages.SettingsWindow View { get; } = new();

    static SettingsWindow? window;

    public static RelayCommand OpenGeneralCommand { get; } = new(Open<General>);
    public static RelayCommand OpenDateTimeCommand { get; } = new(Open<DateAndTime>);
    public static RelayCommand OpenIdleModeCommand { get; } = new(Open<IdleMode>);
    public static RelayCommand OpenFilesCommand { get; } = new(Open<Files>);
    public static RelayCommand OpenNotificationCommand { get; } = new(Open<Notifications>);
    public static RelayCommand OpenTrackerCommand { get; } = new(Open<Tracker>);
    public static RelayCommand OpenWeatherCommand { get; } = new(Open<Weather>);

    public static void Open() => Open<General>();

    public static void Open<T>() where T : SettingsPage
    {

        if (window is not null && window.IsLoaded)
            _ = window.Activate();
        else
        {
            window = new();
            window.Show();
        }

        window.View.SelectedPage = window.View.Pages.FirstOrDefault(p => p.GetType() == typeof(T)) ?? window.View.Pages[0];

    }

}
