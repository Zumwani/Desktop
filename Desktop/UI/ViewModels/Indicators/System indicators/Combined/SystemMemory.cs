using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class SystemMemory : SystemTrackerIndicatorMulti
{

    public override SymbolRegular Icon => SymbolRegular.Ram20;
    public override string? Tooltip => "System memory: Used / Total";

    public override string? StringFormat => StringFormats.GB;
    public override string[] Sensors { get; } =
    {
        nameof(SystemInfo.RamUsed),
        nameof(SystemInfo.RamTotal),
    };

}
