namespace Desktop.ViewModels;

public class SystemStatus
{

    public BluetoothBattery BluetoothBattery { get; } = new();
    public CpuUsage CpuUsage { get; } = new();
    public CpuTemperature CpuTemperature { get; } = new();
    public Memory Memory { get; } = new();

    #region Bluetooth device list

    //void MenuItem_Click(object sender, RoutedEventArgs e)
    //{

    //    if (sender is MenuItem item)
    //        Settings.BluetoothDevice.Current.Value = ((BluetoothDevice)item.DataContext).Name;

    //    ReloadBluetoothDeviceList();
    //    Reload();

    //}

    //void ContextMenu_Opened(object sender, RoutedEventArgs e) =>
    //    ReloadBluetoothDeviceList();

    //void ReloadBluetoothDeviceList()
    //{
    //    foreach (var item in BluetoothDeviceButton.ContextMenu.ItemContainerGenerator.Items.Select(BluetoothDeviceButton.ContextMenu.ItemContainerGenerator.ContainerFromItem).OfType<System.Windows.Controls.MenuItem>().ToArray())
    //        item.IsChecked = Settings.BluetoothDevice.Current.Value == ((BluetoothUtility.Device)item.DataContext).Name;
    //}

    #endregion

}
