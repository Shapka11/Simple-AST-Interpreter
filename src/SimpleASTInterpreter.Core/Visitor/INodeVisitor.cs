using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Ast.Statements;
using ConstantExpression = SimpleASTInterpreter.Core.Ast.Expressions.ConstantExpression;

namespace SimpleASTInterpreter.Core.Visitor;

public interface INodeVisitor
{
    long Visit(BinaryOperationExpression node);

    long Visit(ConstantExpression node);
    
    long Visit(IdentifierExpression node);

    void Visit(AssignmentStatement node);

    void Visit(SequenceStatement node);

    void Visit(WriteStatement node);

    void Visit(ReadStatement node);
    
    void Visit(IfStatement node);

    void Visit(SkipStatement node);
    
    void Visit(WhileStatement node);
    
    void Visit(DoWhileStatement node);
}