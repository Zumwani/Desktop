using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Desktop.ViewModels.Helpers;

public abstract class ViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new(propertyName));

    public RelayCommand OpenDateTimeConfigCommand { get; } = SettingsWindow.OpenDateTimeCommand;
    public RelayCommand OpenFilesConfigCommand { get; } = SettingsWindow.OpenFilesCommand;
    public RelayCommand OpenConfigCommand { get; } = SettingsWindow.OpenGeneralCommand;
    public RelayCommand OpenIdleModeConfigCommand { get; } = SettingsWindow.OpenIdleModeCommand;
    public RelayCommand OpenNotificationConfigCommand { get; } = SettingsWindow.OpenNotificationCommand;
    public RelayCommand OpenTrackerConfigCommand { get; } = SettingsWindow.OpenTrackerCommand;
    public RelayCommand OpenWeatherConfigCommand { get; } = SettingsWindow.OpenWeatherCommand;

}
