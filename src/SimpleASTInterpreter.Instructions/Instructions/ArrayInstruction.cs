using System;
using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class ArrayInstruction : IInstruction
{
    public ArrayInstruction(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        Count = count;
    }

    public int Count { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
