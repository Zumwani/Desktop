using Common.Utility;
using Desktop.Config;
using Desktop.Models;

namespace Desktop.Commands;

public class RemoveNote : Command<Note?>
{

    public override void Execute(Note? note)
    {
        if (note is not null)
            _ = ConfigManager.Notes.Items.Remove(note);
    }

}
