using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using SimpleASTInterpreter.Instructions.Instructions;
using SimpleASTInterpreter.Instructions.Visitor;

namespace SimpleAstProgramToInstructions;

public sealed class InstructionJsonWriter : IInstructionVisitor
{
    private readonly Utf8JsonWriter _writer;

    private InstructionJsonWriter(Utf8JsonWriter writer)
    {
        _writer = writer;
    }

    public static string Write(IReadOnlyList<IInstruction> instructions)
    {
        using MemoryStream stream = new();

        using (Utf8JsonWriter writer = new(stream, new JsonWriterOptions { Indented = true }))
        {
            InstructionJsonWriter self = new(writer);

            writer.WriteStartArray();

            foreach (IInstruction instruction in instructions)
            {
                instruction.Accept(self);
            }

            writer.WriteEndArray();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    public void Visit(ReadInstruction node) => _writer.WriteStringValue("READ");

    public void Visit(WriteInstruction node) => _writer.WriteStringValue("WRITE");

    public void Visit(LdInstruction node) => WriteObject("LD", node.Identifier);

    public void Visit(StInstruction node) => WriteObject("ST", node.Identifier);

    public void Visit(ConstInstruction node) => WriteObject("CONST", node.Value);

    public void Visit(BinopInstruction node) => WriteObject("BINOP", node.Operation);

    public void Visit(LabelInstruction node) => WriteObject("LABEL", node.Label);

    public void Visit(JmpInstruction node) => WriteObject("JMP", node.Label);

    public void Visit(JzInstruction node) => WriteObject("JZ", node.Label);

    public void Visit(JnzInstruction node) => WriteObject("JNZ", node.Label);

    private void WriteObject(string name, string value)
    {
        _writer.WriteStartObject();
        _writer.WriteString(name, value);
        _writer.WriteEndObject();
    }

    private void WriteObject(string name, long value)
    {
        _writer.WriteStartObject();
        _writer.WriteNumber(name, value);
        _writer.WriteEndObject();
    }
}
