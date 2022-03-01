using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns.Tokens;

public sealed class CharLiteralTokenPattern : TokenPattern
{
	public CharLiteralTokenPattern() : base(TokenType.Literal) { }

	public override bool IsPart(string matcher)
	{
		if (matcher.StartsWith('\'') && matcher.EndsWith('\'')) return true;
		if (matcher.StartsWith('\'') && matcher.Count(e => e == '\'') == 1) return true;
		return false;
	}
}