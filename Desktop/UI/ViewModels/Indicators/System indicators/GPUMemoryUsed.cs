using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class GPUMemoryUsed : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.Ram20;
    public override string? Tooltip => "GPU memory usage";

    public override string Sensor => nameof(SystemInfo.GPUMemoryUsed);

}
