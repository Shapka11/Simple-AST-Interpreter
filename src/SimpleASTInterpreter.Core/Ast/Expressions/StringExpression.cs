using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class StringExpression : IExpression
{
    public StringExpression(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public TValue Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}