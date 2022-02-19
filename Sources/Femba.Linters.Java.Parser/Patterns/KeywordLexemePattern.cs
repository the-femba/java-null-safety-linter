using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class KeywordLexemePattern : ILexemePattern
{
	private readonly Regex _regex = new(@"^new|this|if|else|swtich|break|return$");
	
	public bool IsMatch(string matcher) => _regex.IsMatch(matcher);

	public string Match(string matcher) => matcher;
}