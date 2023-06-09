﻿using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class FileItem : ModelViewModel<Models.FileItem>
{

    public RelayCommand OpenCommand { get; }
    public RelayCommand OpenWithCommand { get; }
    public RelayCommand OpenFolderCommand { get; }
    public RelayCommand<FileItem>? RenameCommand { get; set; }
    public RelayCommand DeleteCommand { get; }

    public FileItem()
    {
        OpenCommand = new(() => FileUtility.Open(Model.Path));
        OpenWithCommand = new(() => FileUtility.OpenWith(Model.Path));
        OpenFolderCommand = new(() => FileUtility.OpenFolder(Model.Path));
        DeleteCommand = new(() => FileUtility.Delete(Model.Path));
    }

}
