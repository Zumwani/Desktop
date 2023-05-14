using Desktop.Utility;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class Notifications : SettingsPage<Config.Notifications>
{

    public override string Title => "Notifications";
    public override SymbolRegular Icon => SymbolRegular.Alert48;

    public RelayCommand OpenWindowsSettingsCommand { get; } = new(() => FileUtility.Open("ms-settings:notifications"));

}
