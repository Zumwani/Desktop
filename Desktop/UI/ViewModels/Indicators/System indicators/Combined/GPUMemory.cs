using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class GPUMemory : SystemTrackerIndicatorMulti
{

    public override SymbolRegular Icon => SymbolRegular.Ram20;
    public override string? Tooltip => "GPU memory: Used / Total";

    public override string[] Sensors { get; } =
    {
        nameof(SystemInfo.GPUMemoryUsed),
        nameof(SystemInfo.GPUMemoryTotal),
    };

}
