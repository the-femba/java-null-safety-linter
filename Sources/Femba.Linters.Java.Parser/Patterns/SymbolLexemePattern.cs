using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class SymbolLexemePattern : ILexemePattern
{
	private readonly Regex _regex = new(@"^[-+=)({}*\/;@]$");
	
	public bool IsMatch(string matcher) => _regex.IsMatch(matcher);

	public string Match(string matcher) => matcher;
}