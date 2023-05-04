using Common.Utility;
using Desktop.Config;

namespace Desktop.Commands;

public class HideHiddenNotes : Command
{

    public override void Execute() =>
        ConfigManager.Notes.ShowHiddenNotes = false;

}
