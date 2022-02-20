using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Common;

public abstract class LexemePattern : ILexemePattern
{
	private readonly TokenType _type;
	
	public LexemePattern(TokenType type)
	{
		_type = type;
	}
	
	public abstract bool IsMatch(string matcher);

	public abstract string MatchLexeme(string matcher);

	public virtual IToken MatchToken(string matcher, int position) => new Token(_type, MatchLexeme(matcher), position);
}