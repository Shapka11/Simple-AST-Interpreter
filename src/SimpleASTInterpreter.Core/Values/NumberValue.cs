using System.Globalization;

namespace SimpleASTInterpreter.Core.Values;

public sealed class NumberValue : RuntimeValue
{
    public NumberValue(long value)
    {
        Value = value;
    }

    public long Value { get; }

    public override long AsNumber() => Value;

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}