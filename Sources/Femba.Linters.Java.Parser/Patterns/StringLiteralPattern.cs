using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class StringLiteralPattern : Pattern
{
	public StringLiteralPattern() : base(TokenType.Literal) { }

	public override bool IsMatch(string matcher)
	{
		if (matcher.StartsWith('"') && !matcher.EndsWith("\\\"") && matcher.EndsWith('"')) return true;
		if (matcher.StartsWith('"') && matcher.Count(e => e == '"') == 1) return true;
		return false;
	}

	public override string MatchLexeme(string matcher) => matcher;
}