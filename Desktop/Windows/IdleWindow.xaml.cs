using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Desktop.Config;
using PostSharp.Patterns.Model;

namespace Desktop;

[NotifyPropertyChanged]
public partial class IdleWindow : Window
{

    public bool IsIdle => true;
    public ViewModels.IdleWindow View { get; } = new();

    public IdleWindow()
    {
        ResetBounds();
        InitializeComponent();
        Show();
        ConfigManager.DesktopWindow.PropertyChanged += (s, e) => ResetBounds();
    }

    void ResetBounds()
    {
        rect = ShellUtility.Screens.Screen.FromWindowHandle(Application.Current.MainWindow.Handle()).Bounds.ToRect();
        Left = rect.Left - 1;
        Top = rect.Top - 1;
        Width = rect.Width + 2;
        Height = rect.Height + 2;
    }

    void UiWindow_Loaded(object sender, RoutedEventArgs e)
    {
        ActionUtility.Invoke(() => { if (!GetIfMouseOver()) View.IsOpen = true; }, TimeSpan.FromSeconds(0.1));
        ResetBounds();
        Topmost = true;
        Topmost = false;
        Topmost = true;
    }

    protected override void OnClosing(CancelEventArgs e) =>
        e.Cancel = !Application.Current.Dispatcher.HasShutdownStarted;

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
