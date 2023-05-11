using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using Desktop.Config;
using PostSharp.Patterns.Model;

namespace Desktop;

[NotifyPropertyChanged]
public partial class DesktopWindow : Window
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
    public bool IsMouseDown { get; set; }

    public DesktopWindow()
    {
        InitializeWindow();
        InitializeComponent();
        Show();
    }

    #region Window

    void InitializeWindow() =>
        ResetBounds();

    void UiWindow_Loaded(object sender, RoutedEventArgs e)
    {
        View.Config.PropertyChanged += Config_PropertyChanged;
        View.IsLoaded = true;
        TaskbarFix.Initialize();
        ResetBounds();
    }

    void Config_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {

        if (e.PropertyName != nameof(View.Config.IsEditMode))
            return;

        if (View.Config.IsEditMode)
        {
            Common.Utility.xaml.NoNamespace.Common.SetPinToDesktop(this, false);
            Topmost = false;
            Topmost = true;
            WindowChrome.SetWindowChrome(this, new() { ResizeBorderThickness = new(10), CaptionHeight = 0, GlassFrameThickness = new(0), CornerRadius = new(8) });
        }
        else
        {
            Topmost = false;
            Common.Utility.xaml.NoNamespace.Common.SetPinToDesktop(this, true);
            WindowChrome.SetWindowChrome(this, new() { ResizeBorderThickness = new(0), CaptionHeight = 0, GlassFrameThickness = new(0), CornerRadius = new(8) });
        }

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

    protected override async void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (View.Config.IsEditMode)
        {
            IsMouseDown = true;
            Topmost = false;
            Topmost = true;
            await Task.Delay(10);
            DragMove();
            SaveSize();
            IsMouseDown = false;
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

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {

    }
}
