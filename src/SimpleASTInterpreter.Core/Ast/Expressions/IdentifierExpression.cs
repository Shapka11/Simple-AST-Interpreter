using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class IdentifierExpression : IExpression
{
    public IdentifierExpression(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }

    public TValue Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}