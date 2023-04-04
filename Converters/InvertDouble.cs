using Common.Utility;

namespace Desktop.Converters;

public class InvertDouble : BetterConverter<double, double>
{
    public override double Convert(double value) => -value;
    public override double ConvertBack(double value) => -value;
}
