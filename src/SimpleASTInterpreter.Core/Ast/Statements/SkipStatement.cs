using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class SkipStatement : IStatement
{
    public void Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}