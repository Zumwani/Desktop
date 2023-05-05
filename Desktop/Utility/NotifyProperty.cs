using System;
using PostSharp.Patterns.Model;

namespace Desktop.Utility;

[NotifyPropertyChanged]
public class NotifyProperty<T>
{

    public Action? OnUpdateRequest { get; init; }

    public T? Value { get; set; }

    public void Update() =>
        OnUpdateRequest?.Invoke();

}
