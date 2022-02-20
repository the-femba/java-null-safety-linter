namespace Femba.Linters.Java.Parser.Interfaces;

public interface ILexemePattern : IPattern<string, IToken>
{
	IToken Match(string matcher, int line, int position);
}