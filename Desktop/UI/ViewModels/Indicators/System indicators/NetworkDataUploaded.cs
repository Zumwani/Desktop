using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class NetworkDataUploaded : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.ArrowUpload24;
    public override string? Tooltip => "Data uploaded";

    public override string Sensor => nameof(SystemInfo.NetworkDataUploaded);

}
