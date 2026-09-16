using SimpleASTInterpretator.Core.Visitor;

namespace SimpleASTInterpretator.Core.Ast.Expressions;

public interface IExpression : INode
{
    public long Accept(INodeVisitor visitor);
}