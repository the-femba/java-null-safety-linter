using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Models;

public sealed class Token : IToken
{
	public Token(TokenType type, string lexeme, int line, int position)
	{
		Type = type;
		Lexeme = lexeme;
		Line = line;
		Position = position;
	}

	public int Line { get; }

	public int Position { get; }

	public TokenType Type { get; }

	public string Lexeme { get; }
}