using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class WriteStatement : IStatement
{
    public WriteStatement(IExpression expression)
    {
        Expression = expression;
    }

    public IExpression Expression { get; }

    public void Accept(INodeVisitor visitor) => visitor.Visit(this);
}