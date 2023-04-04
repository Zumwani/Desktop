using Common.Utility;

namespace Desktop.Commands;

public class ShowHiddenNotes : Command
{

    public override void Execute() =>
        Settings.ShowHiddenNotes.Current.Value = true;

}
