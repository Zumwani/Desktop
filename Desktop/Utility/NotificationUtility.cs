using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Config;
using Desktop.Models;
using Windows.UI.Notifications.Management;

namespace Desktop.Utility;

static partial class NotificationUtility
{

    public static ObservableCollection<Notification> Notifications { get; } = new();

    static readonly Dictionary<Notification, CancellationTokenSource> tokens = new();

    static NotificationUtility()
    {
        InitializeNoteNotifications();
        InitializeWindowsNotificationsListener();
    }

    public static async void Hide(Notification notification)
    {

        if (tokens.Remove(notification, out var token))
            token.Cancel();

        notification.IsVisible = false;
        await Task.Delay(250);

        _ = Notifications.Remove(notification);

    }

    public static void ClearAll()
    {
        Notifications.Clear();
        tokens.Clear();
    }

    public static bool Find(string header, [NotNullWhen(true)] out Notification? notification) =>
        (notification = Notifications.FirstOrDefault(n => n.Header == header)) is not null;

    static bool IsDuplicate(Notification notification, [NotNullWhen(true)] out Notification? existingNotification)
    {

        existingNotification = null;

        return
            ConfigManager.Notifications.CollapseNotificationsWhenMultipleRecievedFromSameSource &&
            !string.IsNullOrEmpty(notification.Header) &&
            Find(notification.Header, out existingNotification);

    }

    #region Notify

    public static Task Notify(string message, TimeSpan? duration = null) =>
        Notify(new Notification(message, duration));

    public static async Task Notify(Notification notification)
    {

        if (IsDuplicate(notification, out var existingNotification))
        {
            existingNotification.Collapse();
            return;
        }

        tokens.Add(notification, new());
        Notifications.Add(notification);

        if (!notification.IsPermanent)
            _ = await Task.WhenAny(Cancelled(), DurationRanOut());
        else
            await Cancelled();

        Hide(notification);

        async Task Cancelled()
        {
            while (tokens.TryGetValue(notification, out var token) && !token.IsCancellationRequested)
                await Task.Delay(100);
        }

        async Task DurationRanOut()
        {
            while (notification.EndTime.HasValue && DateTime.Now < notification.EndTime.Value)
                await Task.Delay(100);
        }

    }

    #endregion
    #region Note notifications

    static readonly Dictionary<Note, Notification> noteNotifications = new();

    static IEnumerable<Note> NonActiveNotes => ConfigManager.Notes.Items.Where(n => !noteNotifications.ContainsKey(n));

    static void InitializeNoteNotifications() =>
        ActionUtility.Invoke(
            TimeSpan.FromSeconds(1),
            () => Notify(NonActiveNotes.Where(IsOnTriggerDay).Where(IsWithinTriggerTimeSpan)));

    static bool IsOnTriggerDay(Note note) =>
        TriggersOn[(int)DateTime.Now.DayOfWeek].Invoke(note);

    static readonly List<Func<Note, bool>> TriggersOn = new()
    {
        (n) => n.NotifyOnSunday, //DayOfWeek enum starts on sunday, which certainly isn't confusing for 94% of the world
        (n) => n.NotifyOnMonday,
        (n) => n.NotifyOnTuesday,
        (n) => n.NotifyOnWednesday,
        (n) => n.NotifyOnThursday,
        (n) => n.NotifyOnFriday,
        (n) => n.NotifyOnSaturday,
    };

    static bool IsWithinTriggerTimeSpan(Note note) =>
        DateTime.Now.TimeOfDay > note.NotifyAt.TimeOfDay && DateTime.Now.TimeOfDay < note.NotifyAt.TimeOfDay + note.NotifyDuration;

    static void Notify(IEnumerable<Note> notes)
    {
        foreach (var note in notes.ToArray())
            Notify(note);
    }

    static async void Notify(Note note)
    {

        var notification = note.PermanentNotification ? new Notification(note.Content) : new Notification(note.Content, note.NotifyDuration);
        noteNotifications.Add(note, notification);
        await Notify(notification);

        while (DateTime.Now < notification.EndTime)
            await Task.Delay(100);

        _ = noteNotifications.Remove(note);

    }

    #endregion
    #region Windows notifications

    static void InitializeWindowsNotificationsListener()
    {

        var listener = UserNotificationListener.Current;
        var list = new List<uint>();

        ActionUtility.Invoke(async () =>
        {

            if (!ConfigManager.Notifications.OverrideWindowsNotifications)
                return;

            var notifications =
                (await listener.GetNotificationsAsync(Windows.UI.Notifications.NotificationKinds.Toast)).
                Where(n => !list.Contains(n.Id)).
                ToArray();

            foreach (var notification in notifications)
            {

                list.Add(notification.Id);

                var binding = notification.Notification.Visual.GetBinding(Windows.UI.Notifications.KnownNotificationBindings.ToastGeneric);
                var text = string.Join("\n", binding.GetTextElements().Select(t => t.Text));

                var header = binding.GetTextElements()[0].Text;
                _ = Notify(new Notification(text) { Header = header }).ContinueWith(t =>
                {
                    listener.RemoveNotification(notification.Id);
                    _ = list.Remove(notification.Id);
                });

                await Task.Delay(ConfigManager.Notifications.DelayBeforeHidingWindowsNotification);
                listener.RemoveNotification(notification.Id);

            }


        }, TimeSpan.FromSeconds(0.1));

    }

    #endregion

}
