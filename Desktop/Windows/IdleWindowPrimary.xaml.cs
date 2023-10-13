using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Desktop.ViewModels;
using Microsoft.Win32;
using PostSharp.Patterns.Model;
using ShellUtility.Screens;

namespace Desktop.Windows;

[NotifyPropertyChanged]
public partial class IdleWindowPrimary : Window
{

    public bool IsIdle => true;
    public IdleWindow View { get; } = IdleWindow.Instance;

    public static ObservableCollection<Notification> Notifications { get; private set; } = new();

    public IdleWindowPrimary()
    {

        Timer();
        ResetBounds();
        SystemEvents.DisplaySettingsChanged += (s, e) => ResetBounds();
        InitializeComponent();
        Show();

        View.Notifications.Items.CollectionChanged += async (s, e) =>
        {

            await Task.Delay(1);

            if (!View.IsIdle)
            {
                foreach (var item in Notifications.ToArray())
                    Notifications.Remove(item);
            }
            else
            {

                foreach (var item in e.NewItems?.OfType<Notification>() ?? Enumerable.Empty<Notification>())
                    Notifications.Remove(item);

                foreach (var item in e.NewItems?.OfType<Notification>() ?? Enumerable.Empty<Notification>())
                {
                    Notifications.Add(item);
                    NotificationUtility.Hide(item.Model);
                }

            }

        };

    }

    public DateTime Time { get; private set; }

    async void Timer()
    {

        Time = DateTime.Now;

        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await timer.WaitForNextTickAsync())
        {
            Time = DateTime.Now;
            Topmost = false;
            Topmost = true;
        }

    }

    void ResetBounds()
    {

        var screen = Screen.FromIndex(1, false)?.Bounds;
        if (!screen.HasValue)
            return;

        Left = screen.Value.Left;
        Top = screen.Value.Top;
        Width = screen.Value.Width;
        Height = screen.Value.Height;

        Topmost = true;
        Topmost = false;
        Topmost = true;

    }

}
