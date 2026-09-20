using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public abstract class LoopStatement : IStatement
{
    public IExpression Condition { get; init; }

    public IStatement Body { get; init; }

    protected LoopStatement(IExpression condition, IStatement body)
    {
        Condition = condition;
        Body = body;
    }

    public abstract void Accept(INodeVisitor visitor);
}
