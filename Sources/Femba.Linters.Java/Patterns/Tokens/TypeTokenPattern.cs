using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns.Tokens;

public sealed class TypePattern : TokenRegexPattern
{
	public TypePattern()
		: base(TokenType.Type, new Regex(@"^([\w]+)((\s+|\s+[\w])|)$")) { }
	
	protected override string PartLexeme(string partition)
	{
		return Regex.Match(partition.Trim()).Groups[1].Value;
	}
}