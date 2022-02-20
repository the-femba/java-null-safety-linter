using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class Class1
{
	[Fact]
	public void CustomTest1()
	{
		var text = "void myFun1(string arg1) { }";
		
		var tokens = new Lexer(text).NextToEnd();
		
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
}