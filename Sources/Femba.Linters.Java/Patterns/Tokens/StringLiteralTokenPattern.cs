using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns.Tokens;

public sealed class StringLiteralTokenPattern : TokenPattern
{
	public StringLiteralTokenPattern() : base(TokenType.Literal) { }

	public override bool IsPart(string partition)
	{
		if (partition.StartsWith('"') && !partition.EndsWith("\\\"") && partition.EndsWith('"')) return true;
		if (partition.StartsWith('"') && partition.Count(e => e == '"') == 1) return true;
		return false;
	}
}