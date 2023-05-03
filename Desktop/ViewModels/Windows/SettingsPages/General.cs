using PostSharp.Patterns.Model;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

[NotifyPropertyChanged]
class General : SettingsPage<Config.General>
{

    public override string Title => "General";
    public override SymbolRegular Icon => SymbolRegular.Wrench24;

    public RelayCommand CreateStartmenuShortcutCommand { get; } = new(() => { });

}
