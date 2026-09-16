using SimpleASTInterpretator.Core.Visitor;

namespace SimpleASTInterpretator.Core.Ast.Statements;

public interface IStatement : INode
{
    public void Accept(INodeVisitor visitor);
}