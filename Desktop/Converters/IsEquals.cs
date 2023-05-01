using Common.Utility;

namespace Desktop.Converters;

public class IsEquals : BetterConverter<object, bool, object>
{

    public override bool Convert(object? value, object? parameter) =>
        value == parameter;

}