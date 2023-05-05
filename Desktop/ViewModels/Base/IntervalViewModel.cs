using System;

namespace Desktop.ViewModels.Helpers;

public abstract class IntervalViewModel : ViewModel
{

    public virtual TimeSpan Interval { get; } = TimeSpan.FromSeconds(1);
    public abstract void Update();

    public IntervalViewModel() =>
        ActionUtility.Invoke(Update, Interval);

}
