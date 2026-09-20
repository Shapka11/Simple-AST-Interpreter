using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class SkipStatement : IStatement
{
    public void Accept(INodeVisitor visitor) => visitor.Visit(this);
}
