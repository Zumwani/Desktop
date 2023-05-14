using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public abstract class TrackerIndicator : IntervalViewModel
{
    public RelayCommand OpenTaskManager { get; } = new(() => FileUtility.Open("taskmgr"));
}
