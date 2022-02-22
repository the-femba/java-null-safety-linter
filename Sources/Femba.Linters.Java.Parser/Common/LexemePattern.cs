using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Common;

public abstract class TokenPattern : ITokenPattern
{
	private readonly TokenType _type;
	
	public TokenPattern(TokenType type)
	{
		_type = type;
	}
	
	public abstract bool IsPart(string partition);

	public IToken Part(string partition) => Part(partition, 0);

	protected virtual string PartLexeme(string partition) => partition;

	public virtual IToken Part(string partition, int position) => new Token(_type, PartLexeme(partition), position);
}