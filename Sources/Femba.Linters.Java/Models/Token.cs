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

	private bool Equals(Token other) => Type == other.Type && Lexeme == other.Lexeme;

	public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Token other && Equals(other);

	public override int GetHashCode() => HashCode.Combine((int) Type, Lexeme);
}