using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class BinopInstruction : IInstruction
{
    public BinopInstruction(string operation)
    {
        Operation = operation;
    }

    public string Operation { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
