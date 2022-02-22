using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public abstract class NodePattern : INodePattern
{
	public abstract bool IsPart(IReadOnlyList<IToken> partition);

	public INode Part(IReadOnlyList<IToken> partition)
	{
		Part(partition, out var node);
		return node;
	}

	public Parser? Parser { get; set; }

	public abstract IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node);
}