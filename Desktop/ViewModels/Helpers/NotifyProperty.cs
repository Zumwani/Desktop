using System;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels.Helpers;

[NotifyPropertyChanged]
public class NotifyProperty<T>
{

    public Action? onUpdateRequest { get; init; }

    public T? Value { get; set; }

    public void Update() =>
        onUpdateRequest?.Invoke();

}
