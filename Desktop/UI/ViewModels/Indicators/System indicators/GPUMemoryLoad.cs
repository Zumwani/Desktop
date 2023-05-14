using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class GPUMemoryLoad : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.DataArea24;
    public override string? Tooltip => "GPU memory usage";

    public override string StringFormat => StringFormats.Percentage;
    public override string Sensor => nameof(SystemInfo.GPUMemoryLoad);

}
