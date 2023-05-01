using System;
using System.Collections.Generic;
using Desktop.Models;
using Desktop.Utility;
using Server.Models;

namespace Desktop.ViewModels.Helpers;

public static class Helper
{

    static Helper()
    {
        ActionUtility.Invoke(UpdateDateTime, TimeSpan.FromSeconds(0.05)); //Used by notification progress bars (slower means staggered progress change)
        ActionUtility.Invoke(UpdateBluetoothDevices, TimeSpan.FromSeconds(2));
        ActionUtility.Invoke(UpdateSystemInfo, TimeSpan.FromSeconds(2));
        ActionUtility.Invoke(UpdateWeather, TimeSpan.FromMinutes(1));
    }

    public static NotifyProperty<SystemInfo?> SystemInfo { get; } = new() { onUpdateRequest = UpdateSystemInfo };
    public static NotifyProperty<Models.Weather> Weather { get; } = new() { onUpdateRequest = UpdateWeather };
    public static NotifyProperty<IEnumerable<BluetoothDevice>> BluetoothDevices { get; } = new() { onUpdateRequest = UpdateBluetoothDevices };
    public static NotifyProperty<DateTime> DateTime { get; } = new() { onUpdateRequest = UpdateDateTime };

    async static void UpdateSystemInfo() => SystemInfo.Value = await Server.API.GetSystemStatus();
    async static void UpdateWeather() => Weather.Value = await WeatherUtility.Get();
    async static void UpdateBluetoothDevices() => BluetoothDevices.Value = await BluetoothUtility.Get();
    static void UpdateDateTime() => DateTime.Value = System.DateTime.Now;

}
