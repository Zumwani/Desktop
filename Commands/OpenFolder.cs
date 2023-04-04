using System.Diagnostics;
using System.IO;
using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class OpenFolder : Command<FileItem?>
{

    public override void Execute(FileItem? file)
    {

        if (!file.HasValue)
            return;

        if (File.Exists(file.Value.Path) || Directory.Exists(file.Value.Path))
            _ = Process.Start("explorer", "/select,\"" + file.Value.Path + "\"");

    }

}
