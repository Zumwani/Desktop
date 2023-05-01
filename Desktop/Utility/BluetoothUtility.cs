using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Desktop.Models;
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

    public static async Task<IEnumerable<BluetoothDevice>> Get()
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

        return Array.Empty<BluetoothDevice>();

    }

    public static BluetoothDevice? Find(this IEnumerable<BluetoothDevice> list, string? deviceName) =>
        list.Where(d => d.IsConnected).Cast<BluetoothDevice?>().FirstOrDefault(d => d?.Name == deviceName);

    struct Result
    {
        [JsonPropertyName("devices")]
        public BluetoothDevice[] Devices { get; set; }
    }

}
