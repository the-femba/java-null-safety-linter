using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class KeywordPattern : RegexPattern
{
	public KeywordPattern()
		: base(TokenType.Keyword,
			new Regex(@"^(new .|if\s+\(|else|else .|else\s+\(|case .+:|default|default.+:|switch\s+\(|break|return|return .)$")) { }

	public override string MatchLexeme(string matcher)
	{
		if (matcher.StartsWith("new")) return "new";
		if (matcher.StartsWith("if")) return "if";
		if (matcher.StartsWith("else")) return "else";
		if (matcher.StartsWith("return")) return "return";
		if (matcher.StartsWith("switch")) return "switch";
		if (matcher.StartsWith("break")) return "break";
		if (matcher.StartsWith("case")) return "case";
		return matcher;
	}
}