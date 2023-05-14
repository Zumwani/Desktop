using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Server.Models;

public class SystemInfo
{

    public float?[] Values { get; set; } = Enumerable.Repeat<float?>(null, 13).ToArray();

    [JsonIgnore]
    string[] Names { get; } = new[]
    {

        nameof(CPUTemp),
        nameof(CPULoad),
        nameof(CPUPower),

        nameof(GPUTemp),
        nameof(GPULoad),
        nameof(GPUMemoryLoad),
        nameof(GPUPower),
        nameof(GPUMemoryUsed),
        nameof(GPUMemoryTotal),

        nameof(BatteryChargeLevel),
        nameof(BatteryRemainingTime),

        nameof(RamUsed),
        nameof(RamTotal),

    };

    public float? GetValue([CallerMemberName] string? name = null) => Values[Array.IndexOf(Names, name!)];
    void SetValue(float? value, [CallerMemberName] string? name = null) => Values[Array.IndexOf(Names, name!)] = value;

    public override string ToString() =>
        string.Join("\n", Values.Select((value, i) => Names[i] + ": " + value));

    #region Properties

    [JsonIgnore]
    public float? CPUTemp
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? CPULoad
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? CPUPower
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? GPUTemp
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? GPULoad
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? GPUMemoryLoad
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? GPUPower
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? GPUMemoryUsed
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? GPUMemoryTotal
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? BatteryChargeLevel
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? BatteryRemainingTime
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? RamUsed
    {
        get => GetValue();
        set => SetValue(value);
    }

    [JsonIgnore]
    public float? RamTotal
    {
        get => GetValue();
        set => SetValue(value);
    }

    #endregion

}
