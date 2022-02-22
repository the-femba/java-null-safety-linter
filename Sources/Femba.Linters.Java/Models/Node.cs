using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Models;

public abstract class Node : INode
{
	public INode? Parent { get; set; }

	public int StartPosition { get; init; }

	public int EndPosition { get; init; }
}