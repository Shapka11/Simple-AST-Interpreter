using System.Collections.Generic;
using System.IO;
using SimpleASTInterpreter.Core.Ast.Statements;
using SimpleASTInterpreter.Core.Evaluators;
using SimpleASTInterpreter.Core.Parsing;
using SimpleASTInterpreter.Core.Visitor;

string json = File.ReadAllText("program.json");

var parser = new AstJsonParser();

IStatement program = parser.Parse(json);

var visitor = new NodeVisitor(
    new Dictionary<string, long>(),
    new BinaryOperationEvaluator()
);

program.Accept(visitor);