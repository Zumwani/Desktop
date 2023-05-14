using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class GPUMemoryTotal : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.Ram20;
    public override string? Tooltip => "GPU memory";

    public override string Sensor => nameof(SystemInfo.GPUMemoryTotal);

}
