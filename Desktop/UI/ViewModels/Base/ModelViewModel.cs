using System.ComponentModel;
using System.Runtime.CompilerServices;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels.Helpers;

[NotifyPropertyChanged]
public abstract class ModelViewModel<T> : ViewModel
{

    public T Model { get; set; } = default!;

    public override void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {

        base.OnPropertyChanged(propertyName);
        if (propertyName != nameof(Model) && Model is not null)
            if (Model is INotifyPropertyChanged m)
                m.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Model));


    }

}