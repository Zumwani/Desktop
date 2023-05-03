using System;
using System.Collections.Generic;
using System.ComponentModel;
using Desktop.Config;
using Desktop.Models;
using Desktop.Utility;
using Microsoft.Win32;

namespace Desktop.ViewModels.Helpers;

public static class Helper
{

    static Helper()
    {
        Reload();
        ConfigManager.SystemInfo.PropertyChanged += PropertyChanged;
        ConfigManager.Weather.PropertyChanged += PropertyChanged;
        SystemEvents.PowerModeChanged += (s, e) =>
        {
            if (e.Mode == PowerModes.Resume)
                Reload();
        };
    }

    static void PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Config.Weather.UpdateInterval))
            Reload();
    }

    static void Reload()
    {

        ActionUtility.StopInvoke(UpdateDateTime);
        ActionUtility.StopInvoke(UpdateBluetoothDevices);
        ActionUtility.StopInvoke(UpdateSystemInfo);
        ActionUtility.StopInvoke(UpdateWeather);

        ActionUtility.Invoke(UpdateDateTime, TimeSpan.FromSeconds(0.01)); //Used by notification progress bars (slower means staggered progress change)
        ActionUtility.Invoke(UpdateBluetoothDevices, ConfigManager.SystemInfo.UpdateInterval);
        ActionUtility.Invoke(UpdateSystemInfo, ConfigManager.SystemInfo.UpdateInterval);
        ActionUtility.Invoke(UpdateWeather, ConfigManager.Weather.UpdateInterval);

    }

    public static NotifyProperty<Server.Models.SystemInfo?> SystemInfo { get; } = new() { onUpdateRequest = UpdateSystemInfo };
    public static NotifyProperty<Models.Weather> Weather { get; } = new() { onUpdateRequest = UpdateWeather };
    public static NotifyProperty<IEnumerable<BluetoothDevice>> BluetoothDevices { get; } = new() { onUpdateRequest = UpdateBluetoothDevices };
    public static NotifyProperty<DateTime> DateTime { get; } = new() { onUpdateRequest = UpdateDateTime };

    async static void UpdateSystemInfo() => SystemInfo.Value = await Server.API.GetSystemStatus();
    async static void UpdateWeather() => Weather.Value = await WeatherUtility.Get();
    async static void UpdateBluetoothDevices() => BluetoothDevices.Value = await BluetoothUtility.Get();
    static void UpdateDateTime() => DateTime.Value = System.DateTime.Now;

}
