namespace Femba.Linters.Java.Parser.Interfaces;

public interface INode
{
	INode? Parent { get; set; }
	
	IReadOnlyList<INode> Children { get; }

	int StartPosition { get; }
	
	int EndPosition { get; }
}