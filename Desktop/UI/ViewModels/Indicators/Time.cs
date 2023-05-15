using System.Globalization;
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
        if (ConfigManager.DateAndTime.UseWindowsFormatForTime)
            Value = IndicatorUtility.DateTime.Value?.ToString(DateTimeUtility.SystemTimeFormat, CultureInfo.InvariantCulture);
        else
            Value = IndicatorUtility.DateTime.Value?.ToString(ConfigManager.DateAndTime.TimeFormat, CultureInfo.InvariantCulture);
    }

}
