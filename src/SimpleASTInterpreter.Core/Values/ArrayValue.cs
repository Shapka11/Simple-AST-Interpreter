using System;
using System.Collections.Generic;

namespace SimpleASTInterpreter.Core.Values;

public sealed class ArrayValue : RuntimeValue
{
    private readonly RuntimeValue[] _elements;

    public ArrayValue(IEnumerable<RuntimeValue> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);
        _elements = [.. elements];
    }

    public override int Length => _elements.Length;

    public override RuntimeValue GetElement(long index) => _elements[CheckIndex(index, Length)];

    public override void SetElement(long index, RuntimeValue value)
    {
        _elements[CheckIndex(index, Length)] = value;
    }

    public override string ToString()
    {
        var elements = new string[_elements.Length];
        for (var i = 0; i < _elements.Length; i++)
        {
            elements[i] = _elements[i].ToString()!;
        }

        return "[" + string.Join(", ", elements) + "]";
    }
}
