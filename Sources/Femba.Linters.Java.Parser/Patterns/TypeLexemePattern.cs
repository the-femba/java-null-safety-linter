using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class TypeLexemePattern : ILexemePattern
{
	// TODO: Refactored regex.
	private readonly Regex _regex = new(@"^([\w]+)((\s+|\s+[\w]+)|)$");
	
	public bool IsMatch(string matcher) => _regex.IsMatch(matcher);

	public string Match(string matcher) => _regex.Match(matcher).Groups[1].Value;
}