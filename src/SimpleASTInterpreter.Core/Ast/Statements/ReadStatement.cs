using SimpleASTInterpreter.Core.Visitor;

namespace SimpleASTInterpreter.Core.Ast.Statements;

public sealed class ReadStatement : IStatement
{
    public ReadStatement(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }

    public void Accept<TValue>(INodeVisitor<TValue> visitor) => visitor.Visit(this);
}