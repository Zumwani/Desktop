using Common.Utility;
using Desktop.ViewModels.SettingPages;

namespace Desktop.Commands;

public class ConfigureDateAndTime : Command
{
    public override void Execute() => SettingsWindow.Open<DateAndTime>();
}
