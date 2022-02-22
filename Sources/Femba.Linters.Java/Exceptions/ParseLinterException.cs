using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Exceptions;

public class ParseLinterException : LinterException
{
	public IToken? ErrorToken;
	
	public ParseLinterException(string? message = null, IToken? errorToken = null, Exception? innerException = null) : base(message, innerException)
	{
		ErrorToken = errorToken;
	}
}