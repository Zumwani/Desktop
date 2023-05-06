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

    public bool HasItems { get; set; }
    public Date Date { get; } = new Date();

    public RelayCommand ClearNotificationsCommand { get; } = new(NotificationUtility.ClearAll);
    public RelayCommand TestCommand { get; } = new(() => NotificationUtility.Notify("test"));

    public bool ShowTestButton { get; set; }

    [SafeForDependencyAnalysis]
    public ObservableCollection<Notification> Items { get; } = new();

    public Notifications()
    {

        Items.CollectionChanged += (s, e) => HasItems = Items.Any();
        NotificationUtility.Notifications.OnAdded(n => Items.Add(new() { Model = n }));
        NotificationUtility.Notifications.OnRemoved(n => Items.RemoveWhere(i => i.Model == n));

        NotificationUtility.Notifications.OnClear(Items.Clear);

    }

}
