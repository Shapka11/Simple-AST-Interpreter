using System;
using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class SexpInstruction : IInstruction
{
    public SexpInstruction(string name, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        Name = name;
        Count = count;
    }

    public string Name { get; }

    public int Count { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
