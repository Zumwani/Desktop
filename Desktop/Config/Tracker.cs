using System.Text.Json.Serialization;
using Desktop.Models;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Trackers : ConfigModule
{

    public Duration UpdateInterval { get; set; } = Duration.FromSeconds(2);
    [JsonInclude, SafeForDependencyAnalysis] private string? SelectedDeviceName { get; set; }

    [JsonIgnore]
    public string DeviceName => SelectedDeviceName ?? "No device selected...";

    private BluetoothDevice? device;
    [JsonIgnore, SafeForDependencyAnalysis]
    public BluetoothDevice? SelectedDevice
    {
        get
        {

            if (device?.Name == SelectedDeviceName)
                return device;
            else if (string.IsNullOrWhiteSpace(SelectedDeviceName))
                return null;
            else
                return device = IndicatorUtility.BluetoothDevices.Value!.Find(SelectedDeviceName);

        }
        set
        {
            device = value;
            SelectedDeviceName = device?.Name;
            OnPropertyChanged();
        }
    }

}
