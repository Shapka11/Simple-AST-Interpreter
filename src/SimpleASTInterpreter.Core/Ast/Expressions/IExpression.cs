using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public interface IExpression : INode
{
    public TValue Accept<TValue>(INodeVisitor<TValue> visitor);
}