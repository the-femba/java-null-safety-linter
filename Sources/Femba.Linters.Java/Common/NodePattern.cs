using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public abstract class NodePattern<TNode> : INodePattern<TNode> where TNode : INode
{
	public abstract bool IsPart(IReadOnlyList<IToken> partition);

	public TNode Part(IReadOnlyList<IToken> partition)
	{
		Part(partition, out var node);
		return node;
	}

	public abstract IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out TNode node);
}