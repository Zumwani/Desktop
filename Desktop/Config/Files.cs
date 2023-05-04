using System.Collections.ObjectModel;
using Desktop.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Files : ConfigModule
{

    public bool ShowRecycleBin { get; set; } = true;
    public bool ShowHome { get; set; } = true;
    public bool ShowThisPC { get; set; }
    public bool ShowDownloads { get; set; }

    [AggregateAllChanges]
    public ObservableCollection<string> Sources { get; set; } = new() { FileUtility.DesktopFolder, FileUtility.DownloadsFolder };

}
