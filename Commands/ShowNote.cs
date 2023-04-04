using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class ShowNote : Command<Note?>
{

    public override void Execute(Note? note)
    {
        if (note is not null)
            note.IsHidden = false;
    }

}