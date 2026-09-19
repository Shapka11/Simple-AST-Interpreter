using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class WhileStatement : IStatement
{
    
    public IExpression Condition { get; init; }
    
    public IStatement Body { get; init;  }
    
    public WhileStatement(IExpression condition, IStatement body)
    {
        Condition = condition;
        Body = body;
    }

    public void Accept(INodeVisitor visitor) => visitor.Visit(this);
}