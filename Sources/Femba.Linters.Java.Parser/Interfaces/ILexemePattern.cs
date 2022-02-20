namespace Femba.Linters.Java.Parser.Interfaces;

// TODO: Сделать работу с патерном лучше.
public interface ILexemePattern : IPattern<string, IToken>
{
	IToken Match(string matcher, int line, int position);
}