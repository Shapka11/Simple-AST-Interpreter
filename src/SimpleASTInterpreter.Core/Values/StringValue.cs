namespace SimpleASTInterpreter.Core.Values;

public sealed class StringValue : RuntimeValue
{
    public StringValue(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public override int Length => Value.Length;

    public override RuntimeValue GetElement(long index) => new StringValue(Value[CheckIndex(index, Length)].ToString());

    public override string ToString() => Value;
}