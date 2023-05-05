using Desktop.Config;
using Desktop.Utility;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

public abstract class SettingsPage : ViewModel
{
    public abstract string Title { get; }
    public abstract SymbolRegular Icon { get; }
}

public abstract class SettingsPage<T> : SettingsPage where T : ConfigModule
{

    public SettingsPage() =>
        ConfigUtility.Setup(this);

    public T Config { get; } = ConfigManager.GetModule<T>();

}

[NotifyPropertyChanged]
public class SettingsWindow
{

    public bool IsOpen { get; set; } = true;

    public SettingsPage[] Pages { get; } = new SettingsPage[]
    {
        new General(),
        new IdleMode(),
        new Files(),
        new Notifications(),
        new DateAndTime(),
        new Weather(),
        new Tracker(),
    };

    public SettingsPage SelectedPage { get; set; }

    public RelayCommand<SettingsPage> OpenPageCommand { get; }

    public SettingsWindow()
    {
        OpenPageCommand = new(page => SelectedPage = page!);
        SelectedPage = Pages[0];
    }

}
