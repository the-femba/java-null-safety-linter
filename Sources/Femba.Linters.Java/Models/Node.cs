using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Models;

public abstract record Node : INode
{
	public int StartPosition { get; init; }

	public int EndPosition { get; init; }
}