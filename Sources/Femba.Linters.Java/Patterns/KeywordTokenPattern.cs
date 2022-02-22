using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class KeywordPattern : TokenRegexPattern
{
	public KeywordPattern()
		: base(TokenType.Keyword,
			new Regex(@"^(new .|if\s+\(|else|else .|else\s+\(|case .+:|default|default.+:|switch\s+\(|break|return|return .)$")) { }

	protected override string PartLexeme(string partition)
	{
		if (partition.StartsWith("new")) return "new";
		if (partition.StartsWith("if")) return "if";
		if (partition.StartsWith("else")) return "else";
		if (partition.StartsWith("return")) return "return";
		if (partition.StartsWith("switch")) return "switch";
		if (partition.StartsWith("break")) return "break";
		if (partition.StartsWith("case")) return "case";
		return partition;
	}
}