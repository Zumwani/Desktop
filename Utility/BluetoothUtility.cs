using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Desktop.Utility;

public static class BluetoothUtility
{

    static string? IP { get; }
    static int? Port { get; set; }

    static BluetoothUtility()
    {
        using var key = Registry.CurrentUser.OpenSubKey(@"Software\Luculent Systems\Bluetooth Battery Monitor\ApiServer");
        IP = key?.GetValue("ip") as string;
        Port = key?.GetValue("port") as int?;
    }

    public static async Task<IEnumerable<Device>> Get()
    {

        try
        {

            using var client = new HttpClient();

            var json = await client.GetStringAsync($"http://{IP}:{Port}/devices");
            var result = JsonSerializer.Deserialize<Result>(json);
            return result.Devices;

        }
        catch
        { }

        return Array.Empty<Device>();

    }

    public static async Task<Device?> Get(string? deviceName) =>
        (await Get()).FirstOrDefault(d => d.Name == deviceName);

    struct Result
    {
        [JsonPropertyName("devices")]
        public Device[] Devices { get; set; }
    }

    public struct Device
    {

        [JsonPropertyName("connected")]
        public bool IsConnected { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }

}
