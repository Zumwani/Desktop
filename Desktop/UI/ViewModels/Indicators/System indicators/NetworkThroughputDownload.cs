using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class NetworkThroughputDownload : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.ArrowDownload48;
    public override string? Tooltip => "Network throughput (download)";

    public override string Sensor => nameof(SystemInfo.NetworkThroughputDownload);

}

