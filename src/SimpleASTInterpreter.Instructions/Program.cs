using System.Collections.Generic;
using System.IO;
using SimpleASTInterpreter.Instructions.Instructions;
using SimpleASTInterpreter.Instructions.Parsing;
using SimpleASTInterpreter.Instructions.Visitor;

string json = File.ReadAllText("instructions.json");

IReadOnlyList<IInstruction> program = InstructionParser.Parse(json);

var visitor = new StackMachineVisitor(program);

visitor.Run();
