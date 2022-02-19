namespace Femba.Linters.Java.Parser.Interfaces;

public interface IPattern<TMatcher>
{
	public bool IsMatch(TMatcher matcher);
	
	public TMatcher Match(TMatcher matcher);
}