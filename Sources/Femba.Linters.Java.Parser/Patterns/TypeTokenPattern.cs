using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class TypeTokenPattern : RegexTokenPattern
{
	public TypeTokenPattern()
		: base(TokenType.Type, new Regex(@"^([\w]+)((\s+|\s+[\w])|)$")) { }
	
	protected override string PartLexeme(string partition)
	{
		return Regex.Match(partition.Trim()).Groups[1].Value;
	}
}