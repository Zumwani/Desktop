using System.Runtime.CompilerServices;
using Desktop.Config;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Note : ModelViewModel<Models.Note>
{

    public RelayCommand? ShowAllCommand { get; init; }
    public RelayCommand? HideAllCommand { get; init; }

    public RelayCommand ClickCommand { get; }
    public RelayCommand HideCommand { get; }
    public RelayCommand ShowCommand { get; }
    public RelayCommand RemoveCommand { get; }
    public RelayCommand CreateCommand { get; }

    public Config.Notes Config { get; } = ConfigManager.Notes;

    public bool IsVisible => Config.ShowHiddenNotes || !Model.IsHidden;

    public bool IsPopupOpen { get; set; }

    public Note()
    {

        CreateCommand = new(Config.CreateNote);

        ClickCommand = new(() => IsPopupOpen = true);
        HideCommand = new(() => Model.IsHidden = true);
        ShowCommand = new(() => Model.IsHidden = false);
        RemoveCommand = new(() => Config.Items.Remove(Model));

    }

    public override void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(Model))
            NotifyPropertyChangedServices.SignalPropertyChanged(this, nameof(IsVisible));
    }

}
