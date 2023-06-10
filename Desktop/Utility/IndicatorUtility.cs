using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Desktop.Config;
using Desktop.Models;
using Microsoft.Win32;

namespace Desktop.ViewModels.Helpers;

public static class IndicatorUtility
{

    static IndicatorUtility()
    {

        DateTimeUtility.OnFormatsChanged += Reload;
        ConfigManager.SystemInfo.PropertyChanged += PropertyChanged;
        ConfigManager.Weather.PropertyChanged += PropertyChanged;
        SystemEvents.TimeChanged += (s, e) => Reload();

        SystemEvents.PowerModeChanged += (s, e) =>
        {

            if (e.Mode == PowerModes.Suspend)
                Stop();
            else if (e.Mode == PowerModes.Resume)
                Reload();

        };

        Reload();

    }

    static void PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Config.Weather.UpdateInterval))
            Reload();
    }

    static async void Reload()
    {

        await Task.Delay(100);

        Stop();

        ActionUtility.Invoke(UpdateDateTime, TimeSpan.FromSeconds(0.01)); //Used by notification progress bars (slower means staggered progress change)
        ActionUtility.Invoke(UpdateBluetoothDevices, ConfigManager.SystemInfo.UpdateInterval);
        ActionUtility.Invoke(UpdateTracker, ConfigManager.SystemInfo.UpdateInterval);
        ActionUtility.Invoke(UpdateWeather, ConfigManager.Weather.UpdateInterval);
        ActionUtility.Invoke(WeatherInvalidUpdate, TimeSpan.FromSeconds(2));

    }

    static void Stop()
    {

        ActionUtility.StopInvoke(UpdateDateTime);
        ActionUtility.StopInvoke(UpdateBluetoothDevices);
        ActionUtility.StopInvoke(UpdateTracker);
        ActionUtility.StopInvoke(UpdateWeather);
        ActionUtility.StopInvoke(WeatherInvalidUpdate);

        Tracker.Value = null;
        Weather.Value = null;
        BluetoothDevices.Value = null;
        DateTime.Value = null;

    }

    static void WeatherInvalidUpdate()
    {

        if (WeatherUtility.IsValid && Weather.Value?.Icon is null)
            UpdateWeather();

    }

    public static NotifyProperty<Server.Models.SystemInfo?> Tracker { get; } = new() { OnUpdateRequest = UpdateTracker };
    public static NotifyProperty<Models.Weather?> Weather { get; } = new() { OnUpdateRequest = UpdateWeather };
    public static NotifyProperty<IEnumerable<BluetoothDevice>> BluetoothDevices { get; } = new() { OnUpdateRequest = UpdateBluetoothDevices };
    public static NotifyProperty<DateTime?> DateTime { get; } = new() { OnUpdateRequest = UpdateDateTime };

    static async void UpdateWeather() => Weather.Value = await WeatherUtility.Get();
    static async void UpdateBluetoothDevices() => BluetoothDevices.Value = await BluetoothUtility.Get();
    static void UpdateDateTime() => DateTime.Value = System.DateTime.Now;

    static async void UpdateTracker()
    {
        Tracker.Value = await Server.API.GetSystemStatus();
        if (Tracker.Value is null)
            ServerUtility.TryRestart();
    }

}
