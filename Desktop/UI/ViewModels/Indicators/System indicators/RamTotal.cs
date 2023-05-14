using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class RamTotal : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.Ram20;
    public override string? Tooltip => "System memory";

    public override string Sensor => nameof(SystemInfo.RamTotal);

}
