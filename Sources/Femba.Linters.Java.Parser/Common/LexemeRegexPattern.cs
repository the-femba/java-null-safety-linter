using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Common;

public class LexemeRegexPattern : LexemePattern
{
	protected Regex Regex { get; }

	public LexemeRegexPattern(TokenType type, Regex regex) : base(type)
	{
		Regex = regex;
	}

	public override bool IsMatch(string matcher) => Regex.IsMatch(matcher);

	public override string MatchLexeme(string matcher) => matcher;
}