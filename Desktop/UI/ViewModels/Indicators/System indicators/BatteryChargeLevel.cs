using Server.Models;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

public class BatteryChargeLevel : SystemTrackerIndicator
{

    public override SymbolRegular Icon => SymbolRegular.BatteryCharge24;
    public override string? Tooltip => "Battery level";

    public override string StringFormat => StringFormats.Percentage;
    public override string Sensor => nameof(SystemInfo.BatteryChargeLevel);

}
