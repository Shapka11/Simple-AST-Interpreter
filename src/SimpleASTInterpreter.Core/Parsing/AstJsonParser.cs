using System;
using System.Text.Json;
using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Ast.Statements;

namespace SimpleASTInterpreter.Core.Parsing;

public sealed class AstJsonParser
{
    public IStatement Parse(string json)
    {
        using JsonDocument document = JsonDocument.Parse(json, new JsonDocumentOptions { MaxDepth = 512 });

        return ParseStatement(document.RootElement);
    }

    private IStatement ParseStatement(JsonElement node)
    {
        if (node.ValueKind == JsonValueKind.String)
        {
            if (node.GetString() == "skip")
            {
                return new SkipStatement();
            }

            throw new ArgumentException($"Unknown statement: {node.GetString()}");
        }

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

        if (node.TryGetProperty("read", out JsonElement read))
        {
            string identifier = read.GetString() ?? throw new ArgumentException("Read identifier cannot be null");

            return new ReadStatement(identifier);
        }

        if (node.TryGetProperty("assn", out JsonElement assignment))
        {
            string identifier = assignment
                .GetProperty("dst")
                .GetString()!;

            IExpression expression = ParseExpression(assignment.GetProperty("src"));

            return new AssignmentStatement(identifier, expression);
        }

        if (node.TryGetProperty("if", out JsonElement ifNode))
        {
            IExpression condition = ParseExpression(ifNode.GetProperty("cond"));

            IStatement thenStmt = ParseStatement(ifNode.GetProperty("then"));

            IStatement? elseStmt = null;
            if (ifNode.TryGetProperty("else", out JsonElement elseElement))
            {
                elseStmt = ParseStatement(elseElement);
            }

            return new IfStatement(condition, thenStmt, elseStmt);
        }
        
        if (node.TryGetProperty("while", out JsonElement whileNode))
        {
            IExpression condition = ParseExpression(whileNode.GetProperty("cond"));
            
            IStatement bodyStmt = ParseStatement(whileNode.GetProperty("body"));
            
            return new WhileStatement(condition, bodyStmt);
        }
        
        if (node.TryGetProperty("do", out JsonElement doWhileNode))
        {
            IStatement bodyStmt = ParseStatement(doWhileNode.GetProperty("body"));
            
            IExpression condition = ParseExpression(doWhileNode.GetProperty("cond"));
            
            return new DoWhileStatement(condition, bodyStmt);
        }

        throw new ArgumentException("Unknown statement");
    }

    private IExpression ParseExpression(JsonElement node)
    {
        if (node.TryGetProperty("const", out JsonElement constant))
        {
            return new ConstantExpression(constant.GetInt64());
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