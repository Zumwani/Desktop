using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class HideNotification : Command<Notification>
{

    public override void Execute(Notification? notification)
    {
        if (notification is not null)
            NotificationUtility.Hide(notification);
    }

}
