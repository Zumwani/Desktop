using Common.Utility;
using Desktop.ViewModels.SettingPages;

namespace Desktop.Commands;

public class ConfigureGeneral : Command
{
    public override void Execute() => SettingsWindow.Open<General>();
}
