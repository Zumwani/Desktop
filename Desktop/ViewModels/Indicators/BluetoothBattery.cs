using System;
using System.Collections.Generic;
using Desktop.Models;
using Desktop.Utility;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class BluetoothBattery : IntervalViewModel
{

    public double? Value { get; private set; }
    public IEnumerable<BluetoothDevice> AvailableDevices { get; } = Array.Empty<BluetoothDevice>();

    public BluetoothBattery()
    {
        AvailableDevices = new List<BluetoothDevice>();
    }

    public override async void Update() =>
        Value = (await BluetoothUtility.Get(Settings.BluetoothDevice.Current.Value))?.BatteryLevel;

}