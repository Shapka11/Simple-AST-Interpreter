using System;

namespace SimpleASTInterpreter.Core.Values;

public abstract class RuntimeValue
{
    public virtual long AsNumber() => throw new InvalidOperationException("Expected a number.");

    public virtual int Length => throw new InvalidOperationException("Expected a string, an array or an S-expression.");

    public virtual RuntimeValue GetElement(long index) =>
        throw new InvalidOperationException("Expected a string, an array or an S-expression.");

    public virtual void SetElement(long index, RuntimeValue value) =>
        throw new InvalidOperationException("Expected a string, an array or an S-expression.");

    protected static int CheckIndex(long index, int length)
    {
        if (index < 0 || index >= length)
        {
            throw new IndexOutOfRangeException($"Index {index} is outside a collection of length {length}.");
        }

        return (int)index;
    }
}
