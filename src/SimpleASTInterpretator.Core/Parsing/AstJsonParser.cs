using System;
using System.Text.Json;
using SimpleASTInterpretator.Core.Ast.Expressions;
using SimpleASTInterpretator.Core.Ast.Statements;

namespace SimpleASTInterpretator.Core.Parsing;

public sealed class AstJsonParser
{
    public IStatement Parse(string json)
    {
        using JsonDocument document = JsonDocument.Parse(json);

        return ParseStatement(document.RootElement);
    }

    private IStatement ParseStatement(JsonElement node)
    {
        if (node.TryGetProperty("seq", out JsonElement sequence))
        {
            return new SequenceStatement(
                ParseStatement(sequence.GetProperty("left")),
                ParseStatement(sequence.GetProperty("right"))
            );
        }

        if (node.TryGetProperty("write", out JsonElement write))
        {
            return new WriteStatement(ParseExpression(write));
        }

        if (node.TryGetProperty("assn", out JsonElement assignment))
        {
            string identifier = assignment
                .GetProperty("dst")
                .GetString()!;

            IExpression expression = ParseExpression(
                assignment.GetProperty("src")
            );

            return new AssignmentStatement(
                identifier,
                expression
            );
        }

        throw new ArgumentException("Unknown statement");
    }

    private IExpression ParseExpression(JsonElement node)
    {
        if (node.TryGetProperty("const", out JsonElement constant))
        {
            return new ConstantExpression(
                constant.GetInt64()
            );
        }

        if (node.TryGetProperty("var", out JsonElement identifier))
        {
            return new IdentifierExpression(identifier.GetString()!);
        }

        if (node.TryGetProperty("binop", out JsonElement operation))
        {
            return new BinaryOperationExpression(
                operation.GetString()!,
                ParseExpression(node.GetProperty("left")),
                ParseExpression(node.GetProperty("right"))
            );
        }

        throw new ArgumentException("Unknown expression");
    }
}