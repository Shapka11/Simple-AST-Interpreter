using System;
using System.Collections.Generic;

namespace SimpleASTInterpreter.Core.Values;

public sealed class SexpValue : RuntimeValue
{
    private readonly ArrayValue _arguments;

    public SexpValue(string name, IEnumerable<RuntimeValue> arguments)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _arguments = new ArrayValue(arguments);
    }

    public string Name { get; }

    public override int Length => _arguments.Length;

    public override RuntimeValue GetElement(long index) => _arguments.GetElement(index);

    public override void SetElement(long index, RuntimeValue value) => _arguments.SetElement(index, value);

    public override string ToString()
    {
        if (Name == "nil" && Length == 0)
        {
            return "{}";
        }

        if (Name == "cons" && Length == 2)
        {
            List<string> elements = new();
            RuntimeValue tail = this;
            while (tail is SexpValue { Name: "cons", Length: 2 } cell)
            {
                elements.Add(cell.GetElement(0).ToString()!);
                tail = cell.GetElement(1);
            }

            if (tail is not SexpValue { Name: "nil", Length: 0 })
            {
                throw new InvalidOperationException("A list must end with nil.");
            }

            return "{" + string.Join(", ", elements) + "}";
        }

        if (Length == 0)
        {
            return Name;
        }

        var arguments = new string[Length];
        for (var i = 0; i < arguments.Length; i++)
        {
            arguments[i] = GetElement(i).ToString()!;
        }

        return Name + " (" + string.Join(", ", arguments) + ")";
    }
}
