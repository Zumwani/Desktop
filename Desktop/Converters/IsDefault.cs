using System.Collections;
using Common.Utility;

namespace Desktop.Converters;

public class IsDefault : BetterConverter<object, bool>
{

    public override bool Convert(object? value) =>
        IsDefault.GetBool(value);

    public static bool GetBool(object? obj)
    {
        if (obj is string str)
            return string.IsNullOrWhiteSpace(str);
        else if (obj is IList list)
            return list.Count == 0;
        else
            return obj == default;
    }

}