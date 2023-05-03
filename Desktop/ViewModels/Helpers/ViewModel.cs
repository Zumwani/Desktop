using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Desktop.ViewModels.Helpers;

public abstract class ViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new(propertyName));

}
