using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class NetworkThroughputUpload : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.ArrowUpload24;
    public override string? Tooltip => "Network throughput (upload)";

    public override string Sensor => nameof(SystemInfo.NetworkThroughputUpload);

}
