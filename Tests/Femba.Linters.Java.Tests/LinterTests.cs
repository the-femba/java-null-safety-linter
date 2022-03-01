using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;
using Femba.Linters.Java.Parser.Patterns.Tokens;
using Femba.Linters.Java.Parser.Utils;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class LinterTests
{
	private readonly List<ITokenPattern> _patterns = new List<ITokenPattern>
	{
		new NumberLiteralPattern(),
		new StringLiteralTokenPattern(),
		new CharLiteralTokenPattern(),
		new KeywordPattern(),
		new NamePattern(),
		new SymbolPattern(),
		new TypePattern()
	};
	
	delegate void TestDelegate (ref int x);
	
	[Fact]
	public void TestClearFunctionLex()
	{
		var text = "void myFun1(string arg1) { }";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("void", TokenType.Type),
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("string", TokenType.Type),
			("arg1", TokenType.Name),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.BodyOpenBasket, TokenType.Symbol),
			(TokensNames.BodyCloseBasket, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithNumberLiteralLex()
	{
		var text = "int test = 2;";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("2", TokenType.Literal),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithStringLiteralLex()
	{
		var text = "int test = \"text\";";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("\"text\"", TokenType.Literal),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithCharLiteralLex()
	{
		var text = "int test = 't';";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("'t'", TokenType.Literal),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithOtherVariableLex()
	{
		var text = "int test1 = testOld;";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test1", TokenType.Name),
			("=", TokenType.Symbol),
			("testOld", TokenType.Name),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestVariableWithFunctionLex()
	{
		var text = "int test = myFunc2();";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("int", TokenType.Type),
			("test", TokenType.Name),
			("=", TokenType.Symbol),
			("myFunc2", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeLex()
	{
		var text = "myFun1();";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithVariableArgumentLex()
	{
		var text = "myFun1(test);";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("test", TokenType.Name),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithLiteralNumberArgumentLex()
	{
		var text = "myFun1(3);";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("3", TokenType.Literal),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithManyArgumentsLex()
	{
		var text = "myFun1(3, 't', myVar2);";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("3", TokenType.Literal),
			(TokensNames.Comma, TokenType.Symbol),
			("'t'", TokenType.Literal),
			(TokensNames.Comma, TokenType.Symbol),
			("myVar2", TokenType.Name),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestMethodInvokeWithLiteralCharArgumentLex()
	{
		var text = "myFun1('t');";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("'t'", TokenType.Literal),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestInvokeMethodFromVariableLex()
	{
		var text = "myVar1.myFun1();";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myVar1", TokenType.Name),
			(".", TokenType.Symbol),
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestCreateNewObjectAndWriteToVarLex()
	{
		var text = "Point point1 = new Point();";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("Point", TokenType.Type),
			("point1", TokenType.Name),
			("=", TokenType.Symbol),
			("new", TokenType.Keyword),
			("Point", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestIfElseConstructionLex()
	{
		var text = "if (a == 1) { } else if (a == b) { } else { }";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();

		var expected = new List<(string, TokenType)>
		{
			(TokensNames.If, TokenType.Keyword),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("a", TokenType.Name),
			("==", TokenType.Symbol),
			("1", TokenType.Literal),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.BodyOpenBasket, TokenType.Symbol),
			(TokensNames.BodyCloseBasket, TokenType.Symbol),

			(TokensNames.Else, TokenType.Keyword),
			(TokensNames.If, TokenType.Keyword),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("a", TokenType.Name),
			("==", TokenType.Symbol),
			("b", TokenType.Name),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.BodyOpenBasket, TokenType.Symbol),
			(TokensNames.BodyCloseBasket, TokenType.Symbol),

			(TokensNames.Else, TokenType.Keyword),
			(TokensNames.BodyOpenBasket, TokenType.Symbol),
			(TokensNames.BodyCloseBasket, TokenType.Symbol)
		};

		var actual = tokens.Select(e => (e.Lexeme, e.Type));
		
		Assert.Equal(expected, actual);
	}
	
	[Fact]
	public void TestCodeInMethodWithReturnWithVariableReturnLex()
	{
		var text = "{ int a = 4.13; return a; }";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();

		var expected = new List<(string, TokenType)>
		{
			(TokensNames.BodyOpenBasket, TokenType.Symbol),
			
			("int", TokenType.Type),
			("a", TokenType.Name),
			("=", TokenType.Symbol),
			("4.13", TokenType.Literal),
			(TokensNames.Semicolon, TokenType.Symbol),
			
			("return", TokenType.Keyword),
			("a", TokenType.Name),
			(TokensNames.Semicolon, TokenType.Symbol),
			
			(TokensNames.BodyCloseBasket, TokenType.Symbol),
		};

		var actual = tokens.Select(e => (e.Lexeme, e.Type));
		
		Assert.Equal(expected, actual);
	}
	
	[Fact]
	public void TestCodeInMethodWithReturnWithLiteralReturnLex()
	{
		var text = "{ return 4.13; }";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();

		var expected = new List<(string, TokenType)>
		{
			(TokensNames.BodyOpenBasket, TokenType.Symbol),
			
			("return", TokenType.Keyword),
			("4.13", TokenType.Literal),
			(TokensNames.Semicolon, TokenType.Symbol),
			
			(TokensNames.BodyCloseBasket, TokenType.Symbol),
		};

		var actual = tokens.Select(e => (e.Lexeme, e.Type));
		
		Assert.Equal(expected, actual);
	}
	
	[Fact]
	public void TestMethodInvokeWithLiteralStringArgumentLex()
	{
		var text = "myFun1(\"text\");";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("\"text\"", TokenType.Literal),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestStringLiteralLex()
	{
		var text = "\"text\"";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("\"text\"", TokenType.Literal)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestStringLiteralBackEndLex()
	{
		var text = "\"tex\\\"t\"";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{ 
			("\"tex\\\"t\"", TokenType.Literal)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestThisKeywordAsToVarLex()
	{
		var text = "MyClass myVar = this;";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("MyClass", TokenType.Type),
			("myVar", TokenType.Name),
			("=", TokenType.Symbol),
			("this", TokenType.Name),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestThisKeywordInvokeFieldLex()
	{
		var text = "this.myField;";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("this", TokenType.Name),
			(".", TokenType.Symbol),
			("myField", TokenType.Name),
			(TokensNames.Semicolon, TokenType.Symbol)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestAttributeLex()
	{
		var text = "@NotNull Point nonNullP";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			(TokensNames.Dog, TokenType.Symbol),
			("NotNull", TokenType.Type),
			("Point", TokenType.Type),
			("nonNullP", TokenType.Name)
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
	
	[Fact]
	public void TestSwitchLex()
	{
		var text = "switch (myVar1) { case 1: myFun1(); break; default: myFun2(); }";
		
		var tokens = new Lexer(text){Patterns = _patterns}.LexToEnd();
		
		Assert.Equal(new List<(string, TokenType)>
		{
			("switch", TokenType.Keyword),
			
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			("myVar1", TokenType.Name),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			
			(TokensNames.BodyOpenBasket, TokenType.Symbol),
			
			("case", TokenType.Keyword),
			("1", TokenType.Literal),
			(":", TokenType.Symbol),
			
			("myFun1", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol),
			
			("break", TokenType.Keyword),
			(TokensNames.Semicolon, TokenType.Symbol),
			
			("default", TokenType.Keyword),
			(":", TokenType.Symbol),
			
			("myFun2", TokenType.Name),
			(TokensNames.ArgumentOpenBasket, TokenType.Symbol),
			(TokensNames.ArgumentCloseBasket, TokenType.Symbol),
			(TokensNames.Semicolon, TokenType.Symbol),
			
			(TokensNames.BodyCloseBasket, TokenType.Symbol),
		}, tokens.Select(e => (e.Lexeme, e.Type)));
	}
}