using SimpleASTInterpretator.Core.Ast.Expressions;
using SimpleASTInterpretator.Core.Visitor;

namespace SimpleASTInterpretator.Core.Ast.Statements;

public sealed class AssignmentStatement : IStatement
{
    public AssignmentStatement(string identifier, IExpression expression)
    {
        Identifier = identifier;
        Expression = expression;
    }

    public string Identifier { get; }
    
    public IExpression Expression { get; }

    public void Accept(INodeVisitor visitor) => visitor.Visit(this);
}