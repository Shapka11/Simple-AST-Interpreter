using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class IfStatement : IStatement
{
    public IfStatement(IExpression condition, IStatement then, IStatement? @else)
    {
        Else = @else ?? new SkipStatement();
        Then = then;
        Condition = condition;
    }

    public IExpression Condition { get; init; }

    public IStatement Then { get; init; }

    public IStatement Else { get; init; }

    public void Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}