namespace Femba.Linters.Java.Parser.Interfaces;

public interface INode
{
	int StartPosition { get; }
	
	int EndPosition { get; }
}