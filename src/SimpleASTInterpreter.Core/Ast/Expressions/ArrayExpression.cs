using System.Collections.Generic;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class ArrayExpression : IExpression
{
    public ArrayExpression(IReadOnlyList<IExpression> elements)
    {
        Elements = elements;
    }

    public IReadOnlyList<IExpression> Elements { get; }

    public TValue Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}