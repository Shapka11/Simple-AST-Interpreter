using SimpleASTInterpreter.Instructions.Instructions;
using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions;

public sealed class BinopInstruction : IInstruction
{
    public BinopInstruction(string operation)
    {
        Operation = operation;
    }

    public string Operation { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
