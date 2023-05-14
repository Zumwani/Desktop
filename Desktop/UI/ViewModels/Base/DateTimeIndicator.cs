using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public abstract class DateTimeIndicator : IntervalViewModel
{
    public RelayCommand OpenCalendarCommand { get; } = new(() => FileUtility.Open(ConfigManager.DateAndTime.CalendarUri));
}
