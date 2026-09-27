using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class StInstruction : IInstruction
{
    public StInstruction(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
