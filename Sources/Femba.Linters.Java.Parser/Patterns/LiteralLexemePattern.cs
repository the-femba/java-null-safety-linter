using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class LiteralLexemePattern : ILexemePattern
{
	private readonly Regex _regex = new("^([0-9]+|[0-9]+\\.[0-9]+|\".+\"|'.')$");
	
	public bool IsMatch(string matcher) => _regex.IsMatch(matcher);

	public string Match(string matcher) => matcher;
}