using SimpleASTInterpretator.Core.Ast.Expressions;
using SimpleASTInterpretator.Core.Ast.Statements;
using ConstantExpression = SimpleASTInterpretator.Core.Ast.Expressions.ConstantExpression;

namespace SimpleASTInterpretator.Core.Visitor;

public interface INodeVisitor
{
    long Visit(BinaryOperationExpression node);

    long Visit(ConstantExpression node);
    
    long Visit(IdentifierExpression node);

    void Visit(AssignmentStatement node);

    void Visit(SequenceStatement node);

    void Visit(WriteStatement node);

    void Visit(ReadStatement node);
}