using Desktop.ViewModels.Helpers;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class SystemInfo : SettingsPage<Config.SystemInfo>
{

    public override string Title => "System info";
    public override SymbolRegular Icon => SymbolRegular.Info28;

    public RelayCommand RestartServerCommand { get; } = new(ServerUtility.Restart);
    public RelayCommand ReloadCommand { get; } = new(Helper.SystemInfo.Update);

}
