using System;
using System.Collections.ObjectModel;
using System.Linq;
using Desktop.Config;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Notifications : ViewModel
{

    public WindowConfig? Config { get; set; }
    public Config.Notifications NotificationsConfig { get; } = ConfigManager.Notifications;

    [SafeForDependencyAnalysis]
    public bool HasItems => Items.Any();
    public Date Date { get; } = new Date();

    public RelayCommand ClearNotificationsCommand { get; } = new(NotificationUtility.ClearAll);
    public RelayCommand CreateTimedCommand { get; } = new(() => NotificationUtility.Notify("this is a timed notification", TimeSpan.FromSeconds(2.5)));
    public RelayCommand CreatePermanentCommand { get; } = new(() => NotificationUtility.Notify("this is a permanent notification this is a permanent notification this is a permanent notification this is a permanent notification this is a permanent notification this is a permanent notification this is a permanent notification "));

    public bool ShowTestButton { get; set; }

    [SafeForDependencyAnalysis]
    public ObservableCollection<Notification> Items { get; } = new();

    public Notifications()
    {

        Items.CollectionChanged += (s, e) => NotifyPropertyChangedServices.SignalPropertyChanged(this, nameof(HasItems));
        NotificationUtility.Notifications.OnAdded(n => Items.Add(new() { Model = n }));
        NotificationUtility.Notifications.OnRemoved(n => Items.RemoveWhere(i => i.Model == n));

        NotificationUtility.Notifications.OnClear(Items.Clear);

    }

}
