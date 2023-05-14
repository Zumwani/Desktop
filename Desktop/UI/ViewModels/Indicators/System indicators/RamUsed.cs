using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class RamUsed : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.Ram20;
    public override string? Tooltip => "System memory usage";

    public override string Sensor => nameof(SystemInfo.RamUsed);

}
