using Common.Utility;
using Desktop.Models;
using Desktop.Views.Popups;

namespace Desktop.Commands;

public class EditNote : Command<Note>
{

    static readonly NotePopup popup = new();

    public override void Execute(Note? note) =>
        popup.Open(note);

}