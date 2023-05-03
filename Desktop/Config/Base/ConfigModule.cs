using System.ComponentModel;
using System.Runtime.CompilerServices;
using Desktop.Utility;

namespace Desktop.Config;

public abstract class ConfigModule : INotifyPropertyChanged
{

    public ConfigModule() =>
        ConfigUtility.Setup(this);

    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));

}
