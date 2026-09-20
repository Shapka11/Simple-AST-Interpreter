using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class DoWhileStatement : LoopStatement
{
    public DoWhileStatement(IExpression condition, IStatement body) : base(condition, body)
    {
    }

    public override void Accept(INodeVisitor visitor) => visitor.Visit(this);
}
