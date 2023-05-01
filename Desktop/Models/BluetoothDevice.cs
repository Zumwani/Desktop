using System.Collections;
using System.Text.Json.Serialization;

namespace Desktop.Models;

public struct BluetoothDevice : IEqualityComparer
{

    [JsonPropertyName("connected")]
    public bool IsConnected { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }

    [JsonPropertyName("level")]
    public int BatteryLevel { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public new bool Equals(object? x, object? y) => x is not null && y is not null && ((BluetoothDevice)x).Name == ((BluetoothDevice)y).Name;

    public int GetHashCode(object obj) => Name.GetHashCode();

}
