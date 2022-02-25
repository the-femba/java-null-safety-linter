using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;
using Femba.Linters.Java.Parser.Patterns;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class ParserTests
{
	[Fact]
	public void Test1()
	{
		var text = "int myFunc1(@NotNull int value) { } ";
		
		var parser = new Common.Parser(text, new List<INodePattern>
		{
			new FunctionNodePattern()
		});

		var node = parser.ParseToEnd();}
	
	[Fact]
	public void Test2()
	{
		var tokens = new Lexer("@NotNull int value", patterns: new HashSet<ITokenPattern>
		{
			new NumberLiteralPattern(),
			new StringLiteralTokenPattern(),
			new CharLiteralTokenPattern(),
			new KeywordPattern(),
			new NamePattern(),
			new SymbolPattern(),
			new TypePattern()
		}).LexToEnd();

		var node = new VariableNodePattern().Part(tokens.ToList());
	}
	
	[Fact]
	public void Test3()
	{
		var node = new Common.Parser("Function fun = int myFun1() { };", new List<INodePattern>
		{
			new FunctionNodePattern(),
			new VariableAssignmentNodePattern()
		}).ParseToEnd();
	}
	
	[Fact]
	public void Test4()
	{
		var node = new Common.Parser("myFun1(name1, name2).value.toString();", new List<INodePattern>
		{
			new FunctionNodePattern(),
			new VariableInvokeNodePattern(),
			new FunctionInvokeNodePattern(),
			new VariableAssignmentNodePattern()
		}).ParseToEnd();
	}
}