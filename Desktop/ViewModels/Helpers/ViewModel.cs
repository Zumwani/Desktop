using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Desktop.ViewModels.Helpers;

public abstract class ViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new(propertyName));

    public void OnPropertyChanged() =>
        PropertyChanged?.Invoke(this, new(string.Empty));

}
