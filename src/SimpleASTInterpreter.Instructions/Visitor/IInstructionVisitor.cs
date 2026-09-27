using SimpleASTInterpreter.Instructions.Instructions;

namespace SimpleASTInterpreter.Instructions.Visitor;

public interface IInstructionVisitor
{
    void Visit(ReadInstruction node);

    void Visit(WriteInstruction node);

    void Visit(LdInstruction node);

    void Visit(StInstruction node);

    void Visit(ConstInstruction node);

    void Visit(BinopInstruction node);

    void Visit(LabelInstruction node);

    void Visit(JmpInstruction node);

    void Visit(JzInstruction node);

    void Visit(JnzInstruction node);
}