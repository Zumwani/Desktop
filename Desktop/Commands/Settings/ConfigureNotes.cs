using Common.Utility;
using Desktop.ViewModels.SettingPages;

namespace Desktop.Commands;

public class ConfigureNotes : Command
{
    public override void Execute() => SettingsWindow.Open<Notes>();
}
