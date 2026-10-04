using System;
using System.Collections.Generic;
using System.IO;
using SimpleASTInterpreter.Core.Ast.Statements;
using SimpleASTInterpreter.Core.Parsing;
using SimpleASTInterpreter.Instructions.Instructions;
using SimpleASTInterpreter.Instructions.Visitor;
using SimpleAstProgramToInstructions;

string astJson = File.ReadAllText("program.json");

IStatement ast = new AstJsonParser().Parse(astJson);

var generator = new CodeGenerator();
ast.Accept(generator);

IReadOnlyList<IInstruction> instructions = generator.Instructions;

Console.WriteLine(InstructionJsonWriter.Write(instructions));

var visitor = new StackMachineVisitor(instructions);
visitor.Run();
