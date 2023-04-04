using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class HideNote : Command<Note?>
{

    public override void Execute(Note? note)
    {
        if (note is not null)
            note.IsHidden = true;
    }

}
