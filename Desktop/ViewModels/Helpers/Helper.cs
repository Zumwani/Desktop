using System;
using Desktop.Utility;
using Server.Models;

namespace Desktop.ViewModels.Helpers;

public static class Helper
{

    static Helper()
    {
        ActionUtility.Invoke(UpdateSystemInfo, TimeSpan.FromSeconds(1));
        ActionUtility.Invoke(UpdateWeather, TimeSpan.FromMinutes(1));
    }

    public static NotifyProperty<SystemInfo?> SystemInfo { get; } = new() { onUpdateRequest = UpdateSystemInfo };
    public static NotifyProperty<Models.Weather> Weather { get; } = new() { onUpdateRequest = UpdateWeather };

    async static void UpdateSystemInfo() => SystemInfo.Value = await Server.API.GetSystemStatus();
    async static void UpdateWeather() => Weather.Value = await WeatherUtility.Get();

}
