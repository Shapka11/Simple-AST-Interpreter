using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class JzInstruction : IInstruction
{
    public JzInstruction(string label)
    {
        Label = label;
    }

    public string Label { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
