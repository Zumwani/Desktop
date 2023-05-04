using System;
using System.Collections.Generic;
using System.Threading;

namespace Desktop;

public static class ActionUtility
{

    static readonly Dictionary<Action, PeriodicTimer> actions = new();

    public static void Invoke(TimeSpan interval, Action action) =>
        Invoke(action, interval);

    public static async void Invoke(Action action, TimeSpan interval)
    {

        if (action is null || interval.TotalMilliseconds < 10)
            return;

        StopInvoke(action);
        actions.Add(action, new(interval));

        action.Invoke();
        while (await actions[action].WaitForNextTickAsync())
            action.Invoke();

    }

    public static void StopInvoke(Action action)
    {
        if (actions.Remove(action, out var timer))
            timer.Dispose();
    }

}
