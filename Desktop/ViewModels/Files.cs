using System.Collections.ObjectModel;
using Desktop.Models;
using Desktop.Utility;

namespace Desktop.ViewModels;

public class Files
{

    public ObservableCollection<FileItem> Items => FileUtility.Files;

}