using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Note : ModelViewModel<Models.Note>
{

    public RelayCommand ClickCommand { get; }
    public RelayCommand<Models.Note> EditCommand { get; init; } = null!;
    public RelayCommand HideCommand { get; }
    public RelayCommand ShowCommand { get; }
    public RelayCommand RemoveCommand { get; }

    public RelayCommand ShowAllCommand { get; }
    public RelayCommand HideAllCommand { get; }

    public Config.Notes Config { get; } = ConfigManager.Notes;

    public Note()
    {
        ClickCommand = new(() => EditCommand.Execute(Model));
        HideCommand = new(() => Model.IsHidden = true);
        ShowCommand = new(() => Model.IsHidden = false);
        RemoveCommand = new(() => Config.Items.Remove(Model));
        ShowAllCommand = new(() => Config.ShowHiddenNotes = true);
        HideAllCommand = new(() => Config.ShowHiddenNotes = false);
    }

}
