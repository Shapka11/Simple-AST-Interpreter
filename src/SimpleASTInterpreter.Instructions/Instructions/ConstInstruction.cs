using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class ConstInstruction : IInstruction
{
    public ConstInstruction(long value)
    {
        Value = value;
    }

    public long Value { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
