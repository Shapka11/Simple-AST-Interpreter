using SimpleASTInterpretator.Core.Visitor;

namespace SimpleASTInterpretator.Core.Ast.Expressions;

public sealed class BinaryOperationExpression : IExpression
{
    public BinaryOperationExpression(string operation, IExpression left, IExpression right)
    {
        Operation = operation;
        Left = left;
        Right = right;
    }

    public string Operation { get; }

    public IExpression Left { get; }

    public IExpression Right { get; }

    public long Accept(INodeVisitor visitor) => visitor.Visit(this);
}