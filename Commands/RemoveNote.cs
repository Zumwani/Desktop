using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class RemoveNote : Command<Note?>
{

    public override void Execute(Note? note)
    {
        if (note is not null)
            _ = Settings.Notes.Current.Remove(note);
    }
}
