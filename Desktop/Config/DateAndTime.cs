using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class DateAndTime : ConfigModule
{

    public bool UseWindowsFormatForDate { get; set; } = true;
    public bool UseWindowsFormatForTime { get; set; } = true;

    public string DateFormat { get; set; } = "";
    public string TimeFormat { get; set; } = "";

    public string CalendarUri { get; set; } = "https://calendar.google.com";

}
