using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public abstract class SystemTrackerIndicator : TrackerIndicator
{

    public abstract SymbolRegular Icon { get; }
    public virtual string? StringFormat { get; }
    public abstract string Sensor { get; }
    public virtual string? Tooltip { get; }

    public string? Value { get; private set; }

    public override void Update()
    {
        Value = IndicatorUtility.Tracker.Value?.GetValue(Sensor)?.ToString(StringFormat);
    }
}
