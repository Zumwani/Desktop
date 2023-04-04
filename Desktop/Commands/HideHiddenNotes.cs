using Common.Utility;

namespace Desktop.Commands;

public class HideHiddenNotes : Command
{

    public override void Execute() =>
        Settings.ShowHiddenNotes.Current.Value = false;

}
