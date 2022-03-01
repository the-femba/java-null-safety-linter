using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Common;

public class TokenRegexPattern : TokenPattern
{
	protected Regex Regex { get; }

	protected TokenRegexPattern(TokenType type, Regex regex) : base(type)
	{
		Regex = regex;
	}

	public override bool IsPart(string partition) => Regex.IsMatch(partition);

	protected override string PartLexeme(string partition) => partition;
}