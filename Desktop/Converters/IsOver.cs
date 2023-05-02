using Common.Utility;

namespace Desktop.Converters;

public class IsOver : BetterConverter<int, bool, int>
{

    public override bool Convert(int value, int param) =>
        value > param;

}