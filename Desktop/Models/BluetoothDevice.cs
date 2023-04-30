using System.Text.Json.Serialization;

namespace Desktop.Models;

public struct BluetoothDevice
{

    [JsonPropertyName("connected")]
    public bool IsConnected { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }

    [JsonPropertyName("level")]
    public int BatteryLevel { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

}
