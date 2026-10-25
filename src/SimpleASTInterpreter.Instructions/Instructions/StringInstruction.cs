using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class StringInstruction : IInstruction
{
    public StringInstruction(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
