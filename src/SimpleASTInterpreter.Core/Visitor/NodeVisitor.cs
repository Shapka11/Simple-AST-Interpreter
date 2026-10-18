using System;
using System.Collections.Generic;
using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Ast.Statements;
using SimpleASTInterpreter.Core.Evaluators;
using SimpleASTInterpreter.Core.Values;

namespace SimpleASTInterpreter.Core.Visitor;

public sealed class NodeVisitor : INodeVisitor<RuntimeValue>
{
    private readonly IEvaluator _evaluator;

    public NodeVisitor(Dictionary<string, long> values, IEvaluator evaluator)
        : this(ConvertValues(values), evaluator)
    {
    }

    public NodeVisitor(Dictionary<string, RuntimeValue> values, IEvaluator evaluator)
    {
        Values = values ?? throw new ArgumentNullException(nameof(values));
        _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
    }

    public Dictionary<string, RuntimeValue> Values { get; }

    public RuntimeValue Visit(BinaryOperationExpression node)
    {
        RuntimeValue left = node.Left.Accept(this);
        RuntimeValue right = node.Right.Accept(this);

        if (left is NumberValue leftNumber && right is NumberValue rightNumber)
        {
            return new NumberValue(_evaluator.Evaluate(leftNumber.Value, rightNumber.Value, node.Operation));
        }

        if (node.Operation is "==" or "!=")
        {
            bool equal = left is StringValue leftText && right is StringValue rightText
                ? leftText.Value == rightText.Value
                : ReferenceEquals(left, right);
            return new NumberValue((node.Operation == "==" ? equal : !equal) ? 1 : 0);
        }

        if (node.Operation == "+" && left is StringValue first && right is StringValue second)
        {
            return new StringValue(first.Value + second.Value);
        }

        throw new InvalidOperationException(
            $"Operation '{node.Operation}' is not supported for {left.GetType().Name} and {right.GetType().Name}.");
    }

    public RuntimeValue Visit(ConstantExpression node) => new NumberValue(node.Value);

    public RuntimeValue Visit(StringExpression node) => new StringValue(node.Value);

    public RuntimeValue Visit(IdentifierExpression node)
    {
        return Values.TryGetValue(node.Identifier, out RuntimeValue? value) is false
            ? throw new ArgumentException($"Unknown identifier {node.Identifier}")
            : value;
    }

    public RuntimeValue Visit(ArrayExpression node)
    {
        var elements = new RuntimeValue[node.Elements.Count];
        for (var i = 0; i < elements.Length; i++)
        {
            elements[i] = node.Elements[i].Accept(this);
        }

        return new ArrayValue(elements);
    }

    public RuntimeValue Visit(LengthExpression node) => new NumberValue(node.Expression.Accept(this).Length);

    public RuntimeValue Visit(IndexExpression node)
    {
        var collection = node.Expression.Accept(this);
        var index = node.Index.Accept(this).AsNumber();
        return collection.GetElement(index);
    }

    public void Visit(AssignmentStatement node) => Values[node.Identifier] = node.Expression.Accept(this);

    public void Visit(ElementAssignmentStatement node)
    {
        var collection = node.Destination.Expression.Accept(this);
        var index = node.Destination.Index.Accept(this).AsNumber();
        var value = node.Expression.Accept(this);
        collection.SetElement(index, value);
    }

    public void Visit(SequenceStatement node)
    {
        node.Left.Accept(this);
        node.Right.Accept(this);
    }

    public void Visit(WriteStatement node) => Console.WriteLine(node.Expression.Accept(this));

    public void Visit(ReadStatement node) => Values[node.Identifier] = new NumberValue(long.Parse(Console.ReadLine()
        ?? throw new InvalidOperationException("Unexpected end of input.")));

    public void Visit(IfStatement node)
    {
        if (node.Condition.Accept(this).AsNumber() != 0)
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
        while (node.Condition.Accept(this).AsNumber() != 0)
        {
            node.Body.Accept(this);
        }
    }

    public void Visit(DoWhileStatement node)
    {
        do
        {
            node.Body.Accept(this);
        } while (node.Condition.Accept(this).AsNumber() != 0);
    }

    private static Dictionary<string, RuntimeValue> ConvertValues(Dictionary<string, long> values)
    {
        Dictionary<string, RuntimeValue> result = new();
        foreach (var entry in values)
        {
            result.Add(entry.Key, new NumberValue(entry.Value));
        }

        return result;
    }
}