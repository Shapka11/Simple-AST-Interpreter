using SimpleASTInterpretator.Core.Ast.Expressions;
using SimpleASTInterpretator.Core.Visitor;

namespace SimpleASTInterpretator.Core.Ast.Statements;

public sealed class WriteStatement : IStatement
{
    public WriteStatement(IExpression expression)
    {
        Expression = expression;
    }

    public IExpression Expression { get; }

    public void Accept(INodeVisitor visitor) => visitor.Visit(this);
}