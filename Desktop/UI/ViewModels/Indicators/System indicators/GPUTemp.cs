using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class GPUTemp : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.Temperature24;
    public override string? Tooltip => "GPU temperature";

    public override string Sensor => nameof(SystemInfo.GPUTemp);

}
