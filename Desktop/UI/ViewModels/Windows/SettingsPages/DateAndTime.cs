using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class DateAndTime : SettingsPage<Config.DateAndTime>
{

    public override string Title => "Date & time";
    public override SymbolRegular Icon => SymbolRegular.Clock48;

    public RelayCommand OpenWindowsSettingsCommand { get; } = new(() => FileUtility.Open("ms-settings:dateandtime"));
    public RelayCommand OpenFormatDocCommand { get; } = new(() => FileUtility.Open("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=net-7.0#system-datetime-tostring(system-string)"));

}
