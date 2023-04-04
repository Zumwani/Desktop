using System.IO;
using System.Threading.Tasks;
using Common.Utility;
using Desktop.Models;
using Microsoft.VisualBasic.FileIO;

namespace Desktop.Commands;

public class Delete : Command<FileItem?>
{

    public override void Execute(FileItem? file)
    {

        if (file.HasValue)
            _ = Task.Run(() =>
            {

                if (File.Exists(file?.Path))
                    FileSystem.DeleteFile(file.Value.Path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                else if (Directory.Exists(file?.Path))
                    FileSystem.DeleteDirectory(file.Value.Path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);

            });

    }

}
