using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using Wpf.Ui.Common;

namespace Desktop.ViewModels.SettingPages;

class Files : SettingsPage<Config.Files>
{

    public override string Title => "Files";
    public override SymbolRegular Icon => SymbolRegular.Desktop32;

    public RelayCommand AddSourceCommand { get; }
    public RelayCommand<string> RemoveSourceCommand { get; }

    public Files()
    {

        AddSourceCommand = new(() =>
        {
            if (PickFolder(out var folder))
                Config.Sources.Add(folder);
        });

        RemoveSourceCommand = new(folder => _ = Config.Sources.Remove(folder!));

    }

    bool PickFolder([NotNullWhen(true)] out string? folder)
    {

        folder = null;
        var dialog = new CommonOpenFileDialog()
        {
            Title = "Pick folder...",
            IsFolderPicker = true,
            EnsurePathExists = true,
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            folder = dialog.FileName;

        return Directory.Exists(folder);

    }

}
