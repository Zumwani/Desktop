using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class NetworkDataDownloaded : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.ArrowDownload48;
    public override string? Tooltip => "Data downloaded";

    public override string Sensor => nameof(SystemInfo.NetworkDataDownloaded);

}
