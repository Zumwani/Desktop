using System.Linq;
using System.Windows;
using Desktop.ViewModels.SettingPages;

namespace Desktop;

public partial class SettingsWindow : Window
{

    public SettingsWindow() =>
        InitializeComponent();

    public ViewModels.SettingPages.SettingsWindow View { get; } = new();

    static SettingsWindow? window;
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
