using Desktop.Config;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Date : DateTimeIndicator
{

    public string? Value { get; private set; }

    public override void Update()
    {
        var format = ConfigManager.DateAndTime.UseWindowsFormatForDate ? "d" : ConfigManager.DateAndTime.DateFormat;
        Value = IndicatorUtility.DateTime.Value?.ToString(format);
    }

}
