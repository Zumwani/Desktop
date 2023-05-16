using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Server.Models;

public class SystemInfo
{

    public string?[] Values { get; set; } = Enumerable.Repeat<string?>(null, Names.Length).ToArray();

    static string[] Names { get; } = new string[17]
    {

        nameof(BatteryChargeLevel),
        nameof(BatteryRemainingTime),

        nameof(CPUTemp),
        nameof(CPULoad),
        nameof(CPUPower),
        nameof(RamUsed),
        nameof(RamTotal),

        nameof(GPUTemp),
        nameof(GPULoad),
        nameof(GPUPower),
        nameof(GPUMemoryUsed),
        nameof(GPUMemoryTotal),

        nameof(NetworkLoad),
        nameof(NetworkDataUploaded),
        nameof(NetworkDataDownloaded),
        nameof(NetworkThroughputUpload),
        nameof(NetworkThroughputDownload),

    };

    public string? GetValue([CallerMemberName] string? name = null) => Values[Array.IndexOf(Names, name!)];
    void SetValue(string? value, [CallerMemberName] string? name = null) => Values[Array.IndexOf(Names, name!)] = value;

    public override string ToString() =>
        string.Join("\n", Values.Select((value, i) => Names[i] + ": " + value));

    #region Properties [18]

    #region Battery [2]

    [JsonIgnore]
    public string? BatteryChargeLevel
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? BatteryRemainingTime
    {
        get => GetValue();
        set => SetValue(value);
    }

    #endregion
    #region CPU [5]

    [JsonIgnore]
    public string? CPUTemp
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? CPULoad
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? CPUPower
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? RamUsed
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? RamTotal
    {
        get => GetValue();
        set => SetValue(value);
    }

    #endregion
    #region GPU [5]

    [JsonIgnore]
    public string? GPUTemp
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? GPULoad
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? GPUPower
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? GPUMemoryUsed
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? GPUMemoryTotal
    {
        get => GetValue();
        set => SetValue(value);
    }

    #endregion
    #region Network [5]

    [JsonIgnore]
    public string? NetworkLoad
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? NetworkDataUploaded
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? NetworkDataDownloaded
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? NetworkThroughputUpload
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public string? NetworkThroughputDownload
    {
        get => GetValue();
        set => SetValue(value);
    }

    #endregion

    #endregion

}
