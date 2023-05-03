using System.Collections.ObjectModel;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Files : ConfigModule
{
    public bool ShowRecycleBin { get; set; }
    public ObservableCollection<string> Sources { get; set; } = new();
}
