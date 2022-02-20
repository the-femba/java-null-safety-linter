using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class LinterTests
{
	delegate void TestDelegate (ref int x);
	
	[Fact]
	public void TestClearFunctionLex()
	{
		var text = "void myFun1(string arg1) { }";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("void", TokenType.Type),
			("myFun1", TokenType.Name),
			("(", TokenType.Symbol),
			("string", TokenType.Type),
			("arg1", TokenType.Name),
			(")", TokenType.Symbol),
			("{", TokenType.Symbol),
			("}", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithNumberLiteralLex()
	{
		var text = "int test = 2;";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("2", TokenType.Literal),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithStringLiteralLex()
	{
		var text = "int test = \"text\";";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("\"text\"", TokenType.Literal),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithCharLiteralLex()
	{
		var text = "int test = 't';";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("'t'", TokenType.Literal),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithOtherVariableLex()
	{
		var text = "int test1 = testOld;";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test1", TokenType.Name),
			("=", TokenType.Symbol),
			("testOld", TokenType.Name),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithFunctionLex()
	{
		var text = "int test = myFunc2();";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("myFunc2", TokenType.Name),
			("(", TokenType.Symbol),
			(")", TokenType.Symbol),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeLex()
	{
		var text = "myFun1();";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			("(", TokenType.Symbol),
			(")", TokenType.Symbol),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithVariableArgumentLex()
	{
		var text = "myFun1(test);";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			("(", TokenType.Symbol),
			("test", TokenType.Name),
			(")", TokenType.Symbol),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithLiteralNumberArgumentLex()
	{
		var text = "myFun1(3);";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			("(", TokenType.Symbol),
			("3", TokenType.Literal),
			(")", TokenType.Symbol),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithManyArgumentsLex()
	{
		var text = "myFun1(3, 't', myVar2);";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			("(", TokenType.Symbol),
			("3", TokenType.Literal),
			(",", TokenType.Symbol),
			("'t'", TokenType.Literal),
			(",", TokenType.Symbol),
			("myVar2", TokenType.Name),
			(")", TokenType.Symbol),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithLiteralCharArgumentLex()
	{
		var text = "myFun1('t');";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			("(", TokenType.Symbol),
			("'t'", TokenType.Literal),
			(")", TokenType.Symbol),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithLiteralStringArgumentLex()
	{
		var text = "myFun1(\"text\");";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			("(", TokenType.Symbol),
			("\"text\"", TokenType.Literal),
			(")", TokenType.Symbol),
			(";", TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestStringLiteralLex()
	{
		var text = "\"text\"";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("\"text\"", TokenType.Literal)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestStringLiteralBackEndLex()
	{
		var text = "\"tex\\\"t\"";
		
		var tokens = new Lexer(text).LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("\"tex\\\"t\"", TokenType.Literal)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
}