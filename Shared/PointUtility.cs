using System.Windows;

namespace Desktop;

public static class PointUtility
{

    public static bool InRange(this double value, double targetValue, double range) =>
        value > targetValue - range && value < targetValue + range;

    public static bool InRange(this Point point, Point target, double range) =>
           point.X.InRange(target.X, range) && point.Y.InRange(target.Y, range);

}