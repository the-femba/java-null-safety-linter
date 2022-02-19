using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Extensions;

public static class TokenExtensions
{
	public static bool IsSymbol(this IToken token)
		=> token.Type == TokenType.Symbol;
	
	public static bool IsLiteral(this IToken token)
		=> token.Type == TokenType.Literal;
	
	public static bool IsName(this IToken token)
		=> token.Type == TokenType.Name;
	
	public static bool IsType(this IToken token)
		=> token.Type == TokenType.Type;
	
	public static bool IsKeyword(this IToken token)
		=> token.Type == TokenType.Keyword;
}