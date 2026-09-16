using SimpleASTInterpretator.Core.Visitor;

namespace SimpleASTInterpretator.Core.Ast.Statements;

public sealed class ReadStatement : IStatement
{
    public ReadStatement(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }
    
    public void Accept(INodeVisitor visitor) => visitor.Visit(this);
}