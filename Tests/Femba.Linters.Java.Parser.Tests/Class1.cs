using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class Class1
{
	[Fact]
	public void CustomTest1()
	{
		var programmText = "void myFun1(string arg1) { string t = \"1\"; return 3.4;}";
		var lexer = new Lexer(programmText);
		var tokens = lexer.NextToEnd();
		var types = tokens.Select(e => e.Type);
		
		Assert.Equal(new List<TokenType>
		{
			TokenType.Type,
			TokenType.Name,
			TokenType.Symbol,
			TokenType.Type,
			TokenType.Name,
			TokenType.Symbol,
			TokenType.Symbol,
			TokenType.Type,
			TokenType.Symbol,
			TokenType.Literal,
			TokenType.Symbol,
			TokenType.Keyword,
			TokenType.Literal,
			TokenType.Symbol,
			TokenType.Symbol
		}, types);
	}
}