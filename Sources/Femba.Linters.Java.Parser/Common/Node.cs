using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public abstract class Node : INode
{
	public Node(int startPosition, int endPosition)
	{
		StartPosition = startPosition;
		EndPosition = endPosition;
	}

	public int StartPosition { get; }

	public int EndPosition { get; }
}