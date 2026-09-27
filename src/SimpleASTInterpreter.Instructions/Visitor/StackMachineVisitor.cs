using System;
using System.Collections.Generic;
using SimpleASTInterpreter.Core.Evaluators;
using SimpleASTInterpreter.Instructions.Instructions;

namespace SimpleASTInterpreter.Instructions.Visitor;

public sealed class StackMachineVisitor : IInstructionVisitor
{
    private readonly IReadOnlyList<IInstruction> _instructions;
    private readonly Dictionary<string, int> _labels;
    private readonly Stack<long> _stack = new();
    private readonly Dictionary<string, long> _variables = new();
    private readonly BinaryOperationEvaluator _evaluator = new();

    private int _ip;
    private int _nextIp;

    public StackMachineVisitor(IReadOnlyList<IInstruction> instructions)
    {
        _instructions = instructions;
        _labels = BuildLabelTable(instructions);
    }

    public void Run()
    {
        _ip = 0;

        while (_ip < _instructions.Count)
        {
            _nextIp = _ip + 1;

            _instructions[_ip].Accept(this);

            _ip = _nextIp;
        }
    }

    public void Visit(ReadInstruction node)
    {
        _stack.Push(long.Parse(Console.ReadLine()!));
    }

    public void Visit(WriteInstruction node)
    {
        Console.WriteLine(_stack.Pop());
    }

    public void Visit(LdInstruction node)
    {
        if (_variables.TryGetValue(node.Identifier, out long value) is false)
        {
            throw new ArgumentException($"Unknown identifier {node.Identifier}");
        }

        _stack.Push(value);
    }

    public void Visit(StInstruction node)
    {
        _variables[node.Identifier] = _stack.Pop();
    }

    public void Visit(ConstInstruction node)
    {
        _stack.Push(node.Value);
    }

    public void Visit(BinopInstruction node)
    {
        long right = _stack.Pop();
        long left = _stack.Pop();

        _stack.Push(_evaluator.Evaluate(left, right, node.Operation));
    }

    public void Visit(LabelInstruction node)
    {
    }

    public void Visit(JmpInstruction node)
    {
        JumpTo(node.Label);
    }

    public void Visit(JzInstruction node)
    {
        if (_stack.Pop() == 0)
        {
            JumpTo(node.Label);
        }
    }

    public void Visit(JnzInstruction node)
    {
        if (_stack.Pop() != 0)
        {
            JumpTo(node.Label);
        }
    }

    private void JumpTo(string label)
    {
        if (_labels.TryGetValue(label, out int target) is false)
        {
            throw new ArgumentException($"Unknown label {label}");
        }

        _nextIp = target;
    }

    private static Dictionary<string, int> BuildLabelTable(IReadOnlyList<IInstruction> instructions)
    {
        Dictionary<string, int> labels = new();

        for (int i = 0; i < instructions.Count; i++)
        {
            if (instructions[i] is LabelInstruction { Label: { } label })
            {
                labels[label] = i;
            }
        }

        return labels;
    }
}
