using System;
using SimpleASTInterpreter.Core.Values;

namespace SimpleASTInterpreter.Core.Evaluators;

public sealed class ValueOperationEvaluator
{
    private readonly IEvaluator _numberEvaluator;

    public ValueOperationEvaluator(IEvaluator numberEvaluator)
    {
        _numberEvaluator = numberEvaluator ?? throw new ArgumentNullException(nameof(numberEvaluator));
    }

    public RuntimeValue Evaluate(RuntimeValue left, RuntimeValue right, string operation)
    {
        if (left is NumberValue leftNumber && right is NumberValue rightNumber)
        {
            return new NumberValue(_numberEvaluator.Evaluate(leftNumber.Value, rightNumber.Value, operation));
        }

        if (operation is "==" or "!=" or "<" or "<=" or ">" or ">=")
        {
            int comparison = Compare(left, right);
            bool result = operation switch
            {
                "==" => comparison == 0,
                "!=" => comparison != 0,
                "<" => comparison < 0,
                "<=" => comparison <= 0,
                ">" => comparison > 0,
                _ => comparison >= 0
            };
            return new NumberValue(result ? 1 : 0);
        }

        throw new InvalidOperationException(
            $"Operation '{operation}' is not supported for {left.GetType().Name} and {right.GetType().Name}.");
    }

    private static int Compare(RuntimeValue left, RuntimeValue right)
    {
        if (left is NumberValue leftNumber && right is NumberValue rightNumber)
        {
            return leftNumber.Value.CompareTo(rightNumber.Value);
        }

        if (left is StringValue && right is StringValue)
        {
            int length = Math.Min(left.Length, right.Length);
            for (var i = 0; i < length; i++)
            {
                int comparison = left.GetElement(i).AsNumber().CompareTo(right.GetElement(i).AsNumber());
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return left.Length.CompareTo(right.Length);
        }

        if (left is ArrayValue && right is ArrayValue)
        {
            int comparison = left.Length.CompareTo(right.Length);
            if (comparison != 0)
            {
                return comparison;
            }

            for (var i = 0; i < left.Length; i++)
            {
                comparison = Compare(left.GetElement(i), right.GetElement(i));
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return 0;
        }

        throw new InvalidOperationException(
            $"Cannot compare {left.GetType().Name} and {right.GetType().Name}.");
    }
}
