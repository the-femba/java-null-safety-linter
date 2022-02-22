namespace Femba.Linters.Java.Parser.Interfaces;

public interface IRuler
{
	IList<IRule> Rules { get; }

	void Rule(List<IToken> tokens);
}