using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns.Tokens;

public sealed class NamePattern : TokenRegexPattern
{
	public NamePattern()
		: base(TokenType.Name, new Regex(@"^[\w]+$")) { }
}