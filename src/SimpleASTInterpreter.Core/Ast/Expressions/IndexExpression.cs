using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class IndexExpression : IExpression
{
    public IndexExpression(IExpression expression, IExpression index)
    {
        Expression = expression;
        Index = index;
    }

    public IExpression Expression { get; }

    public IExpression Index { get; }

    public TValue Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}