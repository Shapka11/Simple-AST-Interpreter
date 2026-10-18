using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class ConstantExpression : IExpression
{
    public ConstantExpression(long value)
    {
        Value = value;
    }

    public long Value { get; }

    public TValue Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}