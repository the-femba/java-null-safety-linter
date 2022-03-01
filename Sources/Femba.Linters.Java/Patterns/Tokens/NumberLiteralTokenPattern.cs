using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns.Tokens;

public sealed class NumberLiteralPattern : TokenRegexPattern
{
	public NumberLiteralPattern()
		: base(TokenType.Literal, new Regex("^([0-9]+||[0-9]+\\.|[0-9]+\\.[0-9]+)$")) { }
}