using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class NetworkLoad : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.Wifi120;
    public override string? Tooltip => "Network usage";

    public override string Sensor => nameof(SystemInfo.NetworkLoad);

}
