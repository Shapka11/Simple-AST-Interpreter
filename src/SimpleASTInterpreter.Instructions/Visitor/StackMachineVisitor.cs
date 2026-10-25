using System;
using System.Collections.Generic;
using SimpleASTInterpreter.Core.Evaluators;
using SimpleASTInterpreter.Core.Parsing;
using SimpleASTInterpreter.Core.Values;
using SimpleASTInterpreter.Instructions.Instructions;

namespace SimpleASTInterpreter.Instructions.Visitor;

public sealed class StackMachineVisitor : IInstructionVisitor
{
    private readonly IReadOnlyList<IInstruction> _instructions;
    private readonly Dictionary<string, int> _labels;
    private readonly Stack<RuntimeValue> _stack = new();
    private readonly Dictionary<string, RuntimeValue> _variables = new();
    private readonly ValueOperationEvaluator _evaluator = new(new BinaryOperationEvaluator());

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
        _stack.Push(RuntimeValueParser.Parse(Console.ReadLine()
            ?? throw new InvalidOperationException("Unexpected end of input.")));
    }

    public void Visit(WriteInstruction node)
    {
        Console.WriteLine(_stack.Pop());
    }

    public void Visit(LdInstruction node)
    {
        if (_variables.TryGetValue(node.Identifier, out RuntimeValue? value) is false)
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
        _stack.Push(new NumberValue(node.Value));
    }

    public void Visit(BinopInstruction node)
    {
        RuntimeValue right = _stack.Pop();
        RuntimeValue left = _stack.Pop();

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
        if (_stack.Pop().AsNumber() == 0)
        {
            JumpTo(node.Label);
        }
    }

    public void Visit(JnzInstruction node)
    {
        if (_stack.Pop().AsNumber() != 0)
        {
            JumpTo(node.Label);
        }
    }

    public void Visit(ArrayInstruction node) => _stack.Push(new ArrayValue(PopElements(node.Count)));

    public void Visit(StringInstruction node) => _stack.Push(new StringValue(node.Value));

    public void Visit(LengthInstruction node) => _stack.Push(new NumberValue(_stack.Pop().Length));

    public void Visit(ElemInstruction node)
    {
        long index = _stack.Pop().AsNumber();
        RuntimeValue collection = _stack.Pop();
        _stack.Push(collection.GetElement(index));
    }

    public void Visit(SetInstruction node)
    {
        RuntimeValue value = _stack.Pop();
        long index = _stack.Pop().AsNumber();
        RuntimeValue collection = _stack.Pop();
        collection.SetElement(index, value);
    }

    public void Visit(SexpInstruction node) => _stack.Push(new SexpValue(node.Name, PopElements(node.Count)));

    private RuntimeValue[] PopElements(int count)
    {
        if (_stack.Count < count)
        {
            throw new InvalidOperationException("Not enough values on the stack.");
        }

        var elements = new RuntimeValue[count];
        for (int i = count - 1; i >= 0; i--)
        {
            elements[i] = _stack.Pop();
        }

        return elements;
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
                if (!labels.TryAdd(label, i))
                {
                    throw new ArgumentException($"Duplicate label {label}");
                }
            }
        }

        return labels;
    }
}
