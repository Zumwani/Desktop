using System.Collections.ObjectModel;
using System.Linq;
using Desktop.Config;
using Desktop.ViewModels.Helpers;
using Desktop.Views.Popups;

namespace Desktop.ViewModels;

public class Files : ViewModel
{

    public bool HasItems => Config.ShowRecycleBin || Config.ShowHome || Config.ShowDownloads || Config.ShowThisPC || Items.Any();

    public Config.Files Config { get; } = ConfigManager.Files;
    public ObservableCollection<FileItem> Items { get; } = new();

    public RelayCommand OpenHome { get; }
    public RelayCommand OpenThisPC { get; }
    public RelayCommand OpenDownloads { get; }
    public RelayCommand OpenRecycleBin { get; }

    public RelayCommand CreateFileCommand { get; }
    public RelayCommand CreateFolderCommand { get; }

    public RelayCommand<FileItem> RenameFileCommand { get; }

    FilePopup Popup { get; } = new();

    public Files()
    {

        Items.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasItems));
        Config.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HasItems));

        OpenHome = new(() => FileUtility.Open("explorer", "shell:::{F874310E-B6B7-47DC-BC84-B9E6B38F5903}"));
        OpenThisPC = new(() => FileUtility.Open("explorer", "/root,"));
        OpenDownloads = new(() => FileUtility.Open("shell:Downloads"));
        OpenRecycleBin = new(() => FileUtility.Open("shell:RecycleBinFolder"));

        CreateFileCommand = new(Popup.CreateFile);
        CreateFolderCommand = new(Popup.CreateFolder);
        RenameFileCommand = new(file => Popup.Rename(file?.Model.Path));

        FileUtility.Files.OnAdded(file => Items.Add(new() { Model = file, RenameCommand = RenameFileCommand }));
        FileUtility.Files.OnRemoved(file => Items.Remove(Items.First(i => i.Model.Path == file.Path)));
        FileUtility.Files.OnClear(Items.Clear);

    }

}