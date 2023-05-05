using Desktop.ViewModels.Helpers;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class Tracker : SettingsPage<Config.Trackers>
{

    public override string Title => "Trackers";
    public override SymbolRegular Icon => SymbolRegular.Info28;

    public RelayCommand RestartServerCommand { get; } = new(ServerUtility.Restart);
    public RelayCommand ReloadCommand { get; } = new(IndicatorUtility.SystemInfo.Update);

}
