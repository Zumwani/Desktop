using Common.Utility;
using Desktop.ViewModels.SettingPages;

namespace Desktop.Commands;

public class ConfigureFiles : Command
{
    public override void Execute() => SettingsWindow.Open<Files>();
}
