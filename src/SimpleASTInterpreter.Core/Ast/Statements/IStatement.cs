using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public interface IStatement : INode
{
    public void Accept(INodeVisitor visitor);
}