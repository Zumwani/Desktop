using Common.Utility;

namespace Desktop.Converters;

public class InvertBool : BetterConverter<bool, bool>
{
    public override bool Convert(bool value) => !value;
    public override bool ConvertBack(bool value) => !value;
}