using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class CPUTemp : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.Temperature24;
    public override string? Tooltip => "CPU temperature";

    public override string Sensor => nameof(SystemInfo.CPUTemp);

}
