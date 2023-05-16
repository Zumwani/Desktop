using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class CPULoad : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.DataArea24;
    public override string? Tooltip => "CPU usage";

    public override string Sensor => nameof(SystemInfo.CPULoad);

}
