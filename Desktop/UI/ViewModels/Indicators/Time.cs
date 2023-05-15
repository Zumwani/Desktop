using Desktop.Config;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Time : DateTimeIndicator
{

    public string? Value { get; private set; }

    public override void Update()
    {
        var format = ConfigManager.DateAndTime.UseWindowsFormatForTime ? "t" : ConfigManager.DateAndTime.TimeFormat;
        Value = IndicatorUtility.DateTime.Value?.ToString(format);
    }

}
