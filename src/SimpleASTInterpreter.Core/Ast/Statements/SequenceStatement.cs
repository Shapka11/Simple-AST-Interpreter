using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class SequenceStatement : IStatement
{
    public SequenceStatement(IStatement left, IStatement right)
    {
        Left = left;
        Right = right;
    }

    public IStatement Left { get; }

    public IStatement Right { get; }

    public void Accept(INodeVisitor visitor) => visitor.Visit(this);
}