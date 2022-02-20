using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Common;

public class RegexLexemePattern : ILexemePattern
{
	private readonly TokenType _type;

	protected Regex Regex { get; }

	public RegexLexemePattern(TokenType type, Regex regex)
	{
		_type = type;
		Regex = regex;
	}

	public bool IsMatch(string matcher) => Regex.IsMatch(matcher);

	public virtual string MatchLexeme(string matcher) => matcher;

	public IToken Match(string matcher) => new Token(_type, MatchLexeme(matcher), 1, 0);

	public IToken Match(string matcher, int line, int position) => new Token(_type, MatchLexeme(matcher), line, position);
}