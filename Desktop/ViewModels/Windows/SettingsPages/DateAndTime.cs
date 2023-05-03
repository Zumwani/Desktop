using Desktop.Utility;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class DateAndTime : SettingsPage<Config.DateAndTime>
{

    public override string Title => "Date & time";
    public override SymbolRegular Icon => SymbolRegular.Clock48;

    public RelayCommand OpenWindowsSettingsCommand { get; } = new(() => FileUtility.Open("ms-settings:dateandtime"));

}
