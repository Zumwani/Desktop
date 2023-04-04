using Server.Models;

namespace Server.Utility;

static class NotificationUtility
{

    static readonly List<Notification> notifications = new();

    public static IEnumerable<Notification> ListQueue() =>
        notifications;

    public static IEnumerable<Notification> PopQueue()
    {
        var list = notifications.ToArray();
        notifications.Clear();
        return list;
    }

    public static void Queue(string? appTitle, string? content)
    {
        if (!string.IsNullOrWhiteSpace(appTitle) && !string.IsNullOrWhiteSpace(content))
            notifications.Add(new() { AppTitle = appTitle, Content = content });
    }

}