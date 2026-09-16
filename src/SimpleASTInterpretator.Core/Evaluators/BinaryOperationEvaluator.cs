using System;

namespace SimpleASTInterpretator.Core.Evaluators;

public sealed class BinaryOperationEvaluator : IEvaluator
{
    public long Evaluate(long left, long right, string operation)
    {
        if (operation == "+")
        {
            return left + right;
        }

        if (operation == "-")
        {
            return left - right;
        }

        return operation switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" => left / right,
            "%" => left % right,
            ">" => left > right ? 1L : 0L,
            "<" => left < right ? 1L : 0L,
            ">=" => left >= right ? 1L : 0L,
            "<=" => left <= right ? 1L : 0L,
            "==" => left == right ? 1L : 0L,
            "!=" => left != right ? 1L : 0L,
            "&&" => (left != 0 && right != 0) ? 1L : 0L,
            "!!" => (left != 0 || right != 0) ? 1L : 0L,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };
    }
}