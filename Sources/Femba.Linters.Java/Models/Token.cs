using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Models;

public sealed record Token : IToken
{
	public Token(TokenType type, string lexeme, int position)
	{
		Type = type;
		Lexeme = lexeme;
		Position = position;
	}

	public int Position { get; }

	public TokenType Type { get; }

	public string Lexeme { get; }
}