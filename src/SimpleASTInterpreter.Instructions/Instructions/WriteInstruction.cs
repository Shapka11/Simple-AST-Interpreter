using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class WriteInstruction : IInstruction
{
    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
