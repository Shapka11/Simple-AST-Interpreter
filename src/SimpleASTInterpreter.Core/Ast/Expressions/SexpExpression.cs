using System.Collections.Generic;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public sealed class SexpExpression : IExpression
{
    public SexpExpression(string name, IReadOnlyList<IExpression> arguments)
    {
        Name = name;
        Arguments = arguments;
    }

    public string Name { get; }

    public IReadOnlyList<IExpression> Arguments { get; }

    public TValue Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}
