using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class CpuUsage : IntervalViewModel
{

    public double? Value { get; private set; }

    public override void Update() =>
        Value = IndicatorUtility.SystemInfo.Value?.CpuUsage;

}
