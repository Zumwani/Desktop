using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Notification : ModelViewModel<Models.Notification>
{

    public RelayCommand HideCommand { get; }

    public Notification() => 
        HideCommand = new(() => NotificationUtility.Hide(Model));

}
