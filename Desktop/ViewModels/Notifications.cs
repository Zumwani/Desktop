using System.Collections.ObjectModel;
using System.Linq;
using Desktop.Models;
using Desktop.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Notifications
{

    public bool HasItems { get; set; }
    public Date Date { get; } = new Date();

    public RelayCommand ClearNotificationsCommand { get; } = new(NotificationUtility.ClearAll);
    public RelayCommand TestCommand { get; } = new(() => NotificationUtility.Notify("test"));

    [SafeForDependencyAnalysis]
    public ObservableCollection<Notification> Items => NotificationUtility.Notifications;

    public Notifications() =>
        Items.CollectionChanged += (s, e) => HasItems = Items.Any();

}
