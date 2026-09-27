using System;
using System.Collections.Generic;
using System.Text.Json;
using SimpleASTInterpreter.Instructions.Instructions;

namespace SimpleASTInterpreter.Instructions.Parsing;

public sealed class InstructionParser
{
    public static IReadOnlyList<IInstruction> Parse(string json)
    {
        using JsonDocument document = JsonDocument.Parse(json);

        List<IInstruction> instructions = new();

        foreach (JsonElement element in document.RootElement.EnumerateArray())
        {
            instructions.Add(ParseInstruction(element));
        }

        return instructions;
    }

    private static IInstruction ParseInstruction(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.String)
        {
            string token = element.GetString()!;

            if (token == "READ")
            {
                return new ReadInstruction();
            }

            if (token == "WRITE")
            {
                return new WriteInstruction();
            }

            throw new ArgumentException($"Unknown instruction: {token}");
        }

        JsonElement.ObjectEnumerator enumerator = element.EnumerateObject();
        JsonProperty property = enumerator.Current;
        string name = property.Name;

        if (name == "CONST")
        {
            return new ConstInstruction(property.Value.GetInt64());
        }

        if (name == "LD")
        {
            return new LdInstruction(property.Value.GetString()!);
        }

        if (name == "ST")
        {
            return new StInstruction(property.Value.GetString()!);
        }

        if (name == "BINOP")
        {
            return new BinopInstruction(property.Value.GetString()!);
        }

        if (name == "LABEL")
        {
            return new LabelInstruction(property.Value.GetString()!);
        }

        if (name == "JMP")
        {
            return new JmpInstruction(property.Value.GetString()!);
        }

        if (name == "JZ")
        {
            return new JzInstruction(property.Value.GetString()!);
        }

        if (name == "JNZ")
        {
            return new JnzInstruction(property.Value.GetString()!);
        }

        throw new ArgumentException($"Unknown instruction: {name}");
    }
}
