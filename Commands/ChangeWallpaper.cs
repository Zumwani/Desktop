using System;
using Common.Utility;

namespace Desktop.Commands;

public class ChangeWallpaper : Command
{

    static Action? Handler;

    public static void SetHandler(Action callback) =>
        Handler ??= callback;

    public override void Execute() =>
        Handler?.Invoke();

}
