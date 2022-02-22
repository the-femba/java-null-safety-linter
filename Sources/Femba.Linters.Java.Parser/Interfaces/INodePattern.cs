namespace Femba.Linters.Java.Parser.Interfaces;

public interface INodePattern : IPattern<IReadOnlyList<IToken>, INode>
{
	IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node);
}