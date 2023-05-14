using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class BatteryRemainingTime : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.BatteryCharge24;
    public override string? Tooltip => "Remaining battery time (estimated)";

    public override string Sensor => nameof(SystemInfo.BatteryRemainingTime);

}
