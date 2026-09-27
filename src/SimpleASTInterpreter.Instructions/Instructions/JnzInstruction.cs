using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class JnzInstruction : IInstruction
{
    public JnzInstruction(string label)
    {
        Label = label;
    }

    public string Label { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
