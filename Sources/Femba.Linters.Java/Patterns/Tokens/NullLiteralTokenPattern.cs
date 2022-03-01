using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns.Tokens;

public sealed class NullLiteralTokenPattern : TokenRegexPattern
{
	public NullLiteralTokenPattern() : base(TokenType.Literal, new Regex("^(n|nu|nul|null)$")) { }
}