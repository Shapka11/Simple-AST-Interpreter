using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public sealed class LabelInstruction : IInstruction
{
    public LabelInstruction(string label)
    {
        Label = label;
    }

    public string Label { get; }

    public void Accept(IInstructionVisitor visitor) => visitor.Visit(this);
}
