using System;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Notifications : ConfigModule
{

    public bool CollapseNotificationsWhenMultipleRecievedFromSameSource { get; set; } = true;
    public bool ShowClearAllButton { get; set; } = true;

    public bool OverrideWindowsNotifications { get; set; } = true;
    public TimeSpan DelayBeforeHidingWindowsNotification { get; set; } = TimeSpan.FromSeconds(5);

}
