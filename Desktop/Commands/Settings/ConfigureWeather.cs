using Common.Utility;
using Desktop.ViewModels.SettingPages;

namespace Desktop.Commands;

public class ConfigureWeather : Command
{
    public override void Execute() => SettingsWindow.Open<Weather>();
}
