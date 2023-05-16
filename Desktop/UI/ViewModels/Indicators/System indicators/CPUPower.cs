using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class CPUPower : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.DataUsage24;
    public override string? Tooltip => "CPU power draw";

    public override string Sensor => nameof(SystemInfo.CPUPower);

}
