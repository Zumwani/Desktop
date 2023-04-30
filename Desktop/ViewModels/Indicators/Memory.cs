using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Memory : IntervalViewModel
{

    public double? UsedMemory { get; private set; }
    public double? AvailableMemory { get; private set; }

    public override void Update()
    {
        UsedMemory = Helper.SystemInfo.Value?.UsedMemory;
        AvailableMemory = Helper.SystemInfo.Value?.AvailableMemory;
    }

}
