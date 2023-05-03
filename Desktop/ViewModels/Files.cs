using System.Collections.ObjectModel;
using System.Linq;
using Desktop.Config;
using Desktop.Models;
using Desktop.Utility;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Files : ViewModel
{

    public bool HasItems => Config.ShowRecycleBin || Items.Any();

    public Config.Files Config { get; } = ConfigManager.Files;
    public ObservableCollection<FileItem> Items => FileUtility.Files;

    public Files()
    {
        Items.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasItems));
        Config.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HasItems));
    }

}