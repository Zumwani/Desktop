using System.Collections.ObjectModel;
using System.Linq;
using Desktop.Commands;
using Desktop.Config;
using Desktop.Models;
using Desktop.Utility;
using Desktop.ViewModels.Helpers;
using Desktop.Views.Popups;

namespace Desktop.ViewModels;

public class Files : ViewModel
{

    static Files? instance;

    public bool HasItems => Config.ShowRecycleBin || Items.Any();

    public Config.Files Config { get; } = ConfigManager.Files;
    public ObservableCollection<FileItem> Items => FileUtility.Files;

    public RelayCommand OpenHome { get; }
    public RelayCommand OpenThisPC { get; }
    public RelayCommand OpenDownloads { get; }
    public RelayCommand OpenRecycleBin { get; }

    public RelayCommand CreateFileCommand { get; }
    public RelayCommand CreateFolderCommand { get; }

    FilePopup Popup { get; } = new();

    static Files() =>
        RenameFile.Callback += file => instance?.Popup?.Rename(file.Path);

    public Files()
    {

        instance = this;

        Items.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasItems));
        Config.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HasItems));

        OpenHome = new(() => FileUtility.Open("explorer", "shell:::{F874310E-B6B7-47DC-BC84-B9E6B38F5903}"));
        OpenThisPC = new(() => FileUtility.Open("explorer", "/root,"));
        OpenDownloads = new(() => FileUtility.Open("shell:Downloads"));
        OpenRecycleBin = new(() => FileUtility.Open("shell:RecycleBinFolder"));

        CreateFileCommand = new(Popup.CreateFile);
        CreateFolderCommand = new(Popup.CreateFolder);

    }

}