using PostSharp.Patterns.Model;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

public abstract class SettingsPage
{
    public abstract string Title { get; }
    public abstract SymbolRegular Icon { get; }
}

[NotifyPropertyChanged]
public class SettingsWindow
{

    public bool IsOpen { get; set; } = true;

    public SettingsPage[] Pages { get; } = new SettingsPage[]
    {
        new General(),
        new Files(),
        new Notifications(),
        new Notes(),
        new DateAndTime(),
        new Weather(),
        new SystemInfo(),
    };

    public SettingsPage SelectedPage { get; set; }

    public RelayCommand<SettingsPage> OpenPageCommand { get; }

    public SettingsWindow()
    {
        OpenPageCommand = new(page => SelectedPage = page!);
        SelectedPage = Pages[0];
    }

}
