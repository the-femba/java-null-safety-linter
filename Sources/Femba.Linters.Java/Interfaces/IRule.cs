namespace Femba.Linters.Java.Parser.Interfaces;

public interface IRule
{
	void Rule(IReadOnlyList<IToken> tokens);
}