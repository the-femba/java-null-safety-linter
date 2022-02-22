using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Models;

public sealed class Token : IToken
{
	public Token(TokenType type, string lexeme)
	{
		Type = type;
		Lexeme = lexeme;
	}

	public int Position { get; init;  }

	public TokenType Type { get; }

	public string Lexeme { get; }
}