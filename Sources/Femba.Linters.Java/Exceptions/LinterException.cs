namespace Femba.Linters.Java.Parser.Exceptions;

public class LinterException : Exception
{
	public LinterException(string? message = null, Exception? innerException = null) : base(message, innerException) { }
}