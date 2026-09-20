using System;
using System.Collections.Generic;
using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Ast.Statements;
using SimpleASTInterpreter.Core.Evaluators;

namespace SimpleASTInterpreter.Core.Visitor;

public sealed class NodeVisitor : INodeVisitor
{
    private readonly IEvaluator _evaluator;

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

    public void Visit(IfStatement node)
    {
        if (node.Condition.Accept(this) != 0)
        {
            node.Then.Accept(this);
        }
        else
        {
            node.Else.Accept(this);
        }
    }

    public void Visit(SkipStatement node)
    {
    }

    public void Visit(WhileStatement node)
    {
        while (node.Condition.Accept(this) != 0)
        {
            node.Body.Accept(this);
        }
    }

    public void Visit(DoWhileStatement node)
    {
        do {
            node.Body.Accept(this);
        } while (node.Condition.Accept(this) != 0);
    }
}