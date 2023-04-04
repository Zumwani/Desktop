using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Desktop.Models;
using Desktop.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.Views;

[NotifyPropertyChanged]
public partial class Files : UserControl
{

    public Files()
    {
        InitializeComponent();
        InitializeDragDrop();
    }

    void CreateFolderItem_Click(object sender, RoutedEventArgs e) => FilePopup.CreateFolder();
    void CreateFileItem_Click(object sender, RoutedEventArgs e) => FilePopup.CreateFile();

    void RenameItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem item && item.DataContext is FileItem file)
            FilePopup.Rename(file.Path);
    }

    #region DragDrop

    async void InitializeDragDrop()
    {

        while (Window.GetWindow(this) is null)
            await Task.Delay(100);

        var window = Window.GetWindow(this);
        window.AllowDrop = true;

        DragDrop.AddDragEnterHandler(window, (s, e) =>
        {
            e.Effects =
                e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Move
                : DragDropEffects.None;
        });

        DragDrop.AddDropHandler(window, (s, e) => FilePopup.HandleDragDrop((string[])e.Data.GetData(DataFormats.FileDrop)));

    }

    #endregion

    void OpenWithItem_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem item && item.DataContext is FileItem file)
            item.IsEnabled = File.Exists(file.Path);
    }

}

public class FilesExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider) =>
        FileUtility.Files;
}
