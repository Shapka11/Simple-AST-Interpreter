using System.Collections.Generic;
using SimpleASTInterpreter.Core.Ast.Expressions;
using SimpleASTInterpreter.Core.Ast.Statements;
using SimpleASTInterpreter.Core.Visitor;
using SimpleASTInterpreter.Instructions.Instructions;

namespace SimpleAstProgramToInstructions;

public sealed class CodeGenerator : INodeVisitor
{
    private readonly List<IInstruction> _instructions = new();
    private int _labelCounter;

    public IReadOnlyList<IInstruction> Instructions => _instructions;

    public long Visit(BinaryOperationExpression node)
    {
        node.Left.Accept(this);
        node.Right.Accept(this);
        _instructions.Add(new BinopInstruction(node.Operation));

        return 0;
    }

    public long Visit(ConstantExpression node)
    {
        _instructions.Add(new ConstInstruction(node.Value));

        return 0;
    }

    public long Visit(IdentifierExpression node)
    {
        _instructions.Add(new LdInstruction(node.Identifier));

        return 0;
    }

    public void Visit(AssignmentStatement node)
    {
        node.Expression.Accept(this);
        _instructions.Add(new StInstruction(node.Identifier));
    }

    public void Visit(SequenceStatement node)
    {
        node.Left.Accept(this);
        node.Right.Accept(this);
    }

    public void Visit(WriteStatement node)
    {
        node.Expression.Accept(this);
        _instructions.Add(new WriteInstruction());
    }

    public void Visit(ReadStatement node)
    {
        _instructions.Add(new ReadInstruction());
        _instructions.Add(new StInstruction(node.Identifier));
    }

    public void Visit(IfStatement node)
    {
        int id = _labelCounter++;
        string elseLabel = $"L_else_{id}";
        string endLabel = $"L_endif_{id}";

        node.Condition.Accept(this);
        _instructions.Add(new JzInstruction(elseLabel));

        node.Then.Accept(this);
        _instructions.Add(new JmpInstruction(endLabel));

        _instructions.Add(new LabelInstruction(elseLabel));
        node.Else.Accept(this);

        _instructions.Add(new LabelInstruction(endLabel));
    }

    public void Visit(SkipStatement node)
    {
    }

    public void Visit(WhileStatement node)
    {
        int id = _labelCounter++;
        string condLabel = $"L_while_cond_{id}";
        string bodyLabel = $"L_while_body_{id}";

        _instructions.Add(new JmpInstruction(condLabel));

        _instructions.Add(new LabelInstruction(bodyLabel));
        node.Body.Accept(this);

        _instructions.Add(new LabelInstruction(condLabel));
        node.Condition.Accept(this);
        _instructions.Add(new JnzInstruction(bodyLabel));
    }

    public void Visit(DoWhileStatement node)
    {
        int id = _labelCounter++;
        string bodyLabel = $"L_do_body_{id}";

        _instructions.Add(new LabelInstruction(bodyLabel));
        node.Body.Accept(this);

        node.Condition.Accept(this);
        _instructions.Add(new JnzInstruction(bodyLabel));
    }
}
