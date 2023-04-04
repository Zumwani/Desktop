using System;
using System.Windows.Data;
using System.Windows.Threading;
using PostSharp.Patterns.Model;

namespace Desktop.Views;

[NotifyPropertyChanged]
public class IsIdleExtension : Binding
{

    public bool IsIdle { get; private set; }

    public IsIdleExtension()
    {

        Source = this;
        Path = new(nameof(IsIdle));
        Mode = BindingMode.OneWay;

        var timer = new DispatcherTimer(
            TimeSpan.FromSeconds(0.5),
            DispatcherPriority.Background,
            (s, e) => IsIdle = IdleUtility.IsIdle(),
            Dispatcher.CurrentDispatcher);

    }

}