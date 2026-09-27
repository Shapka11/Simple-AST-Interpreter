using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleASTInterpreter.Instructions.Instructions;

public interface IInstruction
{
    void Accept(IInstructionVisitor visitor);
}
