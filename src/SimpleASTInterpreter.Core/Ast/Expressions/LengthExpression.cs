using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class LengthExpression : IExpression
{
    public LengthExpression(IExpression expression)
    {
        Expression = expression;
    }
    
    public IExpression Expression { get; }

    public TValue Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}
