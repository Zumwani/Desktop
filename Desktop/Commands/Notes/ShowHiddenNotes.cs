using Common.Utility;
using Desktop.Config;

namespace Desktop.Commands;

public class ShowHiddenNotes : Command
{

    public override void Execute() =>
        ConfigManager.Notes.ShowHiddenNotes = true;

}
