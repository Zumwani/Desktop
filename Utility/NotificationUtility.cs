﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Models;
using Windows.UI.Notifications.Management;

namespace Desktop.Utility;

static class NotificationUtility
{

    static readonly ObservableCollection<Notification> notifications = new();
    public static ReadOnlyObservableCollection<Notification> Notifications { get; private set; } = new(notifications);

    static readonly Dictionary<Notification, CancellationTokenSource> tokens = new();

    static NotificationUtility()
    {
        InitializeNoteNotifications();
        InitializeWindowsNotificationsListener();
    }

    public static void Hide(Notification notification)
    {

        if (tokens.Remove(notification, out var token))
            token.Cancel();
        _ = notifications.Remove(notification);


    }

    #region Notify

    public static async Task Notify(Notification notification)
    {

        Hide(notification);
        tokens.Add(notification, new());
        notifications.Add(notification);

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

        async Task DurationRanOut() =>
            await Task.Delay(notification.Duration);

    }

    public static Task Notify(string message) =>
        Notify(message, TimeSpan.FromSeconds(5));

    public static Task Notify(string message, TimeSpan duration) =>
        Notify(new Notification(message, duration));

    #endregion
    #region Note notifications

    static readonly Dictionary<Note, Notification> noteNotifications = new();

    static IEnumerable<Note> NonActiveNotes => Settings.Notes.Current.Where(n => !noteNotifications.ContainsKey(n));

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
        (DateTime.Now.TimeOfDay > note.NotifyAt.TimeOfDay) && (DateTime.Now.TimeOfDay < note.NotifyAt.TimeOfDay + note.NotifyDuration);

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

            var notifications =
            (await listener.GetNotificationsAsync(Windows.UI.Notifications.NotificationKinds.Toast)).
            Where(n => !list.Contains(n.Id)).
            ToArray();

            foreach (var notification in notifications)
            {

                list.Add(notification.Id);

                var binding = notification.Notification.Visual.GetBinding(Windows.UI.Notifications.KnownNotificationBindings.ToastGeneric);
                var text = string.Join("\n", binding.GetTextElements().Select(t => t.Text));

                _ = Notify(new Notification(text)).ContinueWith(async t =>
                 {
                     listener.RemoveNotification(notification.Id);
                     await Task.Delay(1000);
                     _ = list.Remove(notification.Id);
                 });

            }


        }, TimeSpan.FromSeconds(0.5));

    }

    #endregion

}
