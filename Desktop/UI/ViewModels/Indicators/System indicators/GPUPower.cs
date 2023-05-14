using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class GPUPower : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.DataArea24;
    public override string? Tooltip => "GPU power draw";

    public override string Sensor => nameof(SystemInfo.GPUPower);

}
