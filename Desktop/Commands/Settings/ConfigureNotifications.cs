using Common.Utility;
using Desktop.ViewModels.SettingPages;

namespace Desktop.Commands;

public class ConfigureNotifications : Command
{
    public override void Execute() => SettingsWindow.Open<Notifications>();
}
