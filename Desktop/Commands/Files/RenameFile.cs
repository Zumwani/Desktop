using System;
using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class RenameFile : Command<FileItem?>
{

    public static event Action<FileItem>? Callback;

    public override void Execute(FileItem? file)
    {

        if (file.HasValue)
            Callback?.Invoke(file.Value);

    }

}