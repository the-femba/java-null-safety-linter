namespace Femba.Linters.Java.Parser.Interfaces;

public interface INodePattern : IPattern<IReadOnlyList<IToken>, INode>
{
	Common.Parser? Parser { get; set; }
	
	List<IToken> Part(IReadOnlyList<IToken> partition, out INode node);
}