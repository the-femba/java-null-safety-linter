using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class TypePattern : LexemeRegexPattern
{
	public TypePattern()
		: base(TokenType.Type, new Regex(@"^([\w]+)((\s+|\s+[\w])|)$")) { }
	
	public override string MatchLexeme(string matcher)
	{
		return Regex.Match(matcher.Trim()).Groups[1].Value;
	}
}