using System.Linq;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;
using Wpf.Ui.Common;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public abstract class SystemTrackerIndicatorMulti : TrackerIndicator
{

    public abstract SymbolRegular Icon { get; }
    public virtual string? StringFormat { get; }
    public virtual string Delimiter { get; } = " / ";
    public virtual string? Tooltip { get; }

    public abstract string[] Sensors { get; }
    public string? Value { get; private set; }

    public override void Update()
    {
        Value = string.Join(Delimiter, Sensors.Select(sensor => IndicatorUtility.Tracker.Value?.GetValue(sensor)?.ToString(StringFormat)));
    }
}