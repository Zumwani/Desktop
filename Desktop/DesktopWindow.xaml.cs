using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using Desktop.Utility;
using PostSharp.Patterns.Model;
using WindowsHook;
using Wpf.Ui.Controls;

namespace Desktop;

[NotifyPropertyChanged]
public partial class DesktopWindow : UiWindow
{

    public DesktopWindow()
    {
        InitializeWindow();
        InitializeComponent();
        Show();
    }

    #region Window

    void InitializeWindow()
    {
        ResetBounds();
        var hook = Hook.GlobalEvents();
        hook.KeyDown += KeyDown;
        hook.KeyUp += KeyUp;
    }

    public bool IsInitialUpdate { get; private set; } = true;

    async void UiWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await Task.Delay(1000);
        IsInitialUpdate = false;
        TaskbarFix.Initialize();
        ResetBounds();
    }

    void ResetBounds()
    {
        Left = Settings.Left.Current;
        Top = Settings.Top.Current;
        Width = Settings.Width.Current;
        Height = Settings.Height.Current;
    }

    #endregion
    #region Move mode

    public bool IsShiftDown { get; private set; }

    new void KeyDown(object sender, WindowsHook.KeyEventArgs e)
    {
        if (e.KeyCode == Keys.LShiftKey)
        {
            IsShiftDown = true;
            WindowChrome.SetWindowChrome(this, new() { ResizeBorderThickness = new(10), CaptionHeight = 0, GlassFrameThickness = new(0), CornerRadius = new(8) });
        }
    }

    new void KeyUp(object sender, WindowsHook.KeyEventArgs e)
    {
        if (e.KeyCode == Keys.LShiftKey)
        {
            IsShiftDown = false;
            WindowChrome.SetWindowChrome(this, new() { ResizeBorderThickness = new(0), CaptionHeight = 0, GlassFrameThickness = new(0), CornerRadius = new(8) });
            SaveSize();
        }
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (IsShiftDown)
        {
            DragMove();
            SaveSize();
        }
    }

    void SaveSize()
    {
        //Settings.Bounds.Current.Value = new(PointToScreen(new()), PointToScreen(new(ActualWidth, ActualHeight)));
        //Settings.Bounds.Current.Save(false);
        Settings.Left.Current.Value = Left;
        Settings.Top.Current.Value = Top;
        Settings.Width.Current.Value = Width;
        Settings.Height.Current.Value = Height;
    }

    #endregion

}
