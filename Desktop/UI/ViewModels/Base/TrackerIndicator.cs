using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public abstract class TrackerIndicator : IntervalViewModel
{

    public RelayCommand OpenTaskManager { get; } = new(() => FileUtility.Open("taskmgr"));

    public static class StringFormats
    {
        public const string Percentage = "# \\%";
        public const string Temperature = "00 ℃";
        public const string GB = "00 GB";
    }

}
