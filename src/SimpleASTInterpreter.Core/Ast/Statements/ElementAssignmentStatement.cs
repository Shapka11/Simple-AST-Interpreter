using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class ElementAssignmentStatement : IStatement
{
    public ElementAssignmentStatement(IndexExpression destination, IExpression expression)
    {
        Destination = destination;
        Expression = expression;
    }

    public IndexExpression Destination { get; }

    public IExpression Expression { get; }

    public void Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}