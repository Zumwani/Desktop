using Common.Utility;

namespace Desktop.Converters;

public class HasValue : BetterConverter<object, bool>
{

    public override bool Convert(object? value) =>
        !IsDefault.GetBool(value);

}
