using Desktop.Models;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Notifications : ConfigModule
{

    public bool CollapseNotificationsWhenMultipleRecievedFromSameSource { get; set; } = true;
    public bool ShowClearAllButton { get; set; } = true;

    public bool OverrideWindowsNotifications { get; set; } = true;
    public Duration DelayBeforeHidingWindowsNotification { get; set; } = Duration.FromSeconds(5);

}
