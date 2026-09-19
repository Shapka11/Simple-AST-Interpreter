using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class ConstantExpression : IExpression
{
    public ConstantExpression(long value)
    {
        Value = value;
    }

    public long Value { get; }

    public long Accept(INodeVisitor visitor) => visitor.Visit(this);
}