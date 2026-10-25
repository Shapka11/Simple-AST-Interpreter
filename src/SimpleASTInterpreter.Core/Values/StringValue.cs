using System;
using System.Text;

namespace SimpleASTInterpreter.Core.Values;

public sealed class StringValue : RuntimeValue
{
    private readonly byte[] _bytes;

    public StringValue(string value)
    {
        _bytes = Encoding.UTF8.GetBytes(value);
    }
    
    public string Value => Encoding.UTF8.GetString(_bytes);

    public override int Length => _bytes.Length;

    public override RuntimeValue GetElement(long index) => new NumberValue(_bytes[CheckIndex(index, Length)]);

    public override void SetElement(long index, RuntimeValue value)
    {
        int checkedIndex = CheckIndex(index, Length);
        long code = value.AsNumber();
        if (code < byte.MinValue || code > byte.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "A character code must be between 0 and 255.");
        }

        _bytes[checkedIndex] = (byte)code;
    }

    public override string ToString() => "\"" + Value + "\"";
}
