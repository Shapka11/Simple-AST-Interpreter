using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class WhileStatement : LoopStatement
{
    public WhileStatement(IExpression condition, IStatement body) : base(condition, body)
    {
    }

    public override void Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}