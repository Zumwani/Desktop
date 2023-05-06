using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using Desktop.Config;
using PostSharp.Patterns.Model;
using WindowsHook;
using Wpf.Ui.Controls;

namespace Desktop;

[NotifyPropertyChanged]
public partial class DesktopWindow : UiWindow
{

    //TODO: Move edit mode to settings, use button to enable and one to disable
    //TODO: Fix drag start from files
    //TODO: Add option to switch to farenheit for system indicator
    //TODO: Fix tab button scroll bar in settings
    //TODO: Update time and date format when changed in system

    public ViewModels.DesktopWindow View { get; } = new();

    public Config.DesktopWindow Config { get; } = ConfigManager.DesktopWindow;

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

    async void UiWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await Task.Delay(1000);
        View.IsLoaded = true;
        TaskbarFix.Initialize();
        ResetBounds();
    }

    void ResetBounds()
    {
        Left = Config.Left;
        Top = Config.Top;
        Width = Config.Width;
        Height = Config.Height;
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
        Config.Left = Left;
        Config.Top = Top;
        Config.Width = Width;
        Config.Height = Height;
    }

    #endregion

    async void UiWindow_CallbackOnShutdown(object sender, MouseButtonEventArgs e)
    {
        View.IsOpen = false;
        await Task.Delay(250);
    }

}
