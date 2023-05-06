using Desktop.ViewModels.Helpers;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class Weather : SettingsPage<Config.Weather>
{

    public override string Title => "Weather";
    public override SymbolRegular Icon => SymbolRegular.WeatherSunny48;

    public RelayCommand ReloadCommand { get; } = new(IndicatorUtility.Weather.RequestUpdate);
    public RelayCommand UrlClickCommand { get; } = new(() => FileUtility.Open(WeatherUtility.Url));

}
