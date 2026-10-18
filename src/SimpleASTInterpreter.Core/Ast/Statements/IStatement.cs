using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public interface IStatement : INode
{
    public void Accept<TValue>(INodeVisitor<TValue> visitor);
}