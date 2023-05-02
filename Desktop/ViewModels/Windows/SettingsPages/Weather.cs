using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class Weather : SettingsPage
{
    public override string Title => "Weather";
    public override SymbolRegular Icon => SymbolRegular.WeatherSunny48;
}