using System;
using System.Collections.Generic;
using SimpleASTInterpretator.Core.Ast.Expressions;
using SimpleASTInterpretator.Core.Ast.Statements;
using SimpleASTInterpretator.Core.Evaluators;

namespace SimpleASTInterpretator.Core.Visitor;

public sealed class NodeVisitor : INodeVisitor
{
    private IEvaluator _evaluator;

    public NodeVisitor(Dictionary<string, long> values, IEvaluator evaluator)
    {
        Values = values;
        _evaluator = evaluator;
    }

    public Dictionary<string, long> Values { get; set; }

    public long Visit(BinaryOperationExpression node)
    {
        long left = node.Left.Accept(this);
        long right = node.Right.Accept(this);

        return _evaluator.Evaluate(left, right, node.Operation);
    }

    public long Visit(ConstantExpression node)
    {
        return node.Value;
    }

    public long Visit(IdentifierExpression node)
    {
        if (Values.TryGetValue(node.Identifier, out long value) is false)
        {
            throw new ArgumentException($"Unknown identifier {node.Identifier}");
        }

        return value;
    }

    public void Visit(AssignmentStatement node)
    {
        Values[node.Identifier] = node.Expression.Accept(this);
    }

    public void Visit(SequenceStatement node)
    {
        node.Left.Accept(this);
        node.Right.Accept(this);
    }

    public void Visit(WriteStatement node)
    {
        long result = node.Expression.Accept(this);
        Console.WriteLine(result);
    }

    public void Visit(ReadStatement node)
    {
        long value = long.Parse(Console.ReadLine()!);   
        Values[node.Identifier] = value;
    }
}