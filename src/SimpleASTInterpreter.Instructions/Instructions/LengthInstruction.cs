using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class LengthInstruction : IInstruction
{
    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
