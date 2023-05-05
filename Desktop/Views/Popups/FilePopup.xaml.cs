using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.FileIO;
using Microsoft.WindowsAPICodePack.Shell;
using PostSharp.Patterns.Model;

namespace Desktop.Views.Popups;

[NotifyPropertyChanged]
public partial class FilePopup : Popup
{

    public FilePopup() =>
        InitializeComponent();

    #region Popup

    protected override async void OnOpened(EventArgs e)
    {
        _ = TextBox.Focus();
        FocusManager.SetFocusedElement(this, TextBox);
        await Task.Delay(10);
        TextBox.SelectAll();
    }

    void Button_Click(object sender, RoutedEventArgs e) =>
        Apply();

    void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && Validate())
            Apply();
    }

    void TextBox_TextChanged(object sender, TextChangedEventArgs e) =>
        _ = Validate();

    #endregion

    public enum ModeEnum
    {
        None, CreateFile, CreateFolder, RenameFile, RenameFolder, Drop
    }

    public enum ParentFolderEnum
    {
        Desktop, Downloads
    }

    public enum DropModeEnum
    {
        Move, Copy
    }

    [SafeForDependencyAnalysis]
    public string ParentPath =>
        ParentFolder is ParentFolderEnum.Desktop
        ? KnownFolders.Desktop.Path
        : KnownFolders.Downloads.Path;

    [SafeForDependencyAnalysis]
    public string SuggestedPath =>
        Path.Combine(ParentPath, SelectedName);

    [SafeForDependencyAnalysis] public ParentFolderEnum ParentFolder => (ParentFolderEnum)ParentFolderIndex;
    [SafeForDependencyAnalysis] public DropModeEnum DropMode => (DropModeEnum)DropModeIndex;

    public int ParentFolderIndex { get; set; }
    public int DropModeIndex { get; set; }

    public ModeEnum Mode { get; private set; }

    public bool IsDropMode => Mode is ModeEnum.Drop;
    public bool IsFolder => Mode is ModeEnum.CreateFolder or ModeEnum.RenameFolder;

    public new bool IsOpen => base.IsOpen;

    public string SelectedName { get; set; } = string.Empty;

    bool Validate()
    {

        if (validate is null)
            return true;

        var isEmpty = string.IsNullOrWhiteSpace(SelectedName);
        var hasInvalidChars = Path.GetInvalidFileNameChars().Any(SelectedName.Contains);

        var isValid = !isEmpty && !hasInvalidChars && validate.Invoke();
        Button.IsEnabled = isValid;
        TextBox.BorderBrush = isValid ? Brushes.Transparent : Brushes.Red;

        return isValid;

    }

    void Apply()
    {
        try
        {
            apply.Invoke();
            base.IsOpen = false;
        }
        catch (Exception e)
        {
            _ = MessageBox.Show(e.ToString(), e.GetType().Name);
        }
    }

    Func<bool>? validate;
    Action apply = null!;
    void Open(ModeEnum mode, Action apply, Func<bool>? validate = null)
    {

        Mode = mode;
        ParentFolderIndex = 0;
        DropModeIndex = 0;
        TextBox.Text = string.Empty;
        Button.IsEnabled = validate is null;
        TextBox.BorderBrush = Brushes.Transparent;

        this.validate = validate;
        this.apply = apply;

        base.IsOpen = true;

    }

    #region Create

    public void CreateFile() =>
        Open(ModeEnum.CreateFile, OnCreateFile, ValidateFile);

    public void CreateFolder() =>
        Open(ModeEnum.CreateFolder, OnCreateFolder, ValidateFolder);

    void OnCreateFile() => File.Create(SuggestedPath);
    void OnCreateFolder() => Directory.CreateDirectory(SuggestedPath);

    bool ValidateFile() => !File.Exists(SuggestedPath);
    bool ValidateFolder() => !Directory.Exists(SuggestedPath);

    #endregion
    #region Rename

    public void Rename(string? path)
    {

        if (string.IsNullOrEmpty(path))
            return;

        SelectedName = Path.GetFileName(path);

        if (File.Exists(path))
            Open(ModeEnum.RenameFile, RenameFile, ValidateFile);
        else if (Directory.Exists(path))
            Open(ModeEnum.RenameFolder, RenameFolder, ValidateFolder);

        bool ValidateFile() => SuggestedPath != path && !File.Exists(SuggestedPath);
        bool ValidateFolder() => SuggestedPath != path && !Directory.Exists(SuggestedPath);

        void RenameFile() => new Computer().FileSystem.RenameFile(path, SelectedName);
        void RenameFolder() => new Computer().FileSystem.RenameDirectory(path, SelectedName);

    }

    #endregion
    #region DragDrop

    public void HandleDragDrop(string[] paths)
    {

        Open(ModeEnum.Drop, DropFiles);

        void DropFiles()
        {
            foreach (var path in paths)
            {
                SelectedName = Path.GetFileName(path);
                if (DropMode is DropModeEnum.Copy)
                    Copy(path);
                else if (DropMode is DropModeEnum.Move)
                    Move(path);
            }
        }

        void Copy(string path)
        {
            if (File.Exists(path))
                new Computer().FileSystem.CopyFile(path, SuggestedPath, UIOption.AllDialogs, UICancelOption.DoNothing);
            else if (Directory.Exists(path))
                new Computer().FileSystem.CopyDirectory(path, SuggestedPath, UIOption.AllDialogs, UICancelOption.DoNothing);
        }

        void Move(string path)
        {
            if (File.Exists(path))
                new Computer().FileSystem.MoveFile(path, SuggestedPath, UIOption.AllDialogs, UICancelOption.DoNothing);
            else if (Directory.Exists(path))
                new Computer().FileSystem.MoveDirectory(path, SuggestedPath, UIOption.AllDialogs, UICancelOption.DoNothing);
        }

    }

    #endregion

}
