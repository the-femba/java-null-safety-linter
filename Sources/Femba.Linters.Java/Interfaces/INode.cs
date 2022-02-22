namespace Femba.Linters.Java.Parser.Interfaces;

public interface INode
{
	INode? Parent { get; set; }

	int StartPosition { get; }
	
	int EndPosition { get; }
}