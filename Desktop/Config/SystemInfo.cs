using System;
using System.Text.Json.Serialization;
using Desktop.Models;
using Desktop.Utility;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class SystemInfo : ConfigModule
{

    public TimeSpan UpdateInterval { get; set; } = TimeSpan.FromSeconds(2);
    [JsonInclude] private string? SelectedDeviceName { get; set; }

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
                return device = Helper.BluetoothDevices.Value!.Find(SelectedDeviceName);

        }
        set
        {
            device = value;
            SelectedDeviceName = device?.Name;
            OnPropertyChanged();
        }
    }

}
