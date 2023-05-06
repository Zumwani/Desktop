using System.Windows;
using System.Windows.Media;
using Desktop.Config;

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

        Common.Utility.xaml.NoNamespace.Common.SetIsVisibleInAltTab(window, false);
        window.Closing += (s, e) => e.Cancel = !App.Current.Dispatcher.HasShutdownStarted;

        window.Height = 1;
        window.Left = 0;
        window.Width = SystemParameters.PrimaryScreenWidth;
        window.Top = SystemParameters.PrimaryScreenHeight - window.Height;

        if (ConfigManager.General.ShowBorderAlongBottomOfPrimaryScreen)
            window.Show();

        ConfigManager.General.PropertyChanged += (s, e) =>
        {
            if (ConfigManager.General.ShowBorderAlongBottomOfPrimaryScreen)
                window.Show();
            else
                window.Hide();
        };

    }

}
