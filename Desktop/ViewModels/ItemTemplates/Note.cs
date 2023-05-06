using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Note : ModelViewModel<Models.Note>
{

    public RelayCommand<Models.Note>? EditCommand { get; init; }
    public RelayCommand? ShowAllCommand { get; init; }
    public RelayCommand? HideAllCommand { get; init; }

    public RelayCommand ClickCommand { get; }
    public RelayCommand HideCommand { get; }
    public RelayCommand ShowCommand { get; }
    public RelayCommand RemoveCommand { get; }

    public Config.Notes Config { get; } = ConfigManager.Notes;

    public Note()
    {
        ClickCommand = new(() => EditCommand?.Execute(Model));
        HideCommand = new(() => Model.IsHidden = true);
        ShowCommand = new(() => Model.IsHidden = false);
        RemoveCommand = new(() => Config.Items.Remove(Model));
    }

}
