using SimpleASTInterpretator.Core.Visitor;

namespace SimpleASTInterpretator.Core.Ast.Expressions;

public sealed class ConstantExpression : IExpression
{
    public ConstantExpression(long value)
    {
        Value = value;
    }

    public long Value { get; }

    public long Accept(INodeVisitor visitor) => visitor.Visit(this);
}