using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Expressions;

public interface IExpression : INode
{
    public long Accept(INodeVisitor visitor);
}