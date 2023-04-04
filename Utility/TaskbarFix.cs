using System.Windows;
using System.Windows.Media;

namespace Desktop.Utility;

static class TaskbarFix
{

    static readonly Window window = new()
    {
        WindowStyle = WindowStyle.None,
        ResizeMode = ResizeMode.NoResize,
        ShowInTaskbar = false,
        Background = Brushes.Black,
        BorderThickness = new(0),
        Topmost = true
    };

    static bool isInitialized;
    public static void Initialize()
    {

        if (isInitialized)
            return;
        isInitialized = true;

        Common.Utility.AttachedProperties.Common.SetIsVisibleInAltTab(window, false);
        window.Closing += (s, e) => e.Cancel = !App.Current.Dispatcher.HasShutdownStarted;

        window.Height = 1;
        window.Left = 0;
        window.Width = SystemParameters.PrimaryScreenWidth;
        window.Top = SystemParameters.PrimaryScreenHeight - window.Height;

        window.Show();

    }

}
