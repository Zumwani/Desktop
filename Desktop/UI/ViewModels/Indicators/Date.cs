using System.Globalization;
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
        if (ConfigManager.DateAndTime.UseWindowsFormatForDate)
            Value = IndicatorUtility.DateTime.Value?.ToString(DateTimeUtility.SystemDateFormat, CultureInfo.InvariantCulture);
        else
            Value = IndicatorUtility.DateTime.Value?.ToString(ConfigManager.DateAndTime.DateFormat, CultureInfo.InvariantCulture);
    }

}
