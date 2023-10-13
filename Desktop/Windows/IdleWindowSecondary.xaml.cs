using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Desktop.ViewModels;
using Microsoft.Win32;
using PostSharp.Patterns.Model;
using ShellUtility.Screens;

namespace Desktop;

[NotifyPropertyChanged]
public partial class IdleWindowSecondary : Window
{

    public bool IsIdle => true;
    public ViewModels.IdleWindow View { get; private set; } = null!;

    static bool isInitialized;
    public static void Initialize()
    {

        if (isInitialized)
            return;
        isInitialized = true;

        SystemEvents.DisplaySettingsChanging += (s, e) => Reinitialize();
        Reinitialize();

    }

    static IdleWindowSecondary? window;
    public static async void Reinitialize()
    {

        if (window is not null && window.IsLoaded)
        {
            window.close = true;
            window.View = null!;
            window.Close();
        }

        var rect = Screen.FromIndex(0, false)?.Bounds.ToRect();
        if (!rect.HasValue)
        {
            await Task.Delay(1000);
            Reinitialize();
        }
        else
            window = new(rect.Value);

    }

    private IdleWindowSecondary(Rect rect)
    {

        this.rect = rect;

        Reload();
        SystemEvents.PowerModeChanged += (s, e) =>
        {

            if (e.Mode == PowerModes.Resume)
                Reload();

        };

        InitializeComponent();
        Show();

    }

    void Reload() =>
        View = IdleWindow.Instance;

    void ResetBounds()
    {
        Left = rect.Left - 1;
        Top = rect.Top - 1;
        Width = rect.Width + 2;
        Height = rect.Height + 2;
    }

    void UiWindow_Loaded(object sender, RoutedEventArgs e)
    {

        Reload();

        ActionUtility.Invoke(() => { if (!GetIfMouseOver()) if (View is not null) View.IsOpen = true; }, TimeSpan.FromSeconds(0.1));
        ResetBounds();
        Topmost = true;
        Topmost = false;
        Topmost = true;

    }

    bool close;
    protected override void OnClosing(CancelEventArgs e)
    {
        if (!close && !Application.Current.Dispatcher.HasShutdownStarted)
            e.Cancel = true;
    }

    void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e) =>
      View.IsOpen = false;

    void Window_PreviewDragEnter(object sender, DragEventArgs e) =>
      View.IsOpen = false;

    Rect rect;

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetCursorPos(out POINT point);

    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public int X;
        public int Y;
        public static implicit operator Point(POINT p) =>
            new(p.X, p.Y);
    }

    Point oldPos;
    DateTime lastPosChange;
    bool GetIfMouseOver()
    {

        if (!IsLoaded || !GetCursorPos(out var pos))
            return false;

        var newPos = (Point)pos;

        if (oldPos != newPos && !newPos.InRange(oldPos, 10))
        {

            Cursor = Cursors.Arrow;
            oldPos = newPos;
            lastPosChange = DateTime.Now;

        }
        else if (DateTime.Now - lastPosChange > TimeSpan.FromSeconds(2))
            Cursor = Cursors.None;

        return rect.Contains(newPos.X, newPos.Y);

    }

}
