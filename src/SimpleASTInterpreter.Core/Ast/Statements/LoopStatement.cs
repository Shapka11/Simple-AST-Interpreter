using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public abstract class LoopStatement : IStatement
{
    protected LoopStatement(IExpression condition, IStatement body)
    {
        Condition = condition;
        Body = body;
    }

    public IExpression Condition { get; init; }

    public IStatement Body { get; init; }

    public abstract void Accept<TValue>(INodeVisitor<TValue> visitor);
}