using Common.Utility;
using Desktop.ViewModels.SettingPages;

namespace Desktop.Commands;

public class ConfigureSystemInfo : Command
{
    public override void Execute() => SettingsWindow.Open<SystemInfo>();
}
