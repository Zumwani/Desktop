using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class IdleMode : SettingsPage<Config.IdleMode>
{
    public override string Title => "Idle Mode";
    public override SymbolRegular Icon => SymbolRegular.ShareScreenPersonOverlay28;
}
