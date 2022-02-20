using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class NameLexemePattern : ILexemePattern
{
	private readonly Regex _regex = new(@"^[\w]+$");
	
	public bool IsMatch(string matcher) => _regex.IsMatch(matcher);
	
	public IToken Match(string matcher) => Match(matcher, 0, 0);
	
	public IToken Match(string matcher, int line, int position) => new Token(TokenType.Name, matcher, line, position);
}