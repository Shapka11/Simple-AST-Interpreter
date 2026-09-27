using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class ReadInstruction : IInstruction
{
    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
