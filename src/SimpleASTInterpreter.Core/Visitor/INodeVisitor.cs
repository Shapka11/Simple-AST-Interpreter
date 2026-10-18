using System;
using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Ast.Statements;

namespace SimpleASTInterpreter.Core.Visitor;

public interface INodeVisitor : INodeVisitor<long>;

public interface INodeVisitor<TValue>
{
    TValue Visit(BinaryOperationExpression node);
    TValue Visit(ConstantExpression node);
    TValue Visit(IdentifierExpression node);

    TValue Visit(StringExpression node) =>
        throw new NotSupportedException("Strings are not supported by this visitor.");

    TValue Visit(ArrayExpression node) => throw new NotSupportedException("Arrays are not supported by this visitor.");
    TValue Visit(LengthExpression node) => throw new NotSupportedException("Length is not supported by this visitor.");
    TValue Visit(IndexExpression node) => throw new NotSupportedException("Indexing is not supported by this visitor.");

    void Visit(AssignmentStatement node);
    void Visit(SequenceStatement node);
    void Visit(WriteStatement node);
    void Visit(ReadStatement node);
    void Visit(IfStatement node);
    void Visit(SkipStatement node);
    void Visit(WhileStatement node);
    void Visit(DoWhileStatement node);

    void Visit(ElementAssignmentStatement node) =>
        throw new NotSupportedException("Element assignment is not supported by this visitor.");
}