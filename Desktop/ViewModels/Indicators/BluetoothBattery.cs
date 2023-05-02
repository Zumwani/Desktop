using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Desktop.Models;
using Desktop.Utility;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class BluetoothBattery : IntervalViewModel
{

    public double? Value { get; private set; }

    public IEnumerable<BluetoothDevice> AvailableDevices { get; private set; } = Array.Empty<BluetoothDevice>();

    [SafeForDependencyAnalysis]
    public BluetoothDevice SelectedDevice => AvailableDevices.FirstOrDefault(d => d.Name == SelectedDeviceName);
    public Desktop.Settings.BluetoothDevice SelectedDeviceName { get; } = Desktop.Settings.BluetoothDevice.Current;

    public RelayCommand OpenAPIPageCommand { get; } = new(() => FileUtility.Open("https://www.bluetoothgoodies.com/info/battery-monitor-api/"));
    public RelayCommand OpenMainPageCommand { get; } = new(() => FileUtility.Open("https://www.bluetoothgoodies.com/"));

    public RelayCommand<MenuItem> PickDeviceCommand { get; }

    public BluetoothBattery()
    {

        PickDeviceCommand = new(menuItem =>
        {

            if (menuItem is null || menuItem.DataContext is not BluetoothDevice device || menuItem.Tag is not MenuItem parent)
                return;

            SelectedDeviceName.Value = device.Name;
            var items = parent.Items.Cast<BluetoothDevice>().Select(d => parent.ItemContainerGenerator.ContainerFromItem(d)).OfType<MenuItem>().ToArray();
            foreach (var item in items)
                item.IsChecked = false;
            menuItem.IsChecked = true;

        });

        SelectedDeviceName.PropertyChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(SelectedDevice));
            Helper.BluetoothDevices.Update();
            Update();
        };

    }

    public override void Update()
    {

        Value = Helper.BluetoothDevices.Value?.Find(SelectedDeviceName)?.BatteryLevel;

        var devices = Helper.BluetoothDevices.Value;
        if (!(devices?.SequenceEqual(AvailableDevices) ?? false))
            AvailableDevices = Helper.BluetoothDevices.Value ?? Array.Empty<BluetoothDevice>();

    }

}
