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
    //TODO: weather resestting to 'no location set' after system wake up

    //TODO: Add IndicatorWidget, which can take a list of Stack (root is a vertical stack), stacks can be nested
    //TODO: Each stack can take a list of Indicator
    //TODO: Add SpacerIndicator, which expands to consume available space, pushing indicators after it to the end
    //TODO: Add drag and drop for indicators
    //TODO: Add drag and drop for widgets

    //Widgets: FilesWidget, NotificationsWidget, NotesWidget, IndicatorWidget
    //Indicators: Time, Date, Weather, BluetoothBatteryIndicator, CpuUsageIndicator, CpuTemperatureIndicator, MemoryIndicator
    //TODO: Add more system info indicators

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

}
