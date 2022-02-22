namespace Femba.Linters.Java.Parser.Interfaces;

public interface INodePattern<TNode> : IPattern<IReadOnlyList<IToken>, TNode> where TNode : INode
{
	IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out TNode node);
}