namespace Femba.Linters.Java.Parser.Exceptions;

public class RuleLinterException : LinterException
{
	public RuleLinterException(string? message = null, Exception? innerException = null) : base(message, innerException) { }
}